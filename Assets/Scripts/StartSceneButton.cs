using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneButton : MonoBehaviour
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

    public void StartSceneButn()
    {
        StartCoroutine(PlayAudioThenLoadScene());
    }

    IEnumerator PlayAudioThenLoadScene()
    {
        audioSource.PlayOneShot(clip);
        yield return new WaitForSecondsRealtime(clip.length);
        Time.timeScale = 1.0f;
        Destroy(AudioManager.instance);
        SceneManager.LoadScene("StartScene");
    }
}
