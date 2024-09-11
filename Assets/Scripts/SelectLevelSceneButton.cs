using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevelSceneButton : MonoBehaviour
{
    public AudioClip clip;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = AudioManager.instance.seSound; // Sound effect's volume control
    }

    public void MoveToSelectLevelScene()
    {
        StartCoroutine(PlayAudioThenLoadScene());
    }

    IEnumerator PlayAudioThenLoadScene()
    {
        audioSource.PlayOneShot(clip);
        yield return new WaitForSecondsRealtime(clip.length);
        SceneManager.LoadScene("SelectLevelScene");
    }
}
