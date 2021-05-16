using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightLantern : Ability
{
  GameObject player;
  PlayerStats playerStats;
  PlayerAbilities playerAbil;
  GameObject lanternParticle;

  // Keeps track of what level the ability was when it was used,
  // so as not to remove too much damage from the player if they were to level up while the ability is active
  private int abilityLevelTracker;

  public override void InitializeAbility(PlayerAbilities playerAbilities)
  {
    base.InitializeAbility(playerAbilities);

    player = playerAbilities.gameObject;
    playerAbil = playerAbilities;
    playerStats = player.GetComponentInChildren<PlayerStats>();
    lanternParticle = playerAbilities.lanternParticle;
  }

  public override void ExecuteAbility()
  {
    // Play Ability Sound Effect
    audioManager.PlaySound("LightLantern");

    playerAbil.isLanternOn = true;
    playerAbil.lanternHits = 4;
    lanternParticle.SetActive(true);
    playerStats.damage.AddModifier(scaling[level]);
    abilityLevelTracker = level;
  }

  public override void TurnAbilityOff()
  {
    // Play Ability Sound Effect
    audioManager.PlaySound("TurnOffLantern");
    
    Debug.Log("Ability Light Lantern is off");
    playerStats.damage.RemoveModifier(scaling[abilityLevelTracker]);
  }
}
