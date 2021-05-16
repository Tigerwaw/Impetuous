using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{

  #region Singleton

  public static PlayerManager instance;

  void Awake()
  {
    instance = this;
  }

  #endregion

  public PlayerStats playerStats;

  public GameObject player;
  public Transform spawnPoint;
  public GameObject GameOverScreen;
  public Text goldLostText;
  public Text respawnTimeText;

  bool startDeathTimer = false;
  public float deathTimer = 10.0f;
  public int goldLoss = 25;

  public void KillPlayer()
  {
    player.SetActive(false);
    int goldLostOnDeath = PlayerStats.playerLevel * goldLoss;
    if (PlayerStats.currency < goldLostOnDeath)
    {
      goldLostOnDeath = PlayerStats.currency;
    }
    else
    {
      PlayerStats.currency -= goldLostOnDeath;
    }
    deathTimer = 10.0f;
    startDeathTimer = true;

    goldLostText.text = goldLostOnDeath.ToString();
    GameOverScreen.SetActive(true);

    playerStats.immortal = true;
    playerStats.currentHealth = 0;
  }

  public void RespawnPlayer()
  {
    player.transform.position = spawnPoint.position;
    GameOverScreen.SetActive(false);
    player.SetActive(true);
    startDeathTimer = false;
    playerStats.immortal = false;
    playerStats.currentHealth = playerStats.maxHealth.GetValue();
  }

  private void Update()
  {
    if (startDeathTimer == true)
    {
      deathTimer -= Time.deltaTime;
      int deathTimerInt = (int)deathTimer;
      respawnTimeText.text = deathTimerInt.ToString("");
      if (deathTimer <= 0)
      {
        RespawnPlayer();
      }
    }
  }
}
