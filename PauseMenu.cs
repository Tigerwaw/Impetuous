using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
  public static bool GameIsPaused = false;

  public GameObject pauseMenuUI;
  public GameObject pauseMenuPanel;
  public GameObject optionsMenuPanel;

  public AudioManager audioManager;

  private void Start()
  {
    audioManager = Camera.main.GetComponentInChildren<AudioManager>();
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      if (GameIsPaused)
      {
        Resume();
      }
      else
      {
        Pause();
      }
    }
  }

  public void Resume()
  {
    audioManager.PlaySound("ButtonClick");
    pauseMenuUI.SetActive(false);
    AudioListener.pause = false;
    Time.timeScale = 1f;
    GameIsPaused = false;
  }

  void Pause()
  {
    audioManager.PlaySound("ButtonClick");
    pauseMenuUI.SetActive(true);
    AudioListener.pause = true;
    Time.timeScale = 0f;
    GameIsPaused = true;
  }

  public void OptionsMenu()
  {
    audioManager.PlaySound("ButtonClick");
    pauseMenuPanel.SetActive(false);
    optionsMenuPanel.SetActive(true);
  }

  public void BackToPauseMenu()
  {
    audioManager.PlaySound("ButtonClick");
    optionsMenuPanel.SetActive(false);
    pauseMenuPanel.SetActive(true);
  }

  public void QuitToMenu()
  {
    audioManager.PlaySound("ButtonClick");
    SceneManager.LoadScene("MainMenu");
  }
}
