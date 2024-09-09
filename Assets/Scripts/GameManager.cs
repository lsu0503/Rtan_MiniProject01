using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Card firstCard;
    public Card secondCard;

    public Text timeText;
    float time = 0.0f;

    public GameObject endText;
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
        if (time >= 30.0f)
        {
            time = 30.00f;
            timeText.text = time.ToString("N2");
            GameOver();
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
                GameOver();
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

    void GameOver()
    {
        Time.timeScale = 0.0f;
        endText.SetActive(true);
    }
}
