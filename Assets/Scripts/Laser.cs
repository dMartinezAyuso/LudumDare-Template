using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	LineRenderer lineRenderer;
	RaycastHit2D hit;
	public Transform laserHit;
	public GameObject smoke;
	public GameObject hitCollision;

	ParticleSystem smokeParticle;

	Character character;

	void Start () {
		lineRenderer = this.GetComponent<LineRenderer> ();
		lineRenderer.enabled = false;
		lineRenderer.useWorldSpace = true;

		smokeParticle = smoke.GetComponent<ParticleSystem> ();
		character = GetComponentInParent<Character> ();
	}

	void Update () {		 

		if (Input.GetKey (KeyCode.Q)) {
			smokeParticle.startLifetime = 0.05f;
			smoke.SetActive(true);
			hitCollision.SetActive(true);
			string direction = character.CurrentCharacterDirection();

			if(Input.GetKey("up")&&(Input.GetKey("left")||Input.GetKey("right"))){
				if(direction=="right")
					changeRotation(45,-45);
				else
					changeRotation(45,45);

				Vector3 vectorDirection = this.transform.right + Vector3.up;
				hit = Physics2D.Raycast (this.transform.position,  vectorDirection);
				Debug.DrawLine(this.transform.position,hit.point);
				laserHit.position = hit.point;
				lineRenderer.SetPosition (0, this.transform.position);
				lineRenderer.SetPosition (1, laserHit.position );
			}
			else if(Input.GetKey("up")){
				changeRotation(90,0);
				hit = Physics2D.Raycast (this.transform.position, this.transform.up);			
				Debug.DrawLine(this.transform.position,hit.point);
				laserHit.position = hit.point;
				lineRenderer.SetPosition (0, this.transform.position);
				lineRenderer.SetPosition (1, laserHit.position );
			}
			else{			
				if(direction=="right") 
					changeRotation(0,280);			
				else
					changeRotation(0,90);			
				hit = Physics2D.Raycast (this.transform.position, this.transform.right);
				Debug.DrawLine(this.transform.position,hit.point);
				laserHit.position = hit.point;
				lineRenderer.SetPosition (0, this.transform.position);
				lineRenderer.SetPosition (1, laserHit.position );
			}
			lineRenderer.enabled = true;
		}
		if (Input.GetKeyUp (KeyCode.Q)) {
			lineRenderer.enabled = false;
			smoke.SetActive(false);
			hitCollision.SetActive(false);
		}
	}

	void changeRotation(int xValue, int yValue) {
		Vector3 vectorTemp = hitCollision.transform.rotation.eulerAngles;
		vectorTemp.x = xValue;
		vectorTemp.y = yValue;
		hitCollision.transform.rotation = Quaternion.Euler(vectorTemp);
	}

}
