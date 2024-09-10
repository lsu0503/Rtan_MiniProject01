using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public AudioClip clip;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        audioSource.volume = AudioManager.instance.seSound;
    }

    public void StartButn()
    {
        StartCoroutine(PlayAudioThenLoadScene());
    }

    IEnumerator PlayAudioThenLoadScene()
    {
        audioSource.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        SceneManager.LoadScene("MainScene");
    }
}
