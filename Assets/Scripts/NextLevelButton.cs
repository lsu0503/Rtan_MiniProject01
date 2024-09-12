using System.Collections;
using UnityEngine;

public class NextLevelButton : MonoBehaviour
{
    public AudioClip clip;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = AudioManager.instance.seSound; // Sound effect's volume control
    }

    public void GoNextLevel()
    {
        StartCoroutine(PlayAudioThenLoadScene());
    }

    IEnumerator PlayAudioThenLoadScene()
    {
        audioSource.PlayOneShot(clip);
        yield return new WaitForSecondsRealtime(clip.length);
        GameManager.instance.StartLevel(GameManager.instance.Level + 1);
    }
}