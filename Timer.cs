using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

  public Text timerText;
  private float startTime;

	// Use this for initialization
	void Start () {

    startTime = Time.time;
  }
	
	// Update is called once per frame
	void Update () {

    float t = Time.time - startTime;

    string minutes = ((int)t / 59).ToString("00");
    string seconds = (t % 59).ToString("00");
    timerText.text = minutes + ":" + seconds;
	}
}