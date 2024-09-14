using System.Collections;
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
    float timeLimit;

    int timeRtanCount = 0;
    public GameObject timeRtan;

    public GameObject successUi;
    public GameObject failureUi;
    public GameObject clearLevelUi;
    public GameObject hiddenStageUi;
    public Text timeRecordNumText;
    public Text bestTimeRecordNumText;
    public int cardCount = 0;

    bool onAlert;
    public bool isEnd;

    int level;
    //int lastLevel = 1;

    public int Level {
        get => level;
        set => level = value;
    }

    int flipCount = 0;

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
        //animatorTimeText.SetBool("isAlert", false);
        //onAlert = false;
        //isEnd = false;
        //AudioManager.instance.SetClipStart(1);

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = AudioManager.instance.seSound; // Sound effect's volume control

        level = PlayerPrefs.GetInt("Level", 0);
        StartLevel(level);

        //Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (!isEnd)
        {
            if (level == 2) { // Hidden stage
                UpdateTimeRtan();
            }
            UpdateAlertSound();
            UpdateTimeText();
        }
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
        if (time >= timeLimit - 5f && time < timeLimit && !onAlert)
        {
            onAlert = true;
            animatorTimeText.SetBool("isAlert", true);
            AudioManager.instance.SetClipStart(2);
        }
    }

    void UpdateTimeText()
    {
        if (time >= timeLimit)
        {
            time = timeLimit;
            timeText.text = time.ToString("N2");
            isEnd = true;
            GameFailure();
        }
        else
        {
            timeText.text = time.ToString("N2");
        }
    }

    public void Matched()
    {
        flipCount++;
        if (firstCard.idx == secondCard.idx)
        {
            firstCard.DestroyCard();
            secondCard.DestroyCard();

            cardCount -= 2;
            if (cardCount <= 0)
            {
                isEnd = true;
                SetAllowLevel(level+1);

                if (level == 1) {
                    if (flipCount < 35) {
                        GoToHiddenStage();
                    } else {
                        GameSuccess();
                    }
                } else if (level == 2) {
					GameSuccess();
				} else {
                    ClearLevel();
                }
            } else {
                StartCoroutine(SoundOccur(0));
			}
        }

        else
        {
            StartCoroutine(SoundOccur(1));

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
        audioSource.PlayOneShot(clip[2]);
    }

	void GameFailure()
    {
        AudioManager.instance.StopAudio(); // Stop BGM
        Time.timeScale = 0.0f;
        failureUi.SetActive(true);

        audioSource.volume = AudioManager.instance.seSound * 0.5f;
        audioSource.PlayOneShot(clip[3]);
    }

    public void AddTime(float amount)
    {
        time -= amount;
        if (time < 0.0f)
            time = 0.0f;
    }

    public IEnumerator SoundOccur(int clipNum)
    {
        yield return new WaitForSecondsRealtime(0.7f);
        audioSource.PlayOneShot(clip[clipNum]);
    }

    public void StartLevel (int level) {
        this.level = level;
        flipCount = 0;
        time = 0f;
        timeRtanCount = 0;
		Time.timeScale = 1f;

        if (level == 0 || level == 1) {
            timeLimit = 60f;
        } else if (level == 2) {
            timeLimit = 30f;
        }

        isEnd = false;
        onAlert = false;

        successUi.SetActive(false);
        clearLevelUi.SetActive(false);
        hiddenStageUi.SetActive(false);
        failureUi.SetActive(false);

        AudioManager.instance.SetClipStart(1);
        animatorTimeText.SetBool("isAlert", false);

        BoardScript.Instance.RemoveCards();
		BoardScript.Instance.StartLevel(level);
    }

    void SetAllowLevel(int level)
    {
        string allowLevelKey = "AllowLevel";
        PlayerPrefs.SetInt(allowLevelKey, level);
    }

    void GoToHiddenStage () {
		AudioManager.instance.StopAudio(); // Stop BGM
		Time.timeScale = 0.0f;
		hiddenStageUi.SetActive(true);
		audioSource.PlayOneShot(clip[2]);
	}
}








