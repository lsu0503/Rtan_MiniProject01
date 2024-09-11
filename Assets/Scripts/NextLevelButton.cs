using UnityEngine;

public class NextLevelButton : MonoBehaviour {
	public void GoNextLevel() {
		GameManager.instance.GoNextLevel();
	}
}