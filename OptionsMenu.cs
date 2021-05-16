using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
  public AudioMixer audioMixer;
  public AudioManager audioManager;

  public GameObject gameplayPanel;
  public GameObject videoPanel;
  public GameObject audioPanel;
  public GameObject controlsPanel;

  Resolution[] resolutions;
  public Dropdown resolutionDropdown;

  void Start()
  {
    audioManager = Camera.main.GetComponentInChildren<AudioManager>();

    resolutions = Screen.resolutions;

    resolutionDropdown.ClearOptions();

    List<string> options = new List<string>();

    int currentResolutionIndex = 0;
    for (int i = 0; i < resolutions.Length; i++)
    {
      string option = resolutions[i].width + " x " + resolutions[i].height;
      options.Add(option);

      if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
      {
        currentResolutionIndex = i;
      }
    }

    resolutionDropdown.AddOptions(options);
    resolutionDropdown.value = currentResolutionIndex;
    resolutionDropdown.RefreshShownValue();
  }

  public void SwitchOptionsMenu(string type)
  {
    audioManager.PlaySound("ButtonClick");
    gameplayPanel.SetActive(false);
    videoPanel.SetActive(false);
    audioPanel.SetActive(false);
    controlsPanel.SetActive(false);

    if (type == "gameplay")
    {
      gameplayPanel.SetActive(true);
    }
    
    if (type == "video")
    {
      videoPanel.SetActive(true);
    }

    if (type == "audio")
    {
      audioPanel.SetActive(true);
    }

    if (type == "controls")
    {
      controlsPanel.SetActive(true);
    }
  }

  #region Audio Settings
  public void SetVolume_sfx (float volume)
  {
    audioMixer.SetFloat("sfxVolume", volume);
  }

  public void SetVolume_music(float volume)
  {
    audioMixer.SetFloat("musicVolume", volume);
  }

  public void SetVolume_ambient(float volume)
  {
    audioMixer.SetFloat("ambientVolume", volume);
  }

  public void SetVolume_ui(float volume)
  {
    audioMixer.SetFloat("uiVolume", volume);
  }
  #endregion

  #region Video Settings
  public void SetFullScreen(bool isFullscreen)
  {
    audioManager.PlaySound("ButtonClick");
    Screen.fullScreen = isFullscreen;
  }

  public void SetQuality(int qualityIndex)
  {
    audioManager.PlaySound("ButtonClick");
    QualitySettings.SetQualityLevel(qualityIndex);
  }

  public void SetResolution(int resolutionIndex)
  {
    audioManager.PlaySound("ButtonClick");
    Resolution resolution = resolutions[resolutionIndex];
    Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
  }
  #endregion
}
