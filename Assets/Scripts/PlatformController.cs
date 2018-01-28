using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

	public Transform platform;
	public Transform startTransform;
	public Transform endTransform;
	public float platformSpeed;

	Vector3 direction;
	Transform destination;

	void Start() {
		SetDestination(startTransform);
	}

	void FixedUpdate() {
		platform.GetComponent<Rigidbody>().MovePosition(platform.position + direction * platformSpeed * Time.fixedDeltaTime);

		if(Vector3.Distance(platform.position, destination.position) < platformSpeed * Time.fixedDeltaTime) {
			SetDestination(destination == startTransform ? endTransform : startTransform);
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(startTransform.position, platform.localScale);

		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(endTransform.position, platform.localScale);
	}

	void SetDestination(Transform dest) {
		destination = dest;
		direction = (destination.position - platform.position).normalized;
	}

}
