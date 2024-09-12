using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectorButton: MonoBehaviour
{
    public AudioClip clip;
    AudioSource audioSource;
    public int level = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = AudioManager.instance.seSound; // Sound effect's volume control
    }

    public void SelectLevel()
    {
        StartCoroutine(PlayAudioThenLoadScene());
    }

    IEnumerator PlayAudioThenLoadScene()
    {
        audioSource.PlayOneShot(clip);
        yield return new WaitForSecondsRealtime(clip.length);
        LevelPreSelector.GetInstance().PreSelectedLevel = level;
        PlayerPrefs.SetInt("Level", level);
        SceneManager.LoadScene("MainScene");
    }
}
