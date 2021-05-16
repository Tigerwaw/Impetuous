using UnityEngine;

public class CharacterStats : MonoBehaviour {

  public int currentHealth { get; set; }
  public bool immortal = false;

  public Stat maxHealth;
  public Stat movementSpeed;
  public Stat attackSpeed;
  public Stat armor;
  public Stat damage;
  public Stat healthRegen;
  public Stat healthOnKill;

  [HideInInspector]
  public AudioManager audioManager;

  public string deathSoundName;

  public Transform recalTransform;

  public GameObject impactParticlePrefab;
  public GameObject coinParticlePrefab;

  public event System.Action<int, int, int> OnHealthChanged;
  public float regenRate = 1.0f;

  void Awake()
  {
    audioManager = gameObject.GetComponent<AudioManager>();
    currentHealth = maxHealth.GetValue();
    InvokeRepeating("HealthRegeneration", 0.0f, regenRate);
  }

  void HealthRegeneration ()
  {
    GetHeal((int)healthRegen.GetValue() / 10);
  }

  public void TakeDamage (int damage)
  {
    if (immortal)
    {
      return;
    }

    float multiplier = 1 - (0.03f * armor.GetValue());
    damage = (int)(damage * multiplier);
    damage = Mathf.Clamp(damage, 1, int.MaxValue);

    currentHealth -= damage;

    if (OnHealthChanged != null)
    {
      OnHealthChanged(maxHealth.GetValue(), currentHealth, damage);
    }

    GameObject ImpactParticle = Instantiate(impactParticlePrefab, transform.position, Quaternion.identity);
    Destroy(ImpactParticle, 5.0f);

    if (currentHealth <= 0)
    {
      GameObject coinParticle = Instantiate(coinParticlePrefab, transform.position, Quaternion.identity);
      Destroy(coinParticle, 3.0f);
      Die();
    }
  }

  public virtual void GetHeal (int heal)
  {
    currentHealth += heal;
    if (currentHealth > maxHealth.GetValue())
      currentHealth = maxHealth.GetValue();
  }

  public virtual void Die ()
  {
    audioManager.PlaySound(deathSoundName);
  }

}
