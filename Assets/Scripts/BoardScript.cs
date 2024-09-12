using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardScript : MonoBehaviour
{
	static BoardScript instance;

	public GameObject card;

	List<GameObject> cardList = new List<GameObject>();

	public static BoardScript Instance {
		get { return instance; }
	}

	void Awake () {
		if (instance == null) {
			instance = this;
		}
	}

	void Start()
    {
		//StartLevel(LevelPreSelector.GetInstance().PreSelectedLevel);
		//StartLevel(2);
	}

	public void StartLevel (int level) 
	{
		cardList.Clear();
		switch (level) 
		{
			case 0: 
				SetLevel0();
				break;
			case 1:
				SetLevel1(); 
				break;
			case 2:
				SetLevel2();
				break;
			default:
				break;
		}
	}

	public void RemoveCards () {
		foreach (GameObject card in cardList) {
			if (card == null) {
				continue;
			}
			Destroy(card);
		}
	}

	void SetLevel0 () {
		int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
		arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();

		for (int i = 0; i < arr.Length; i++) {
			float x = (i % 4) * 1.4f - 2.1f;
			float y = (i / 4) * -1.4f + 1.25f;

			GameObject go = Instantiate(card, transform);
			go.transform.position = new Vector2(x - 8f, y + 8f);
			go.GetComponent<Card>().Setting(arr[i], x, y, i * 0.08f);
			cardList.Add(go);
		}

		GameManager.instance.cardCount = arr.Length;
	}

    void SetLevel1 () {
		int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9 };
		arr = arr.OrderBy(x => Random.Range(0f, 9f)).ToArray();

		int l = 0, t = 0, w = 4, h = 5; // left, top, width, height
		int cx = 0, cy = 0; // current x, y
		int[] dx = {1, 0, -1, 0}; // delta x, y
		int[] dy = {0, 1, 0, -1};
		int dir = 0;

		for (int i = 0; i < arr.Length; i++) {
			float x = cx * 1.4f - 2.1f;
			float y = cy * -1.4f + 1.25f;

			GameObject go = Instantiate(card, transform);
			go.transform.position = new Vector2(x - 8f, y + 8f);
			go.GetComponent<Card>().Setting(arr[i], x, y, i * 0.08f);
			cardList.Add(go);

			int nx = cx + dx[dir];
			int ny = cy + dy[dir];

			if (nx == l && ny == t) {
				l++; t++; w--; h--;
			}
			if (nx == l-1 || ny == t-1 || nx == w || ny == h) {
				dir = (dir + 1) % 4;
			}

			cx += dx[dir];
			cy += dy[dir];
		}

		GameManager.instance.cardCount = arr.Length;
	}

	void SetLevel2 () {
		int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9 };
		arr = arr.OrderBy(x => Random.Range(0f, 9f)).ToArray();

		Vector2[] initPos = {
			new Vector2(-8f, 8f), new Vector2(-4f, 8f), new Vector2(0f, 8f), new Vector2(4f, 8f), new Vector2(8f, 8f),
			new Vector2(8f, 8f), new Vector2(8f, 4f), new Vector2(8f, 0f), new Vector2(8f, -4f), new Vector2(8f, -8f),
			new Vector2(8f, -8f), new Vector2(4f, -8f), new Vector2(0f, -8f), new Vector2(-4f, -8f), new Vector2(-8f, -8f),
			new Vector2(-8f, -8f), new Vector2(-8f, -4f), new Vector2(-8f, 0f), new Vector2(-8f, 4f), new Vector2(-8f, 8f)
		};

		int l = 0, t = 0, w = 4, h = 5; // left, top, width, height
		int cx = 0, cy = 0; // current x, y
		int[] dx = {1, 0, -1, 0}; // delta x, y
		int[] dy = {0, 1, 0, -1};
		int dir = 0;

		for (int i = 0; i < arr.Length; i++) {
			float x = cx * 1.4f - 2.1f;
			float y = cy * -1.4f + 1.25f;

			GameObject go = Instantiate(card, transform);
			go.transform.position = initPos[i];
			go.GetComponent<Card>().Setting(arr[i], x, y, i * 0.08f);
			cardList.Add(go);

			int nx = cx + dx[dir];
			int ny = cy + dy[dir];

			if (nx == l && ny == t) {
				l++; t++; w--; h--;
			}
			if (nx == l - 1 || ny == t - 1 || nx == w || ny == h) {
				dir = (dir + 1) % 4;
			}

			cx += dx[dir];
			cy += dy[dir];
		}

		GameManager.instance.cardCount = arr.Length;
	}
}
