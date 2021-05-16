using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnAllCamps : MonoBehaviour {

  public GameObject[] camps;
  InstantiateEnemies instantiateEnemies;

  public void RespawnCamps()
  {
    for (int i = 0; i < camps.Length; i++)
    {
      instantiateEnemies = camps[i].GetComponent<InstantiateEnemies>();
      instantiateEnemies.AttemptSpawn();
    }
  }
}
