using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour {

  public PlayerStats playerStats;

  public Transform earthSpikeOrigin;
  public GameObject lanternParticle;
  public bool isLanternOn;
  public int lanternHits;

  //Ability lists
  public Ability[] CharAbilities;

  private void Awake()
  {
    foreach (Ability a in CharAbilities)
    {
      a.InitializeAbility(this);
    }
  }

  void Update () 
  {
    CheckLanternHits();

    /*
    if (Input.GetKeyDown("t"))
    {
      Debug.Log("Cheat Mode Activated");
      PlayerStats.currency += 999;
      playerStats.currentHealth += 999;
      PlayerStats.skillPoints += 1;
      CharAbilities[0].currentCD = 0;
      CharAbilities[1].currentCD = 0;
      CharAbilities[2].currentCD = 0;
    }
    */

    for (int i = 0; i < 3; i++)
    {
      CharAbilities[i].currentCD -= Time.deltaTime;
    }

    if (Input.GetKeyDown("q"))
    {
      CharAbilities[0].UseAbility();
    }

    if (Input.GetKeyDown("w"))
    {
      CharAbilities[1].UseAbility();
    }

    if (Input.GetKeyDown("e"))
    {
      CharAbilities[2].UseAbility();
    }
  }

  private void CheckLanternHits()
  {
    if (!isLanternOn)
    {
      return;
    }

    if (lanternHits <= 0)
    {
      lanternParticle.SetActive(false);
      CharAbilities[2].TurnAbilityOff();
      isLanternOn = false;
    }
  }
}