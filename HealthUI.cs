using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class HealthUI : MonoBehaviour {

  public GameObject uiPrefab;
  public Transform target;
  float visibleTime = 5;

  float lastMadeVisibleTime;
  Transform ui;
  Image healthLostSlider;
  Image healthSlider;
  Transform cam;
  EnemyStats stats;

  float healthLostTimer = 2f;
  bool wasHealthLost;

  // Use this for initialization
  void Start ()
  {
    cam = Camera.main.transform;
    foreach (Canvas c in FindObjectsOfType<Canvas>())
    {
      if (c.renderMode == RenderMode.WorldSpace)
      {
        ui = Instantiate(uiPrefab, c.transform).transform;
        healthLostSlider = ui.GetChild(0).GetComponent<Image>();
        healthSlider = ui.GetChild(1).GetComponent<Image>();
        ui.gameObject.SetActive(false);
        break;
      }
    }
    GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
    stats = GetComponent<EnemyStats>();
	}

  void OnHealthChanged(int maxHealth, int currentHealth, int healthLost)
  {
    if (ui == null)
    {
      return;
    }
    ui.gameObject.SetActive(true);
    lastMadeVisibleTime = Time.time;

    if (currentHealth <= 0)
    {
      Destroy(ui.gameObject);
    }

    float healthPercent = (float)currentHealth / maxHealth;
    healthSlider.fillAmount = healthPercent;

    float previousHealth = currentHealth + healthLost;
    float previousHealthPercent = (float)previousHealth / maxHealth;
    healthLostSlider.fillAmount = previousHealthPercent;

    wasHealthLost = true;
  }

  public void UpdateHealthBar()
  {
    float healthPercent = (float)stats.currentHealth / stats.maxHealth.GetValue();
    healthSlider.fillAmount = healthPercent;
  }

  // Update is called once per frame
  void LateUpdate ()
  {
    if (ui == null)
    {
      return;
    }

    ui.position = target.position;
    ui.forward = -cam.forward;

    if (wasHealthLost == false)
    {
      return;
    }

    healthLostTimer -= Time.deltaTime;
    if (healthLostTimer < 1f)
    {
      healthLostSlider.fillAmount -= 0.05f;
      if (healthLostSlider.fillAmount < healthSlider.fillAmount)
      {
        healthLostSlider.fillAmount = healthSlider.fillAmount;
        wasHealthLost = false;
        healthLostTimer = 2f;
      }
    }

    if (Time.time - lastMadeVisibleTime > visibleTime)
    {
      ui.gameObject.SetActive(false);
    }
  }
}
