using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothController : MonoBehaviour {

	public float maxSpeed = 3f;
	public float acceleration = 0.1f;

	public float maxSpeedRotate = 30f;
	public float accelerationRotate = 1f;

	public float distance = 1f;

	public float speed;
	private float smoothStop = 4f;

	public float speedRotate;
	private float smoothStopRotate = 4f;

	private RaycastHit hit;

	// Use this for initialization
	void Start () {
		speed = 0;
		speedRotate = 0;
		Physics.Raycast (transform.position, -transform.up, out hit);
		transform.up = hit.normal;
	}
	
	// Update is called once per frame
	void Update () {

		//Debug.Log (Physics.Raycast (transform.position + new Vector3 (0, 1, 0), -transform.up, out hit, distance));

		if (Physics.Raycast (transform.position + new Vector3 (0, 1, 0), -transform.up, out hit, distance)) {

			//Rotate
			if (Input.GetKey (KeyCode.D)) {
				speedRotate += accelerationRotate;
				speedRotate = Mathf.Clamp (speedRotate, -maxSpeedRotate, maxSpeedRotate);
			} else if (Input.GetKey (KeyCode.A)) {
				speedRotate -= accelerationRotate;
				speedRotate = Mathf.Clamp (speedRotate, -maxSpeedRotate, maxSpeedRotate);
			} else {
				//Debug.Log ("Entrou");
				speedRotate = Mathf.Lerp (speedRotate, 0, smoothStopRotate * Time.deltaTime);
			}

			// Front and Back
			if (Input.GetKey (KeyCode.W)) {
				speed += acceleration;
				speed = Mathf.Clamp (speed, -maxSpeed, maxSpeed);
			} else if (Input.GetKey (KeyCode.S)) {
				speed -= acceleration;
				speed = Mathf.Clamp (speed, -maxSpeed, maxSpeed);
			} else {
				//Debug.Log ("Entrou");
				speed = Mathf.Lerp (speed, 0, smoothStop * Time.deltaTime);
			}
		} else {
			speed = 0;
			speedRotate = 0;
		}

		if (speed < 0.0001f && speed > -0.0001f)
			speed = 0;

		if (speedRotate < 0.0001 && speedRotate > -0.0001)
			speedRotate = 0;

		//transform.position += (transform.forward * speed * Time.deltaTime);
		//transform.rotation = Quaternion.Euler (transform.localEulerAngles + transform.up * speedRotate * Time.deltaTime);
	}
	void FixedUpdate(){
		transform.position += (transform.forward * speed * Time.deltaTime);
		transform.rotation = Quaternion.Euler (transform.localEulerAngles + transform.up * speedRotate * Time.deltaTime);
	}

}
