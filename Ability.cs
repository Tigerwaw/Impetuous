using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
  public Sprite icon;
  new public string name;
  [TextArea]
  public string description;
  [Space]
  public int maxLevel;
  public int level;
  public float currentCD;
  [Space]
  public int[] cooldowns;
  public string scalingType;
  public int[] scaling;

  [HideInInspector]
  public AudioManager audioManager;

  public virtual void InitializeAbility(PlayerAbilities playerAbilities)
  {
    maxLevel = 5;
    level = 0;
    currentCD = 0;
    // Initialize references the ability might need from the player

    audioManager = playerAbilities.GetComponentInParent<AudioManager>();
  }

  public virtual void UseAbility()
  {
    if (level < 1)
    {
      Debug.Log("Ability not leveled!");
      return;
    }

    if (currentCD > 1)
    {
      Debug.Log("Ability on cooldown!");
      return;
    }

    currentCD = cooldowns[level];
    ExecuteAbility();
    Debug.Log("Used Ability " + name);
  }

  public virtual void ExecuteAbility()
  {
    // Do ability code
  }

  public virtual void TurnAbilityOff()
  {
    // Optional Function for "turning off" certain ability bonus, eg. removing the damage modifier from the Light Lantern ability
  }
}
