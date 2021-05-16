using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlot : MonoBehaviour {

  public Image abilityIcon;
  public Button abilityButton;
  public Button LevelUpButton;
  public Text cooldownText;
  public Text abilityKeyText;
  [Space]

  public string abilityKey;
  public int abilityOrderNr;
  [Space]

  public GameObject[] liquids;
  [Space]

  public Animator corkAnimator;
  public GameObject vialParticleEffect;
  [Space]

  private Color untinted = new Color(1f, 1f, 1f);
  private Color tinted = new Color(0.5f, 0.5f, 0.5f);
  [Space]

  public GameObject abilityToolTip;
  public Text abilityNameText;
  public Text abilityDescText;
  public Text abilityLevelText;
  public Text abilityScalingType;
  public Text abilityScalingText;
  public Text abilityCooldownText;
  [Space]

  public PlayerAbilities playerAbilities;

  [HideInInspector]
  public AudioManager audioManager;

  [HideInInspector]
  Ability ability;

  void Start()
  {
    audioManager = Camera.main.GetComponentInChildren<AudioManager>();

    ability = playerAbilities.CharAbilities[abilityOrderNr];

    abilityKeyText.text = abilityKey;
    abilityIcon.sprite = ability.icon;

    UpdateAbilityButton();
  }

  public void OnLevelUpButton()
  {
    if (PlayerStats.skillPoints >= 1)
    {
      LevelAbility();
    }
    else
    {
      Debug.Log("No skillpoints left!");
    }
  }

  public void OnHoverOverEnter()
  {
    abilityNameText.text = ability.name;
    abilityDescText.text = ability.description;
    abilityLevelText.text = (ability.level).ToString();
    abilityScalingType.text = ability.scalingType;
    abilityScalingText.text =
      ability.scaling[1].ToString() + "/" +
      ability.scaling[2].ToString() + "/" +
      ability.scaling[3].ToString() + "/" +
      ability.scaling[4].ToString() + "/" +
      ability.scaling[5].ToString();

    abilityCooldownText.text =
      ability.cooldowns[1].ToString() + "/" +
      ability.cooldowns[2].ToString() + "/" +
      ability.cooldowns[3].ToString() + "/" +
      ability.cooldowns[4].ToString() + "/" +
      ability.cooldowns[5].ToString();

    abilityToolTip.gameObject.SetActive(true);
  }

  public void OnHoverOverExit()
  {
    abilityToolTip.gameObject.SetActive(false);
  }

  public void OnAbilityUseButton()
  {
    playerAbilities.CharAbilities[abilityOrderNr].UseAbility();
  }

  public void LevelAbility()
  {
    if (ability.level == ability.maxLevel)
    {
      Debug.Log("Ability is already at max level!");
      return;
    }


    // Play Level Ability Sound Effect
    audioManager.PlaySound("CorkClose");

    ability.level += 1;
    PlayerStats.skillPoints -= 1;
    UpdateLiquids();
  }

  private void UpdateAbilityButton()
  {
    if (ability.level == 0)
    {
      abilityButton.image.color = tinted;
      cooldownText.gameObject.SetActive(false);
      return;
    }

    if (ability.currentCD < 1)
    {
      abilityButton.image.color = untinted;
      cooldownText.gameObject.SetActive(false);
      return;
    }

    abilityButton.image.color = tinted;
    cooldownText.gameObject.SetActive(true);
    cooldownText.text = ((int)ability.currentCD).ToString("0");
  }

  private void UpdateLiquids()
  {
    for (int i = 0; i < ability.level; i++)
    {
      liquids[i].SetActive(true);
    }
  }

  private void LevelUpCheck()
  {
    if (PlayerStats.skillPoints < 1)
    {
      LevelUpButton.gameObject.SetActive(false);
      corkAnimator.SetBool("isCorkOpen", false);
      vialParticleEffect.SetActive(false);
      return;
    }

    if (ability.level >= ability.maxLevel)
    {
      LevelUpButton.gameObject.SetActive(false);
      corkAnimator.SetBool("isCorkOpen", false);
      vialParticleEffect.SetActive(false);
      return;
    }

    LevelUpButton.gameObject.SetActive(true);
    corkAnimator.SetBool("isCorkOpen", true);
    vialParticleEffect.SetActive(true);
  }

  void Update ()
  {
    LevelUpCheck();
    UpdateAbilityButton();
  }
}
