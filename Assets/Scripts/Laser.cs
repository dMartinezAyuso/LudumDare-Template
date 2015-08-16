using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	LineRenderer lineRenderer;
	RaycastHit2D hit;
	public Transform laserHit;
	public GameObject smoke;

	ParticleSystem smokeParticle;

	void Start () {
		lineRenderer = this.GetComponent<LineRenderer> ();
		lineRenderer.enabled = false;
		lineRenderer.useWorldSpace = true;

		smokeParticle = smoke.GetComponent<ParticleSystem> ();
	}

	void Update () {		 

		if (Input.GetKey (KeyCode.Q)) {
			smokeParticle.startLifetime = 0.05f;
			smoke.SetActive(true);
			if(Input.GetKey("up")&&(Input.GetKey("left")||Input.GetKey("right"))){
				Vector3 direction = this.transform.right + Vector3.up;

				hit = Physics2D.Raycast (this.transform.position,  direction);
				Debug.DrawLine(this.transform.position,hit.point);
				laserHit.position = hit.point;
				lineRenderer.SetPosition (0, this.transform.position);
				lineRenderer.SetPosition (1, laserHit.position );
			}
			else if(Input.GetKey("up")){
				hit = Physics2D.Raycast (this.transform.position, this.transform.up);			
				Debug.DrawLine(this.transform.position,hit.point);
				laserHit.position = hit.point;
				lineRenderer.SetPosition (0, this.transform.position);
				lineRenderer.SetPosition (1, laserHit.position );
			}
			else{
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
		}
	}

}
