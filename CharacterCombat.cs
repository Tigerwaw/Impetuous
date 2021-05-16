using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour {

  private float defaultAttackSpeed = 200f;
  private float attackCooldown = 0f;
  const float combatCooldown = 5f;
  float lastAttackTime;

  bool attackSlowmo = false;
  float attackSlowmoTimer;

  public float attackDelay = .6f;

  public bool InCombat { get; private set; }
  public event System.Action OnAttack;

  CharacterStats myStats;
  CharacterStats opponentStats;
  PlayerAbilities playerAbilities;
  NavMeshAgent agent;

  private void Start()
  {
    myStats = GetComponent<CharacterStats>();
    playerAbilities = GetComponent<PlayerAbilities>();
    agent = GetComponent<NavMeshAgent>();
  }

  public void Attack (CharacterStats targetStats)
  {
    if (agent.velocity != Vector3.zero)
    {
      return;
    }

    if (targetStats.currentHealth <= 0)
    {
      return;
    }

    if (attackCooldown > 0f)
    {
      return;
    }

    opponentStats = targetStats;
    if (OnAttack != null)
      OnAttack();

    attackCooldown = defaultAttackSpeed / (100f + myStats.attackSpeed.GetValue());
    InCombat = true;
    lastAttackTime = Time.time;
  }

  public void AttackHit_AnimationEvent()
  {
    opponentStats.TakeDamage(myStats.damage.GetValue());
    myStats.audioManager.PlaySound("Punch_1");

    // Creates a slowmo effect when the player hits an enemy, the more damage done, the slower time becomes
    if (transform.gameObject.tag == "Player")
    {
      float damageTimescale = 100 - myStats.damage.GetValue();
      damageTimescale = Mathf.Clamp((damageTimescale / 100), 0.4f, 1f);
      Time.timeScale = damageTimescale;
      attackSlowmo = true;
      playerAbilities.lanternHits -= 1;
    }
  }

  void Update()
  {
    attackCooldown -= Time.deltaTime;

    if (attackSlowmo)
    {
      attackSlowmoTimer -= Time.deltaTime;
    }

    if (attackSlowmoTimer <= 0)
    {
      attackSlowmo = false;
      Time.timeScale = 1f;
      attackSlowmoTimer = .2f;
    }

    if (Time.time - lastAttackTime > combatCooldown)
    {
      InCombat = false;
    }

    if (InCombat == true)
    {
      if (opponentStats.currentHealth <= 0)
      {
        InCombat = false;
        agent.SetDestination(gameObject.transform.position);
        myStats.GetHeal((int)myStats.healthOnKill.GetValue());
        myStats.audioManager.PlaySound("GoldGained");
      }
      else
      {
        Attack(opponentStats);
      }
    }
  }
}
