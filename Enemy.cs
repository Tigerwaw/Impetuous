using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable {

  PlayerManager playerManager;
  CharacterStats myStats;

  void Start()
  {
    playerManager = PlayerManager.instance;
    myStats = GetComponent<CharacterStats>();
  }

  public override void Interact()
  {
    base.Interact();

    if (myStats.currentHealth <= 0)
    {
      return;
    }

    CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();
    if (playerCombat == null)
    {
      return;
    }

    playerCombat.Attack(myStats);
  }
}
