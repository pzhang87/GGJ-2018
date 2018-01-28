using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DebrisController : NetworkBehaviour {

	Collider collider;

	public float rotationSpeed = 100f;

	void Start(){
		collider = GetComponent<Collider> ();
	}

	// Update is called once per frame
	void Update () {
	  //distance (in angles) to rotate on each frame. distance = speed * time
	  float angle = rotationSpeed * Time.deltaTime;

	  //rotate on Y
	  transform.Rotate(Vector3.up * angle, Space.World);
	}
}
