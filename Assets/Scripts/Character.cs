using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	const int STATE_IDLE = 0;
	const int STATE_RUN = 1;
	const int STATE_JUMP = 2;
	const int STATE_WALK = 3;

	Animator anim;
	int currentAnimationState = STATE_IDLE;

	Rigidbody2D rgbd2D;
	bool isGrounded = true;
	float runSpeed = 2.0f;
	float walkSpeed = 0.8f;
	float jumpForce = 5.0f;
	string currentDirection = "right";

	void Start() {
		anim = this.GetComponent<Animator> ();	
		rgbd2D = this.GetComponent<Rigidbody2D> ();	
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.name == "BorderHorizontal"){
			changeState(STATE_IDLE);	
			isGrounded = true;					
		}		
	}

	void FixedUpdate() {
		//StartCoroutine (Yawn());
		if (Input.GetKey ("space")&& isGrounded){
			isGrounded = false;
			rgbd2D.AddForce(new Vector2(0, jumpForce),ForceMode2D.Impulse);
			changeState(STATE_JUMP);
		}
		else if (Input.GetKey ("right")&&Input.GetKey(KeyCode.LeftShift)) {
			changeDirection ("right");
			transform.Translate(Vector3.right * runSpeed * Time.deltaTime);
			if(isGrounded)
				changeState(STATE_RUN);
		}else if (Input.GetKey ("left")&&Input.GetKey(KeyCode.LeftShift)){
			changeDirection ("left");
			transform.Translate(Vector3.right * runSpeed * Time.deltaTime);
			if(isGrounded)
				changeState(STATE_RUN);			
		}else if (Input.GetKey ("right")) {
			changeDirection ("right");
			transform.Translate(Vector3.right * walkSpeed * Time.deltaTime);
			if(isGrounded)
				changeState(STATE_WALK);
		}else if (Input.GetKey ("left")){
			changeDirection ("left");
			transform.Translate(Vector3.right * walkSpeed * Time.deltaTime);
			if(isGrounded)
				changeState(STATE_WALK);			
		}else {
			if(isGrounded)
				changeState(STATE_IDLE);
		}
	}

	void changeState(int state){		
		if (currentAnimationState == state)
			return;

		switch (state) {
			
			case STATE_RUN:
				anim.SetInteger ("state", STATE_RUN);
				break;

			case STATE_JUMP:
				anim.SetInteger ("state", STATE_JUMP);
				break;
				
			case STATE_IDLE:
				anim.SetInteger ("state", STATE_IDLE);
				break;

			case STATE_WALK:
				anim.SetInteger ("state", STATE_WALK);
				break;
			
		}	

		currentAnimationState = state;

	}

	void changeDirection(string direction){		
		if (currentDirection != direction) {
			if (direction == "right") {
				transform.Rotate (0, 180, 0);
				currentDirection = "right";
			} else if (direction == "left") {
				transform.Rotate (0, -180, 0);
				currentDirection = "left";
			}
		}		
	}

	/*IEnumerator Yawn() {
		yield return new WaitForSeconds(2);
		anim.SetBool ("isYawn", true);
	}*/
}
