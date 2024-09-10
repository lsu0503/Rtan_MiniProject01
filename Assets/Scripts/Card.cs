using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public SpriteRenderer frontImage;
    public GameObject front;
    public GameObject back;
    public Animator anim;
    public Text text;

    public int idx = 0;

    float destX, destY;
    float moveAfterSec;
    [SerializeField]
    float flySpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveAfterSec -= Time.deltaTime;
		if (moveAfterSec <= 0f) {
            Vector2 dest = new Vector2(destX, destY);
            transform.position = Vector2.Lerp(transform.position, dest, flySpeed * Time.deltaTime);
        }
    }

    public void Setting(int idx, float destX, float destY, float moveAfterSec)
    {
        this.idx = idx;
        frontImage.sprite = Resources.Load<Sprite>($"rtan{this.idx}");
        this.destX = destX;
        this.destY = destY;
        this.moveAfterSec = moveAfterSec;
	}

    public void OpenCard()
    {
        anim.SetBool("isOpen", true);
        text.text = "";
        //front.SetActive(true);
        //back.SetActive(false);

        if(GameManager.instance.firstCard == null)
        {
            GameManager.instance.firstCard = this;
        }
        else if (GameManager.instance.secondCard == null)
        {
            GameManager.instance.secondCard = this;
            GameManager.instance.Matched();
        }
    }

    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 1.0f);
    }

    void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 1.0f);
    }

    void CloseCardInvoke()
    {
        anim.SetBool("isOpen", false);
		text.text = "?";
		//front.SetActive(false);
		//back.SetActive(true);
	}
}
