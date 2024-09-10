using System.Linq;
using UnityEngine;

public class BoardScript : MonoBehaviour
{
    public GameObject card;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();

        for(int i = 0; i < arr.Length; i++)
        {
            GameObject go = Instantiate(card, this.transform);
            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * -1.4f + 1.25f;
            go.transform.position = new Vector2(x - 8f, y + 8f);
            go.GetComponent<Card>().Setting(arr[i], x, y, i * 0.08f);
        }

        GameManager.instance.cardCount = arr.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
