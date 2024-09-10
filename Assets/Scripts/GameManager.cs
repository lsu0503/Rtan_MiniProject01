using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Card firstCard;
    public Card secondCard;

    public Text timeText;
    float time = 0.0f;
    int timeRtanCount = 0;
    public GameObject timeRtan;

    public GameObject successUi;
    public GameObject failureUi;
    public Text timeRecordNumText;
    public Text bestTimeRecordNumText;
    public int cardCount = 0;

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
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if((int)(time / 1.0f) > timeRtanCount)
        {
            timeRtanCount++;
            Instantiate(timeRtan);
        }

        if (time >= 30.0f)
        {
            time = 30.00f;
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
        if(firstCard.idx == secondCard.idx)
        {
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;
            if(cardCount <= 0)
            {
                GameSuccess();
            }
        }
        else
        {
            firstCard.CloseCard();
            secondCard.CloseCard();
        }
        firstCard = null;
        secondCard = null;
    }

    void GameSuccess()
    {
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

        if (bestTimeRecord < time)
        {
            bestTimeRecord = time;
        }
        else
        {
            bestTimeRecord = time;
            PlayerPrefs.SetFloat(bestTimeRecordKey, bestTimeRecord);
        }

        bestTimeRecordNumText.text = bestTimeRecord.ToString("N2");
    }

    void GameFailure()
    {
        Time.timeScale = 0.0f;
        failureUi.SetActive(true);
    }

    public void AddTime(float amount)
    {
        time -= amount;
        if (time < 0.0f)
            time = 0.0f;
    }
}
