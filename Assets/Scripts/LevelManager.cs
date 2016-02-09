using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public GameObject panel;
	public GameObject button;
	public Hero hero;
	public bool isGameRunning = false; //TODO: Make private after debugging

	// Use this for initialization
	void Start () {
		hero.SetHealth (100f);
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
