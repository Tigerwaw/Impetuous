using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCampAggro : MonoBehaviour
{
  public bool campIsAggroed = false;

  public GameObject FindClosestCamp()
  {
    GameObject[] camps;
    camps = GameObject.FindGameObjectsWithTag("Camps");
    GameObject closest = null;
    float distance = Mathf.Infinity;
    Vector3 position = transform.position;
    foreach (GameObject camp in camps)
    {
      Vector3 diff = camp.transform.position - position;
      float curDistance = diff.sqrMagnitude;
      if (curDistance < distance)
      {
        closest = camp;
        distance = curDistance;
      }
    }
    return closest;
  }
}
