using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class csMyMove : MonoBehaviour {

	public float walkSpeed = 2.0f;
	public float gravity = 20.0f;
	public float jumpSpeed = 8.0f;

	private Vector3 velocity;
	private Vector3 moveTo;

	CharacterController controller;

	float life = 100;
	int score = 0;

	Text txtLife;
	Text txtScore;
	Text txtOver;
	Text txtTotalScore;

	public GameObject coinEffect;
	public GameObject ballEffect;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
		GetComponent<Animation> () ["Walk"].speed = 1.5f;

		txtLife = GameObject.Find ("txtLife").GetComponent<Text> ();
		txtScore = GameObject.Find ("txtScore").GetComponent<Text> ();
		txtOver = GameObject.Find ("txtOver").GetComponent<Text> ();
		txtTotalScore = GameObject.Find ("txtTotalScore").GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {
		if (life <= 0) {
			StopAllCoroutines ();
			Time.timeScale = 0.0f;
			GameOver ();
		}
		if (controller.isGrounded) {
			if (Input.GetButtonUp ("Fire1")) {
				moveTo = new Vector3 (0, 0, 0);

				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;

				if(Physics.Raycast(ray, out hit)){
					moveTo = new Vector3 (hit.point.x, transform.position.y, hit.point.z);
				}
			}


			if (moveTo.magnitude == 0) {
				velocity = new Vector3 (
					Input.GetAxis ("Horizontal"),
					0,
					Input.GetAxis ("Vertical"));
				velocity *= walkSpeed;

				if (Input.GetButtonDown ("Jump")) {
					velocity.y = jumpSpeed;
					StartCoroutine ("doJump");
				} else if (velocity.magnitude > 0.5) {
					GetComponent<Animation> ().CrossFade ("Walk", 0.1f);
					transform.LookAt (transform.position + velocity);
				} else {
					GetComponent<Animation> ().CrossFade ("Idle", 0.1f);
				}

			} else {
				float distance = (moveTo - transform.position).magnitude;
				//Debug.Log ("Distance .... " + distance);

				if (distance < 0.5) {
					moveTo = new Vector3 (0, 0, 0);
				}


				int xto = 0;
				int zto = 0;

				if ((moveTo.x - transform.position.x) > 0)
					xto = 1;
				if ((moveTo.x - transform.position.x) < 0)
					xto = -1;
				if ((moveTo.z - transform.position.z) > 0)
					zto = 1;
				if ((moveTo.z - transform.position.z) < 0)
					zto = -1;

				velocity = new Vector3 (
					xto,
					0,
					zto);
				velocity *= walkSpeed;

				if (velocity.magnitude > 0.5) {
					GetComponent<Animation> ().CrossFade ("Walk", 0.1f);
					transform.LookAt (moveTo);
				} else {
					GetComponent<Animation> ().CrossFade ("Idle", 0.1f);
				}
			}
		}
			
		velocity.y -= gravity * Time.deltaTime;

		controller.Move (velocity * Time.deltaTime);
	}

	IEnumerator doJump(){
		GetComponent<Animation> ().Play ("Jump");
		yield return new WaitForSeconds (0.46f);
		GetComponent<Animation> ().Play ("Idle");
	}

	void GameOver(){
		txtScore.text = "";
		txtLife.text = "";
		txtOver.text = "Game Over";
		txtTotalScore.text = "Total Score : " + score;
	}

	void OnControllerColliderHit(ControllerColliderHit hit){
		if (hit.collider.gameObject.tag == "Coin") {
			//Debug.Log ("coin");
			Destroy (hit.collider.gameObject);
			score += 1;
			txtScore.text = "Score : " + score;

			//effect
			GameObject tmp = Instantiate(coinEffect) as GameObject;
			tmp.transform.position = this.transform.position;
			Destroy (tmp, 1.0f);
		}
		else if(hit.collider.gameObject.tag == "SpikeBall"){
			//Debug.Log ("ball");
			Destroy (hit.collider.gameObject);
			life -= 10;
			txtLife.text = "Life : " + life;

			//effect
			GameObject tmp = Instantiate(ballEffect) as GameObject;
			tmp.transform.position = this.transform.position;
			Destroy (tmp, 1.0f);
		}
	}
}