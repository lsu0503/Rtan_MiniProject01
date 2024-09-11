using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    AudioSource audioSource;
    public AudioClip[] clip = new AudioClip[4];
    int clipCurrent;

    public Card firstCard;
    public Card secondCard;

    public Text timeText;
    public Animator animatorTimeText;
    float time = 0.0f;

    int timeRtanCount = 0;
    public GameObject timeRtan;

    public GameObject successUi;
    public GameObject failureUi;
    public GameObject clearLevelUi;
    public Text timeRecordNumText;
    public Text bestTimeRecordNumText;
    public int cardCount = 0;

    bool onAlert;

    [SerializeField]
    int level = 0;
    int lastLevel = 1;

    public int Level {
        get { return level; }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animatorTimeText.SetBool("isAlert", false);
        onAlert = false;
        AudioManager.instance.SetClipStart(1);

        audioSource =GetComponent<AudioSource>();
        audioSource.volume = AudioManager.instance.seSound; // Sound effect's volume control

        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        UpdateTimeRtan();
        UpdateAlertSound();
        UpdateTimeText();
    }

    void UpdateTimeRtan()
    {
        if((int)(time / 1.0f) > timeRtanCount)
        {
            timeRtanCount++;
            Instantiate(timeRtan);
        }
    }

    void UpdateAlertSound()
    {
        if (time >= 25.0f && time < 30.0f && !onAlert)
        {
            onAlert = true;
            animatorTimeText.SetBool("isAlert", true);
            AudioManager.instance.SetClipStart(2);
        }
    }

    void UpdateTimeText()
    {
        if (time >= 30.0f)
        {
            time = 30.0f;
            timeText.text = time.ToString("N2");
            GameFailure();
        }
        else
        {
            timeText.text = time.ToString("N2");
        }
    }

    public void Matched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            firstCard.DestroyCard();
            secondCard.DestroyCard();

            cardCount -= 2;
            if (cardCount <= 0)
            {
                if (level == lastLevel) {
                    GameSuccess();
                } else {
                    ClearLevel();
                }
            } else {
				clipCurrent = 0;
				Invoke("SoundOccur", 0.7f);
			}
        }
        else
        {
            clipCurrent = 1;
            Invoke("SoundOccur", 0.7f);

            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        firstCard = null;
        secondCard = null;
    }

    void GameSuccess()
    {
        AudioManager.instance.StopAudio(); // Stop BGM
        Time.timeScale = 0.0f;
        successUi.SetActive(true);
		timeRecordNumText.text = time.ToString("N2");

        string bestTimeRecordKey = "bestTimeRecord";
        float bestTimeRecord;
        if (PlayerPrefs.HasKey(bestTimeRecordKey))
        {
            bestTimeRecord = PlayerPrefs.GetFloat(bestTimeRecordKey);
        }
        else
        {
            bestTimeRecord = time;
        }

        if (time < bestTimeRecord)
        {
            bestTimeRecord = time;
            PlayerPrefs.SetFloat(bestTimeRecordKey, bestTimeRecord);
        }

        bestTimeRecordNumText.text = bestTimeRecord.ToString("N2");

        audioSource.PlayOneShot(clip[2]);
    }

	public void ClearLevel () {
		AudioManager.instance.StopAudio(); // Stop BGM
		Time.timeScale = 0.0f;
        clearLevelUi.SetActive(true);
        var script = clearLevelUi.GetComponent<ClearLevelUiScript>();
        script.TimeRecordNumText.text = time.ToString("N2");


		string bestTimeRecordKey = "bestTimeRecord";
		float bestTimeRecord;
		if (PlayerPrefs.HasKey(bestTimeRecordKey)) {
			bestTimeRecord = PlayerPrefs.GetFloat(bestTimeRecordKey);
		} else {
			bestTimeRecord = time;
		}

		if (bestTimeRecord < time) {
			bestTimeRecord = time;
		} else {
			bestTimeRecord = time;
			PlayerPrefs.SetFloat(bestTimeRecordKey, bestTimeRecord);
		}

		script.BestTimeRecordNumText.text = bestTimeRecord.ToString("N2");
	}

	void GameFailure()
    {
        AudioManager.instance.StopAudio(); // Stop BGM
        Time.timeScale = 0.0f;
        failureUi.SetActive(true);

        audioSource.volume = AudioManager.instance.seSound * 0.08f;
        audioSource.PlayOneShot(clip[3]);
    }

    public void AddTime(float amount)
    {
        time -= amount;
        if (time < 0.0f)
            time = 0.0f;
    }

    public void SoundOccur()
    {
        audioSource.PlayOneShot(clip[clipCurrent]);
    }

    public void GoNextLevel () {
        level += 1;
        time = 0f;
        timeRtanCount = 0;
		Time.timeScale = 1f;

        successUi.SetActive(false);
        clearLevelUi.SetActive(false);

        BoardScript.Instance.RemoveCards();
		BoardScript.Instance.StartLevel(level);
    }

    
}








