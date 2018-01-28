using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DebrisController : NetworkBehaviour {

	Collider collider;

	public float rotationSpeed = 100f;

	public float radius = 5.0F;
	public float power = 10.0F;

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

	void onCollisionEnter(Collision collision){
		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
		foreach (Collider hit in colliders) {
			Rigidbody rb = hit.GetComponent<Rigidbody> ();

			if (rb != null)
				rb.AddExplosionForce (power, explosionPos, radius, 3.0F);
		}

		Destroy (this);
	}
}
