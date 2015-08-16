using UnityEngine;
using System.Collections;

public class Meteorite : MonoBehaviour {

	Vector3 rotateAmount;

	void Start () {
		rotateAmount = new Vector3 (40,32,0);
	}

	void Update () {
		transform.Rotate (rotateAmount * Time.deltaTime);	
	}
}
