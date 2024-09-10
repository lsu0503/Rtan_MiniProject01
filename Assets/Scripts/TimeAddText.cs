using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class TimeAddText : MonoBehaviour
{
    public Text textComp;
    public float addAmount;
    float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 0.0f;
        textComp.text = (-addAmount).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        transform.position += Vector3.up * 0.1f * Time.deltaTime;
        Color temp = textComp.GetComponent<Text>().color;
        temp.a = 1.0f - (timer / 0.5f);
        textComp.GetComponent<Text>().color = temp;

        if (timer >= 5.0f)
            Destroy(gameObject);
    }
}
