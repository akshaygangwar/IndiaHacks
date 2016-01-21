using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hero : MonoBehaviour {

	public float speed;

	private Text textBox;
	private bool isUpEnabled = true;
	private int randomizer;

	// Use this for initialization
	void Start () {
		textBox = FindObjectOfType<Text>();
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
	}

	void OnTriggerEnter2D(Collider2D collider) {
		GameObject obj = collider.gameObject;

		if (obj.tag == "Door_One") {			//If door one is triggered
			//Leads towards freedom from Level_1
			if(Application.loadedLevelName == "Level_1") {
				Application.LoadLevel("Level_2");
			}
			//Leads deeper from Level_2
			if (Application.loadedLevelName == "Level_2") {
				Application.LoadLevel("Level_5");
			}
			//Leads deeper from Level_4
			if (Application.loadedLevelName == "Level_4") {
				Application.LoadLevel("Level_9");
			}
			//Leads towards any of the two from Level_8
			if (Application.loadedLevelName == "Level_8") {
				LoadRandomLevel("Level_12", "Level_8", "Level_11");
			}

		} else if (obj.tag == "Door_Two") {		//If door two is triggered
			//Leads towards any of the two for Level_1
			if (Application.loadedLevelName == "Level_1") {
				LoadRandomLevel("Level_2", "Level_1", "Level_3");
			}
			//Leads towards any of the two for Level_2
			if (Application.loadedLevelName == "Level_2") {
				LoadRandomLevel("Level_4", "Level_2", "Level_5");
			}
			//Leads towards freedom from Level_4
			if (Application.loadedLevelName == "Level_4") {
				Application.LoadLevel("Level_8");
			}
			//Leads deeper from Level_8
			if(Application.loadedLevelName == "Level_8") {
				Application.LoadLevel("Level_11");
			}

		} else if (obj.tag == "Door_Three") {	//If door three is triggered, head deeper inside.
			//Leads further deep for Level_1
			if (Application.loadedLevelName == "Level_1") {
				Application.LoadLevel ("Level_3");
			}
			//Leads towards freedom from Level_2
			if (Application.loadedLevelName == "Level_2") {
				Application.LoadLevel("Level_4");
			}
			//Leads towards any of the two from Level_4
			if (Application.loadedLevelName == "Level_4") {
				LoadRandomLevel("Level_8", "Level_4", "Level_9");
			}
			//Leads towards freedom from Level_8
			if(Application.loadedLevelName == "Level_8") {
				Application.LoadLevel("Level_12");
			}

		} else if (obj.tag == "LevelGeo") {		//If level geometry is triggered, such as walls.

			isUpEnabled = false;

		} else if (obj.tag == "Box") {			//If hint boxes/scrolls are triggered.

			if(Application.loadedLevelName == "Level_1") {
				textBox.text = "Three doors you will see, one of them will help you get free. One will lead you further, deep and one " +
					"will always change where it leads." +
					"Take the first door. Or don't.";
			} else if (Application.loadedLevelName == "Level_2") {
				textBox.text = "You've taken your first step towards freedom. The rules are the same. Choose wisely." +
						" Hint: 5x - 15 = 0." +
						" I count left to right.";
			} else if (Application.loadedLevelName == "Level_4") {
				textBox.text = "Hint: tan(45°) + 1";
			} else if (Application.loadedLevelName == "Level_8") {
				textBox.text = "Hint: If a half of 5 equals 3, then the door you want is one more than the half of a third of 10.";
			}
	
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		//reset temporary changes made by triggers
		isUpEnabled = true;
		textBox.text = "";
	}

	void OnCollisionEnter2D(Collision2D collision)
	{}

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

}
