using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource Music = null;
    [SerializeField] AudioSource Click = null;
    [SerializeField] AudioSource Lose = null;

    private void PlayClickAudio() => Music.Play();
    private void PlayLoseAudio() => Lose.Play();

    private void OnEnable()
    {
        Music.Play();
        GameManager.OnPlayerClick += PlayClickAudio;
        GameManager.OnPlayerLose += PlayLoseAudio;
    }

    private void OnDisable()
    {
        Music.Stop();
        GameManager.OnPlayerClick -= PlayClickAudio;
        GameManager.OnPlayerLose -= PlayLoseAudio;
    }
}
