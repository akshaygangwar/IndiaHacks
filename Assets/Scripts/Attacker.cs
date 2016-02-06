using UnityEngine;
using System.Collections;

public class Attacker : MonoBehaviour {

	public GameObject hero;
	private bool canMove = true;
	public Hero heroScript;
	private int flag = 0;
	private float health = 50f;
	public GameObject levelManager;
	public GameObject weapon;
	public LevelManager levelManagerScript;
	private bool isRangedAttacker = false;
	public float projectileSpeed = 0f;
	Vector3 direction;
	// Use this for initialization
	void Start () {
		if (this.tag == "RangedAttacker") {
			this.isRangedAttacker = true;
			InvokeRepeating("ThrowWeapon", 1f, 1f);
		}
		heroScript = hero.GetComponent<Hero> ();
		levelManagerScript = levelManager.GetComponent<LevelManager> ();
	}

	void ThrowWeapon() {
		Vector3 StartPosition = this.transform.position + new Vector3 (-1f, 0f, 0f);
		GameObject newWeapon = Instantiate (weapon, StartPosition, Quaternion.identity) as GameObject;
		newWeapon.transform.localScale = new Vector3 (-3f, 3f, 1f);
		newWeapon.GetComponent<Rigidbody2D> ().velocity = new Vector3 (-projectileSpeed, 0f, 0f);
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
		if (levelManagerScript.GetGameRunningStatus ()) {
			CalculateDirection ();
			if (canMove) {
				this.transform.position += direction * Time.deltaTime;
			}
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
		if (obj.tag == "Weapon") {
			DealDamage(10); //do damage to attacker
			Destroy(obj); //destroy the weapon projectile after damage has been dealt
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

	void DealDamage(float damage) {
		if (health > 0) {
			health -= damage;
		} else {
			Destroy(gameObject);
		}
	}


//	void StartAttacking(GameObject obj) {
//		obj.GetDamaged ();
//	}
}
