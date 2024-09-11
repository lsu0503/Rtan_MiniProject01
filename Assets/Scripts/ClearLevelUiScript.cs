using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearLevelUiScript : MonoBehaviour {
	[SerializeField]
	Text timeRecordNumText;
	[SerializeField]
	Text bestTimeRecordNumText;
	
	public Text TimeRecordNumText {
		get { return timeRecordNumText; }
	}

	public Text BestTimeRecordNumText {
		get { return bestTimeRecordNumText; }
	}
}
