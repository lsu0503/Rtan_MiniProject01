using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    AudioSource audioSource;
    public AudioClip[] clip = new AudioClip[3];

    public float bgmSound;
    public float seSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        audioSource = GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("bgmSound"))
            SetBgmSound(PlayerPrefs.GetFloat("bgmSound"));
        else
            SetBgmSound(0.5f);

        if (PlayerPrefs.HasKey("seSound"))
            SetSeSound(PlayerPrefs.GetFloat("seSound"));
        else
            SetSeSound(0.5f);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetClipStart(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetClipStart(int clipNum)
    {
        audioSource.clip = clip[clipNum];
        audioSource.Play();
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }

    public void SetBgmSound(float _Degree)
    {
        bgmSound = _Degree;
        PlayerPrefs.SetFloat("bgmSound", bgmSound);

        audioSource.volume = bgmSound * 0.5f;
    }

    public void SetSeSound(float _Degree)
    {
        seSound = _Degree;
        PlayerPrefs.SetFloat("seSound", seSound);
    }
}








