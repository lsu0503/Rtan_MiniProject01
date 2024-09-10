using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour

{
    public AudioClip clip;
    AudioSource audioSource;
    void Start()
    {
        audioSource=GetComponent<AudioSource>();

    }
    
    public void Retry()
    {
        audioSource.PlayOneShot(clip);
        SceneManager.LoadScene("MainScene");
        
    }

}
