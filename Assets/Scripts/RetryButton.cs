using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    public AudioClip clip;
    AudioSource audioSource;

    void Start()
    {
        audioSource=GetComponent<AudioSource>();
        audioSource.volume = AudioManager.instance.seSound; // Sound effect's volume control
    }
    
    public void Retry()
    {
        StartCoroutine(PlayAudioThenLoadScene());
    }

    IEnumerator PlayAudioThenLoadScene()
    {
        audioSource.PlayOneShot(clip);
        yield return new WaitForSecondsRealtime(clip.length);
        //LevelPreSelector.GetInstance().PreSelectedLevel = GameManager.instance.Level;
        //SceneManager.LoadScene("MainScene");
        GameManager.instance.StartLevel(GameManager.instance.Level);
    }
}
