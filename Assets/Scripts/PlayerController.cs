using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	public float walkSpeed = 7f;
	public float jumpSpeed = 3f;

	Rigidbody rb;
	Collider coll;
	MeshRenderer mr;

	// first person Camera
	public Camera playerCamera;


	[SyncVar] public bool isHost;

	[Command]
	void CmdSetHost(bool host) {
		isHost = host;
	}
		
	// Track jumping
	bool jumpPressed = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		coll = GetComponent<Collider>();
		mr = GetComponent<MeshRenderer> ();
	}

//	public override void OnStartServer() {
//
//		// enable 1st person view if player is not hosting
//		if (isServer == true) {
//		} 
//	}

	// Update is called once per frame
	void Update () {

		// basically don't run input handlers for non-local players
		if (!isLocalPlayer) return;
		CmdSetHost(isServer && isClient);

		if(isHost){
			ServerUpdate();
		} else {
			ClientUpdate();
		}
	}

	void ServerUpdate(){
		mr.enabled = false;
	}

	void ClientUpdate(){
		if (coll) {
			//Planar movement
			WalkHandler();
		}
		//Vertical movement
		JumpHandler();
	}


	void WalkHandler() {
		rb.velocity = new Vector3(0, rb.velocity.y, 0);

		float distance = walkSpeed * Time.deltaTime;

		// Input on x ("Horizontal")
		float hAxis = Input.GetAxis("Horizontal");

		// Input on z ("Vertical")
		float vAxis = Input.GetAxis("Vertical");

		// Movement vector
		Vector3 movement = new Vector3(hAxis * distance, 0f, vAxis * distance);

		// Current position
		Vector3 currPosition = transform.position;

		// Updated position
		Vector3 newPosition = currPosition + movement;

		// Apply to rigid body
		rb.MovePosition(newPosition);
	}

	void JumpHandler() {

		float jAxis = Input.GetAxis("Jump");

		bool isGrounded = CheckGrounded();

		if (jAxis > 0f) {
			if (!jumpPressed && isGrounded) {
				jumpPressed = true;

				// Jumping vector
				Vector3 jumpVector = new Vector3(0f, jumpSpeed, 0f);

				// Make the player jump by adding velocity
				rb.velocity = rb.velocity + jumpVector;
			} else {
				jumpPressed = false;
			}
		}
	}

	bool CheckGrounded() {
		// Object size in x
		float sizeX = coll.bounds.size.x;
		float sizeZ = coll.bounds.size.z;
		float sizeY = coll.bounds.size.y;

		// Position of the 4 bottom corners of the game object
		// We add 0.01 in Y so that there is some distance between the point and the floor
		Vector3 corner1 = transform.position + new Vector3(sizeX/2, -sizeY / 2 + 0.01f, sizeZ / 2);
		Vector3 corner2 = transform.position + new Vector3(-sizeX / 2, -sizeY / 2 + 0.01f, sizeZ / 2);
		Vector3 corner3 = transform.position + new Vector3(sizeX / 2, -sizeY / 2 + 0.01f, -sizeZ / 2);
		Vector3 corner4 = transform.position + new Vector3(-sizeX / 2, -sizeY / 2 + 0.01f, -sizeZ / 2);

		// Send a short ray down the cube on all 4 corners to detect ground
		bool grounded1 = Physics.Raycast(corner1, new Vector3(0, -1, 0), 0.01f);
		bool grounded2 = Physics.Raycast(corner2, new Vector3(0, -1, 0), 0.01f);
		bool grounded3 = Physics.Raycast(corner3, new Vector3(0, -1, 0), 0.01f);
		bool grounded4 = Physics.Raycast(corner4, new Vector3(0, -1, 0), 0.01f);

		// If any corner is grounded, the object is grounded
		return (grounded1 || grounded2 || grounded3 || grounded4);
 }

 void OnTriggerEnter (Collider collider) {
	 if (collider.gameObject.tag == "Goal") {
		 Destroy(gameObject);
	 }
 }
}
