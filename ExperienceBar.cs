using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour {

  public Image experienceSlider;
  public Text experienceText;
  public PlayerStats playerStats;

  public Text levelText;

  // Use this for initialization
  void Start ()
  {
  }
	
	// Update is called once per frame
	void Update ()
  {
    float experiencePercent = (float)PlayerStats.playerExperience / PlayerStats.xpReq;
    experienceSlider.fillAmount = experiencePercent;
    string currentXP = PlayerStats.playerExperience.ToString();
    string xpRequirement = PlayerStats.xpReq.ToString();
    experienceText.text = currentXP + " / " + xpRequirement;

    levelText.text = PlayerStats.playerLevel.ToString();
  }
}
