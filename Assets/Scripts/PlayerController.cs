using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float walkSpeed = 7f;
	public float jumpSpeed = 9f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		// Input on x ("Horizontal")
    float hAxis = Input.GetAxis("Horizontal");

    print("Horizontal axis");
    print(hAxis);

    // Input on z ("Vertical")
    float vAxis = Input.GetAxis("Vertical");

    print("Vertical axis");
    print(vAxis);

	}
}
