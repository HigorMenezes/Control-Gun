using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCamera : MonoBehaviour {

	public Transform target;
	public float distance = 20f;
	private float angle = 30f;

	private float distanceZ;
	private float distanceY;
	private float maxDistanceY;

	private float previousAngle;

	private bool controlCamera;

	private RaycastHit hit;

	void Start () {
		controlCamera = false;

		distanceZ = distance * Mathf.Cos(angle * Mathf.Deg2Rad);
		distanceY = distance * Mathf.Sin(angle * Mathf.Deg2Rad);
		maxDistanceY = distanceY;
		updateCamera ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Space)) {
			controlCamera = true;
		} else {
			controlCamera = false;
		}
	}

	void FixedUpdate () {

		if (Physics.Raycast (transform.position, -Vector3.up, out hit)) {
			if (hit.collider.tag.Equals ("Ground")) {
				float dist = Mathf.Clamp(transform.position.y - Mathf.Abs(hit.point.y), maxDistanceY/1.2f, maxDistanceY);
				distanceY = Mathf.Lerp (distanceY, dist, 0.05f);
			}
		}

		updateCamera ();
	}

	void updateCamera (){
		transform.position = target.position - new Vector3(0, -distanceY, distanceZ);
		transform.LookAt (target.position + Vector3.up * 3);

		if (!controlCamera) {
			transform.RotateAround (target.position, Vector3.up, target.eulerAngles.y);
		} else {
			float rotate = Input.GetAxis("Mouse X");
			transform.RotateAround (target.position, Vector3.up, target.eulerAngles.y + rotate);
		}
	}

}
