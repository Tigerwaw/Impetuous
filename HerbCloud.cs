using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbCloud : Ability
{
  public GameObject RuneHealPrefab;

  GameObject player;
  PlayerStats playerStats;
  PlayerHealthBar playerHealthBar;
  Transform playerTransform;

  public override void InitializeAbility(PlayerAbilities playerAbilities)
  {
    base.InitializeAbility(playerAbilities);

    player = playerAbilities.gameObject;
    playerStats = player.GetComponentInChildren<PlayerStats>();
    playerHealthBar = player.GetComponentInChildren<PlayerHealthBar>();
    playerTransform = player.transform;
  }

  public override void ExecuteAbility()
  {
    // Play Ability Sound Effect
    audioManager.PlaySound("HerbCloud");

    // Heal Player
    playerStats.currentHealth += scaling[level];
    if (playerStats.currentHealth > playerStats.maxHealth.GetValue())
      playerStats.currentHealth = playerStats.maxHealth.GetValue();
    playerHealthBar.UpdateHealthBar();

    // Instantiate and destroy Heal Particle Effect
    GameObject runeHealParticle = Instantiate(RuneHealPrefab, playerTransform.position, playerTransform.rotation);
    Destroy(runeHealParticle, 2.0f);
  }
}
