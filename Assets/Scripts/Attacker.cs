using UnityEngine;
using System.Collections;

public class Attacker : MonoBehaviour {

	public GameObject hero;
	private bool canMove = true;
	public Hero heroScript;
	private int flag = 0;
	Vector3 direction;
	// Use this for initialization
	void Start () {
		heroScript = hero.GetComponent<Hero> ();
	}

	void CalculateDirection() {
		if(hero) { //prevent attempt to access hero's transform after gameobject has been destroyed.
			direction = hero.transform.position - this.transform.position;
		}
		if (flag <= 5) {
			//Debug.Log(direction.ToString());
			//flag++;
		}
		//Debug.Log (direction.ToString ());
	}
	
	// Update is called once per frame
	void Update () {
		CalculateDirection ();
		if (canMove) {
			this.transform.position += direction * Time.deltaTime;
		}
	}

	void OnCollisionEnter2D(Collision2D collider) {
		Debug.Log ("collision detected");
		GameObject obj = collider.gameObject;
		if (obj.tag == "Hero") {
			canMove = false;
			InvokeRepeating("StartAttacking", 0, 0.5f);
			Debug.Log("X: " + direction.x.ToString() + " Y: " + direction.y.ToString());
		}
	}

	void StartAttacking() {
		if (hero) { //check if hero is alive
			heroScript.GetDamaged (10);
		}
	}

	void OnCollisionExit2D(Collision2D collider) {
		Debug.Log ("collision exited");
		canMove = true;
		CancelInvoke ("StartAttacking");
	}


//	void StartAttacking(GameObject obj) {
//		obj.GetDamaged ();
//	}
}
