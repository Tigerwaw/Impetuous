using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

  float lookRadius = 2.0f;
  float leashRadius = 5.0f;

  Vector3 originalPos;

  Transform target;
  NavMeshAgent agent;
  CharacterCombat combat;
  CheckCampAggro checkCampAggro;
  Transform camp;

	// Use this for initialization
	void Start ()
  {
    target = PlayerManager.instance.player.transform;
    agent = GetComponent<NavMeshAgent>();
    combat = GetComponent<CharacterCombat>();
    checkCampAggro = GetComponentInParent<CheckCampAggro>();
    camp = checkCampAggro.FindClosestCamp().transform;

    originalPos = gameObject.transform.position;
  }
	
	// Update is called once per frame
	void Update ()
  {
    float distance = Vector3.Distance(target.position, transform.position);
    float distanceFromCamp = Vector3.Distance(camp.position, transform.position);

    if (distance <= lookRadius)
    {
      agent.SetDestination(target.position);
      checkCampAggro.campIsAggroed = true;

      if (distance <= agent.stoppingDistance)
      {
        CharacterStats targetStats = target.GetComponent<CharacterStats>();
        if (targetStats != null && targetStats.currentHealth > 0)
        {
          combat.Attack(targetStats);
        }
        FaceTarget();
      }
    }

    if (checkCampAggro.campIsAggroed == true)
    {
      agent.SetDestination(target.position);
    }

    if (leashRadius <= distanceFromCamp)
    {
      checkCampAggro.campIsAggroed = false;
      agent.SetDestination(originalPos);
    }
  }

  void FaceTarget ()
  {
    Vector3 direction = (target.position - transform.position).normalized;
    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, lookRadius);
    Gizmos.DrawWireSphere(transform.position, leashRadius);
  }
}
