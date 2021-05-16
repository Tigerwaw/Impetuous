using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class PlayerHealthBar : MonoBehaviour {

  public Image healthSlider;
  public Image healthLostSlider;
  public Text healthText;
  public PlayerStats playerStats;

  bool wasHealthLost;
  float healthLostTimer = 2f;

  void Start ()
  {
    UpdateHealthBar();
    GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
  }

  public void UpdateHealthBar()
  {
    float healthPercent = (float)playerStats.currentHealth / playerStats.maxHealth.GetValue();
    healthSlider.fillAmount = healthPercent;

    string health = playerStats.currentHealth.ToString();
    string maxhealth = playerStats.maxHealth.GetValue().ToString();
    healthText.text = health + " / " + maxhealth;
  }

  void OnHealthChanged(int maxHealth, int currentHealth, int healthLost)
  {
    float healthPercent = (float)currentHealth / maxHealth;
    healthSlider.fillAmount = healthPercent;

    float previousHealth = currentHealth + healthLost;
    float previousHealthPercent = (float)previousHealth / maxHealth;
    healthLostSlider.fillAmount = previousHealthPercent;

    string health = playerStats.currentHealth.ToString();
    string maxhealth = playerStats.maxHealth.GetValue().ToString();
    healthText.text = health + " / " + maxhealth;

    wasHealthLost = true;
  }

  void LateUpdate ()
  {
    UpdateHealthBar();

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
  }
}
