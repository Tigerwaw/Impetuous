using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateEnemies : MonoBehaviour {

  public GameObject[] campPrefabs;
  public GameObject fireParticlePrefab;

  public bool isBoxEmpty = true;
  int randomInt;
  public float timeUntilRespawn = 60.0f;
  bool startTimer = false;

  private void Awake()
  {
    AttemptSpawn();
  }

  public void AttemptSpawn()
  {
    if (isBoxEmpty == true)
    {
      randomInt = Random.Range(0, campPrefabs.Length);
      Instantiate(campPrefabs[randomInt], transform.localPosition, Quaternion.identity);
    }
  }

  void OnTriggerStay (Collider collider)
  {
    if (collider.gameObject.tag == "Enemy")
      isBoxEmpty = false;
      startTimer = false;
  }

  void OnTriggerEnter (Collider collider)
  {
    if (collider.gameObject.tag == "Enemy")
      isBoxEmpty = false;
      startTimer = false;
  }

  void OnTriggerExit (Collider collider)
  {
    if (collider.gameObject.tag == "Enemy")
      isBoxEmpty = true;
      startTimer = true;
  }

  void Update()
  {
    if (startTimer == true)
    {
      timeUntilRespawn -= Time.deltaTime;
      if (timeUntilRespawn < 0)
      {
        AttemptSpawn();
      }
    }
    else
    {
      timeUntilRespawn = 60.0f;
    }
  }
}
