using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
	// Create variables to hold original position and rotation of camera
	public Vector3 playerPosition;
	public Quaternion playerRotation;

	void Start() {
		// Get values for original position and rotation
		playerPosition = transform.position;
		playerRotation = transform.rotation;
	}

	void LateUpdate() {
		// Find all player objects by searching for the PlayerController class
		var players = FindObjectsOfType<PlayerController>();
		// Create variable to hold localPlayer
		PlayerController localPlayer = null;
		// Set localPlayer as value for variable
		foreach(var player in players) {
			if (player.isLocalPlayer){
				localPlayer = player;
				break;
			}
		}

		// If there is no localPlayer, exit
		if (!localPlayer) return;

		// Get the camera of the linked gameObject
		var camera = GetComponent<Camera>();

		// If there is no camera, exit
		if (!camera) return;

		// Create a variable storing whether or not the localPlayer is a host
		var isHost = localPlayer.isHost;

		// If the player is not the host, have the camera follow the character avatar
		// Otherwise, disable the camera
		if (!isHost) {
			camera.enabled = true;
			var targetTransform = localPlayer.playerCamera.transform;
			transform.position = targetTransform.position;
			transform.rotation = targetTransform.rotation;
		} else {
			camera.enabled = false;
		}
 	}
}
