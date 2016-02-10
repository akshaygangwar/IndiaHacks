using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeDisplay : MonoBehaviour {

	public GameObject levelManager;
	
	private LevelManager levelManagerScript;
	private Text timeText;
	private static float seconds = 0f;
	private static float minutes = 0f;

	// Use this for initialization
	void Start () {
		timeText = this.GetComponent<Text> ();
		//timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
		levelManagerScript = levelManager.GetComponent<LevelManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (levelManagerScript.GetGameRunningStatus ()) {
			if (seconds < 60) {
				seconds += Time.deltaTime;
			} else if (seconds >= 60) {
				seconds = 0f;
				minutes++;
			}
			float formattedSeconds = seconds % 60;
			timeText.text = minutes.ToString ("00") + ":" + formattedSeconds.ToString ("00");
		}
	}
}
