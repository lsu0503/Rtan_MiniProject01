using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoHiddenStageButton : MonoBehaviour {
	public void GoToHiddenStage() {
		GameManager.instance.StartLevel(2);
	}
}