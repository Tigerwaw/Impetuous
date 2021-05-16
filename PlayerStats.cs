using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats {

  public static int currency { get; set; }
  public static int playerExperience { get; set; }
  public static int xpReq { get; set; }
  public static int playerLevel { get; set; }
  public static int skillPoints { get; set; }

  PlayerHealthBar playerHealthBar;

  void Start ()
  {
    playerHealthBar = gameObject.GetComponent<PlayerHealthBar>();

    currency = 50;
    playerExperience = 0;
    playerLevel = 1;
    xpReq = 120 * playerLevel;
    skillPoints = 1;
  }

  public void ApplyItemEffects (Item item)
  {
    armor.AddModifier(item.armorModifier);
    damage.AddModifier(item.damageModifier);
    maxHealth.AddModifier(item.maxHealthModifier);
    attackSpeed.AddModifier(item.attackSpeedModifier);
    healthRegen.AddModifier(item.healthRegenModifier);
    movementSpeed.AddModifier(item.movementSpeedModifier);
    healthOnKill.AddModifier(item.healthOnKillModifier);
  }

  public void RemoveItemEffects (Item item)
  {
    armor.RemoveModifier(item.armorModifier);
    damage.RemoveModifier(item.damageModifier);
    maxHealth.RemoveModifier(item.maxHealthModifier);
    attackSpeed.RemoveModifier(item.attackSpeedModifier);
    healthRegen.RemoveModifier(item.healthRegenModifier);
    movementSpeed.RemoveModifier(item.movementSpeedModifier);
    healthOnKill.RemoveModifier(item.healthOnKillModifier);
  }

  public override void Die()
  {
    base.Die();
    PlayerManager.instance.KillPlayer();
  }

  public override void GetHeal(int heal)
  {
    base.GetHeal(heal);

    playerHealthBar.UpdateHealthBar();
  }

  public static void GiveExperience(int xp)
  {
    int xpUntilNextLevel = xpReq - playerExperience;
    if (xp >= xpUntilNextLevel)
    {
      AudioManager audioMng = Camera.main.GetComponentInChildren<AudioManager>();
      audioMng.PlaySound("LevelUp");
      // Do level and xp stuff
      playerExperience = xp - xpUntilNextLevel;
      playerLevel += 1;
      skillPoints += 1;
      xpReq = 120 * playerLevel;
      if (playerExperience < 0)
        playerExperience = 0;
    }
    else
    {
      playerExperience += xp;
    }
  }
}
