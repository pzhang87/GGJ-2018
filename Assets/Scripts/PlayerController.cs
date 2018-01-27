using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float walkSpeed = 7f;
	public float jumpSpeed = 9f;

	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {
		MoveHandler();
	}

	void MoveHandler() {
		rb.velocity = new Vector3(0, rb.velocity.y, 0);

		float distance = walkSpeed * Time.deltaTime;

		// Input on x ("Horizontal")
		float hAxis = Input.GetAxis("Horizontal");

		print("Horizontal axis");
		print(hAxis);

		// Input on z ("Vertical")
		float vAxis = Input.GetAxis("Vertical");

		print("Vertical axis");
		print(vAxis);

		// Movement vector
		Vector3 movement = new Vector3(hAxis * distance, 0f, vAxis * distance);

		// Current position
		Vector3 currPosition = transform.position;

		// Updated position
		Vector3 newPosition = currPosition + movement;

		// Apply to rigid body
		rb.MovePosition(newPosition);
	}
}
