using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public GameObject panel;
	public GameObject button;
	public GameObject pausePanel;
	public GameObject playButton;
	public GameObject pauseButton;
	public GameObject quitButton;
	public Hero hero;
	public bool isGameRunning = false; //TODO: Make private after debugging

	// Use this for initialization
	void Start () {
		if (!(Application.loadedLevelName == "WinScene")) {
			if (!panel) {
				SetGameRunningStatus (true);
				pauseButton.SetActive (true);
			}
			hero.SetHealth (100f);
			pausePanel.SetActive (false);
			playButton.SetActive (false);
			quitButton.SetActive (false);
			if (panel) {
				pauseButton.SetActive (false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartClicked() {
		SetGameRunningStatus (true);
		Destroy (panel);
		//panel.SetActive (false);
		Destroy (button);
		//button.SetActive (false);
		pauseButton.SetActive (true);

	}

	public void PauseButtonClicked() {
		pauseButton.SetActive (false);
		pausePanel.SetActive (true);
		playButton.SetActive (true);
		quitButton.SetActive (true);
		SetGameRunningStatus (false);
	}

	public void PlayButtonClicked() {
		pausePanel.SetActive (false);
		playButton.SetActive (false);
		pauseButton.SetActive (true);
		quitButton.SetActive (false);
		SetGameRunningStatus (true);
	}

	public void QuitButtonClicked() {
		Application.Quit ();
	}

	public bool GetGameRunningStatus() {
		return isGameRunning;
	}

	void SetGameRunningStatus(bool status) {
		isGameRunning = status;
	}


}
