using UnityEditor.Build.Content;
using UnityEngine;

public class TimeRtan : MonoBehaviour
{
    public GameObject moving;
    public GameObject touched;
    public GameObject textAddTime;

    AudioSource audioSource;
    public AudioClip clip;

    bool isOver;

    float minY = 2.5f;
    float maxY = 4.5f;
    float velocity = 1.5f;
    float addTime = 0.5f;
    bool isRight;
    bool isClicked;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = AudioManager.instance.seSound; // Sound effect's volume control

        moving.SetActive(true);
        touched.SetActive(false);

        if (Random.Range(0.0f, 1.0f) > 0.5f)
        {
            isRight = true;
            transform.position = new Vector3(3.3f, Random.Range(minY, maxY), 0.0f);
            moving.GetComponent<SpriteRenderer>().flipX = true;
        }

        else
        {
            isRight = false;
            transform.position = new Vector3(-3.3f, Random.Range(minY, maxY), 0.0f);
            moving.GetComponent<SpriteRenderer>().flipX = false;
        }

        isOver = false;
        isClicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isClicked)
        {
            if (isRight)
            {
                transform.position += Vector3.left * velocity * Time.deltaTime;
                if (transform.position.x < -3.5f)
                    Destroy(gameObject);
            }
            else
            {
                transform.position += Vector3.right * velocity * Time.deltaTime;
                if (transform.position.x > 3.5f)
                    Destroy(gameObject);
            }

            if (isOver && Input.GetMouseButtonDown(0))
            {
                OnClick();
            }

            if (GameManager.instance.isEnd)
                Invoke("DestroyObject", 0.05f);
        }
    }

    public void OnClick()
    {
        isClicked = true;
        moving.SetActive(false);
        touched.SetActive(true);

        audioSource.PlayOneShot(clip);

        Invoke("Clicked", 0.5f);
    }

    void Clicked()
    {
        GameManager.instance.AddTime(addTime);
        GameObject timeAddTextObj = Instantiate(textAddTime, position: transform.position, rotation: Quaternion.identity);
        timeAddTextObj.SetActive(false);
        timeAddTextObj.GetComponent<TimeAddText>().addAmount = addTime;
        timeAddTextObj.SetActive(true);

        Destroy(gameObject);
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void OnMouseOver()
    {
        isOver = true;
    }

    private void OnMouseExit()
    {
        isOver = false;
    }
}
