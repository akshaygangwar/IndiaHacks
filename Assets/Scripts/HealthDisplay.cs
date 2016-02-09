using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthDisplay : MonoBehaviour {

	private Text healthText;
	public GameObject heroObject;
	// Use this for initialization
	void Start () {
		healthText = this.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		healthText.text = heroObject.GetComponent<Hero> ().GetHealth().ToString ();
	}
}
