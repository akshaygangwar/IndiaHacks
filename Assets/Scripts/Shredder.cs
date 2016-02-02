using UnityEngine;
using System.Collections;

public class Shredder : MonoBehaviour {

	public GameObject instanceOfHero;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D collision) {
		Destroy (collision.gameObject);
		instanceOfHero.GetComponent<Hero>().DecrementProjectileCount ();
		Debug.Log (instanceOfHero.GetComponent<Hero> ().GetProjectileCount ());
	}
}
