using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hero : MonoBehaviour {

	float speed = 0;
	string direction;

	public float health = 100f;

	public GameObject weapon;
	public float projectileSpeed;
	public float projectileDamage;

	public Text textBox;
	private bool isUpEnabled = true;
	private bool isFacingLeft = false;
	private bool isAttackerLevel = false;
	private int randomizer;
	private int projectileCount = 0;
	//private bool isMoving = false;

	// Use this for initialization
	void Start () {
		//textBox = FindObjectOfType<Text>();
		if (Application.loadedLevelName == "Level_3" || Application.loadedLevelName == "Level_10" ) {
			isAttackerLevel = true;
		}
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 viewPos = Camera.main.WorldToViewportPoint (this.transform.position);
		viewPos.x = Mathf.Clamp01 (viewPos.x);
		viewPos.y = Mathf.Clamp01 (viewPos.y);
		this.transform.position = Camera.main.ViewportToWorldPoint (viewPos);

		//Vector3 heroPosition = gameObject.transform.position;

		if(Input.GetKey (KeyCode.LeftArrow)){
			gameObject.transform.position += Vector3.left * speed * Time.deltaTime;
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			gameObject.transform.position += Vector3.right * speed * Time.deltaTime;
		}

		if (Input.GetKey (KeyCode.UpArrow)) {
			if(isUpEnabled){
				gameObject.transform.position += Vector3.up * speed * Time.deltaTime;
			}
		}

		if (Input.GetKey (KeyCode.DownArrow)) {
			gameObject.transform.position += Vector3.down * speed * Time.deltaTime;
		}

		Move (speed, direction);
		if (health <= 0) {
			Destroy(gameObject);
		}

	}

	//TODO: Add door handlers for transitions from Level_9 onwards
	//TODO: Add hint for Level_9

	void OnTriggerEnter2D(Collider2D collider) {
		GameObject obj = collider.gameObject;

		if (obj.tag == "Door_One") {			//If door one is triggered
			//Leads towards freedom from Level_1
			if (Application.loadedLevelName == "Level_1") {
				Application.LoadLevel ("Level_2");
			}
			//Leads deeper from Level_2
			if (Application.loadedLevelName == "Level_2") {
				Application.LoadLevel ("Level_5");
			}

			//Leads towards any of the two from Level_3
			if(Application.loadedLevelName == "Level_3") {
				LoadRandomLevel("Level_6", "Level_3", "Level_7");
			}
			//Leads deeper from Level_4
			if (Application.loadedLevelName == "Level_4") {
				Application.LoadLevel ("Level_9");
			}
			//Leads towards freedom from Level_5
			if (Application.loadedLevelName == "Level_5") {
				Application.LoadLevel ("Level_9");
			}
			//Leads deeper from Level_6
			if(Application.loadedLevelName == "Level_6") {
				Application.LoadLevel("Level_5");
			}
			//Leads towards any of the two from Level_8
			if (Application.loadedLevelName == "Level_8") {
				LoadRandomLevel ("Level_12", "Level_8", "Level_11");
			}
			//Leads towards freedom from Level_10
			if(Application.loadedLevelName == "Level_10") {
				Application.LoadLevel("Level_4");
			}
			//Leads deeper from Level_12
			if (Application.loadedLevelName == "Level_12") {
				Application.LoadLevel ("Level_7");
			}

		} else if (obj.tag == "Door_Two") {		//If door two is triggered
			//Leads towards any of the two for Level_1
			if (Application.loadedLevelName == "Level_1") {
				LoadRandomLevel ("Level_2", "Level_1", "Level_3");
			}
			//Leads towards any of the two for Level_2
			if (Application.loadedLevelName == "Level_2") {
				LoadRandomLevel ("Level_4", "Level_2", "Level_5");
			}

			//Leads deeper from Level_3
			if(Application.loadedLevelName == "Level_3") {
				Application.LoadLevel("Level_7");
			}
			//Leads towards freedom from Level_4
			if (Application.loadedLevelName == "Level_4") {
				Application.LoadLevel ("Level_8");
			}
			//Leads towards any of the two from Level_5
			if (Application.loadedLevelName == "Level_5") {
				LoadRandomLevel("Level_9", "Level_5", "Level_3");
			}
			//Leads towards freedom from Level_6
			if(Application.loadedLevelName == "Level_6") {
				Application.LoadLevel("Level_10");
			}
			//Leads deeper from Level_8
			if (Application.loadedLevelName == "Level_8") {
				Application.LoadLevel ("Level_11");
			}
			//Leads to any of the two from Level_10
			if (Application.loadedLevelName == "Level_10") {
				LoadRandomLevel ("Level_4", "Level_10", "Level_11");
			}
			//Leads to win screen from Level_12
			if (Application.loadedLevelName == "Level_12") {
				Application.LoadLevel ("WinScene");
			}

		} else if (obj.tag == "Door_Three") {	//If door three is triggered, head deeper inside.
			//Leads further deep for Level_1
			if (Application.loadedLevelName == "Level_1") {
				Application.LoadLevel ("Level_3");
			}
			//Leads towards freedom from Level_2
			if (Application.loadedLevelName == "Level_2") {
				Application.LoadLevel ("Level_4");
			}
			//Leads towards freedom from Level_3
			if(Application.loadedLevelName == "Level_3") {
				Application.LoadLevel("Level_6");
			}
			//Leads towards any of the two from Level_4
			if (Application.loadedLevelName == "Level_4") {
				LoadRandomLevel ("Level_8", "Level_4", "Level_9");
			}
			//Leads deeper from Level_5
			if (Application.loadedLevelName == "Level_5") {
				Application.LoadLevel("Level_3");
			}
			//Leads towards any level from Level_6
			if(Application.loadedLevelName == "Level_6") {
				LoadRandomLevel("Level_10", "Level_6", "Level_5");
			}
			//Leads towards freedom from Level_8
			if (Application.loadedLevelName == "Level_8") {
				Application.LoadLevel ("Level_12");
			}
			//Leads deeper from Level_10
			if (Application.loadedLevelName == "Level_10") {
				Application.LoadLevel("Level_11");
			}
			//Leads towards any level from Level_12
			if (Application.loadedLevelName == "Level_12") {
				LoadRandomLevel ("WinScene", "Level_12", "Level_7");
			}

		} else if (obj.tag == "LevelGeo") {		//If level geometry is triggered, such as walls.

			isUpEnabled = false;

		} else if (obj.tag == "Box") {			//If hint boxes/scrolls are triggered.

			if (Application.loadedLevelName == "Level_1") {
				textBox.text = "Three doors you will see, one of them will help you get free. One will lead you further, deep and one " +
					"will always change where it leads." +
					"Take the first door. Or don't.";
			} else if (Application.loadedLevelName == "Level_2") {
				textBox.text = "You've taken your first step towards freedom. The rules are the same. Choose wisely." +
					" Hint: 5x - 15 = 0." +
					" I count left to right.";
			} else if (Application.loadedLevelName == "Level_3") {

				textBox.text = "Find the next number in the series 2 9 3 1 8 4 3 6 5 7 _ and go to it's right.";

			} else if (Application.loadedLevelName == "Level_4") {
				textBox.text = "Hint: tan(45°) + 1";
			} else if (Application.loadedLevelName == "Level_5") {
				textBox.text = "What is the value of (i^2) * (-1)?";
			} else if (Application.loadedLevelName == "Level_6") {
				textBox.text = "What least number must be added to 1056, so that the sum is completely divisible by 23? Take your time.";
			} else if (Application.loadedLevelName == "Level_8") {
				textBox.text = "Hint: If a half of 5 equals 3, then the door you want is one more than the half of a third of 10.";
			} else if (Application.loadedLevelName == "Level_10") {
				textBox.text = "Door number 0! will lead you on... That's not just an exclamation; just saying.";
			} else if (Application.loadedLevelName == "Level_12") {
				textBox.text = "Finally, you're just one step away from succeeding in your quest." +
					"Talk to the assassin. He'll help you.";
			}
	
		} else if (obj.tag == "HelpfulAssassin") {
			textBox.text = "If the number 481 ? 67 is completely divisible by 9, then the smallest whole number in place of ? " +
				"will be on the left of the door you need.";
		} else if (obj.tag == "HealthPotion") {
			if(health <= 50f) { //If health is less than 50, add 50 health.
				health += 50f;
				Destroy(obj);		//Destroy the health potion after consuming
			} else if (health > 50 && health < 100){			//otherwise (if health > 50), simply make health 100 to avoid overflows
				health = 100f;
				Destroy(obj);		//Destroy the health potion after consuming
			}
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		//reset temporary changes made by triggers
		isUpEnabled = true;
		if (isAttackerLevel == true) {
		} else {
			textBox.text = "";
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		GameObject obj = collision.gameObject;
		if (obj.tag == "Weapon") {
			GetDamaged(10f);
			Destroy(obj);
		}
	}

	void LoadRandomLevel(string upperLevel, string currentLevel, string lowerLevel) {
		randomizer = Random.Range(0,3);
		
		if (randomizer == 0) {
			//Load same level
			Application.LoadLevel(currentLevel);
		}
		
		else if (randomizer == 1) {
			//Load Level that leads to freedom
			Application.LoadLevel(upperLevel);
		}
		
		else {
			//Load level that goes further deep
			Application.LoadLevel(lowerLevel);
		}

	}
//	public void SetIsMoving() {
//		isMoving = true;
//	}
//
//	public void SetIsNotMoving() {
//		isMoving = false;
//	}

	public void SetSpeed(float desiredSpeed) {
		speed = desiredSpeed;
	}

	public void SetDirection (string desiredDirection) {
		direction = desiredDirection;
	}

//	public void IncreaseSpeed () {
//		if (isMoving) {
//			SetSpeed (10);
//		} else {
//			SetSpeed (0);
//		}
//	}

	void Move(float velocity, string requiredDirection) {
		if (requiredDirection == "up") {
			if (isUpEnabled) {
				this.transform.position += Vector3.up * velocity * Time.deltaTime;
			}
		}
		if (requiredDirection == "down") {
			this.transform.position += Vector3.down * velocity * Time.deltaTime;
		}
		if (requiredDirection == "right") {
			isFacingLeft = false;
			this.transform.localScale = new Vector3(0.6875004f, 1f, 1f);
			this.transform.position += Vector3.right * velocity * Time.deltaTime;
		}
		if (requiredDirection == "left") {
			isFacingLeft = true;
			this.transform.localScale = new Vector3(-0.6875004f, 1f, 1f); 
			this.transform.position += Vector3.left * velocity * Time.deltaTime;
		}
	}

	public void GetDamaged(float damage) {
		health -= damage;
		//Debug.Log (health.ToString ());
	}

	public void Fire() {
		if (projectileCount <= 10) {
			projectileCount++;
			if (!isFacingLeft) {
				Vector3 startPosition = transform.position + new Vector3 (1, 0, 0);
				GameObject newWeapon = Instantiate (weapon, startPosition, Quaternion.identity) as GameObject;
				newWeapon.GetComponent<Rigidbody2D> ().velocity = new Vector3 (projectileSpeed, 0, 0);
			} else {
				Vector3 startPosition = transform.position + new Vector3 (-1, 0, 0);
				GameObject newWeapon = Instantiate (weapon, startPosition, Quaternion.identity) as GameObject;
				newWeapon.transform.localScale = new Vector3 (-3f, 3f, 1f);
				newWeapon.GetComponent<Rigidbody2D> ().velocity = new Vector3 (-projectileSpeed, 0, 0);
			}
		}
	}

	public int GetProjectileCount() {
		return projectileCount;
	}

	public void DecrementProjectileCount() {
		projectileCount--;
	}
}
