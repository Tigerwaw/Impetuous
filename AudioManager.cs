using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
  private AudioSource[] sounds;

  public void Awake()
  {
    sounds = GetComponentsInChildren<AudioSource>();
  }

  public void PlaySound(string name)
  {
    AudioSource s = Array.Find(sounds, sound => sound.clip.name == name);
    if (s == null)
    {
      Debug.LogWarning("Can't find specified sound: " + name);
      return;
    }
    s.Play();
  }
}