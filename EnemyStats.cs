using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats {

  public int lootMin;
  public int lootMax;

  public int xpLootMin;
  public int xpLootMax;

  [HideInInspector]
  public HealthUI healthUI;

  private void Start()
  {
    healthUI = gameObject.GetComponent<HealthUI>();
  }

  public override void GetHeal(int heal)
  {
    base.GetHeal(heal);

    healthUI.UpdateHealthBar();
  }

  public override void Die()
  {
    base.Die();

    // Add ragdoll effect / death animation
    // Give player loot / Experience

    int currencyLoot = Random.Range(lootMin, lootMax);
    PlayerStats.currency += currencyLoot;

    int experienceLoot = Random.Range(xpLootMin, xpLootMax);
    PlayerStats.GiveExperience(experienceLoot);

    transform.position = new Vector3(-100f, -100f, -100f);
    Destroy(gameObject, 0.5f);
  }
}