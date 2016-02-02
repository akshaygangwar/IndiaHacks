using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public GameObject panel;
	public GameObject button;

	private bool isGameRunning = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartClicked() {
		Destroy (panel);
		Destroy (button);
		isGameRunning = true;

	}

	public bool GetGameRunningStatus() {
		return isGameRunning;
	}


}
