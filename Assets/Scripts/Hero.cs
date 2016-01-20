using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hero : MonoBehaviour {

	public float speed;

	private Text textBox;
	private bool isUpEnabled = true;

	// Use this for initialization
	void Start () {
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
		if (obj.tag == "Door_One") {			//If door one is triggered.
			Debug.Log ("Door 1");
		} else if (obj.tag == "Door_Two") {		//If door two is triggered.
			Debug.Log ("Door Two");
		} else if (obj.tag == "Door_Three") {	//If door three is triggered.
			Debug.Log ("Door Three");
		} else if (obj.tag == "LevelGeo") {		//If level geometry is triggered, such as walls.
			isUpEnabled = false;
		} else if (obj.tag == "Box") {			//If hint boxes/scrolls are triggered.
			textBox = FindObjectOfType<Text>();
			textBox.text = "Three doors you will see, one of them will help you get free. One will lead you further, deep and one " +
				"will always change where it leads.";
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		//reset temporary changes made by triggers
		isUpEnabled = true;
		textBox.text = "";
	}

	void OnCollisionEnter2D(Collision2D collision)
	{}
}
