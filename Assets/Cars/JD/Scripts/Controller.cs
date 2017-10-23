using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

	public float speedMoviment = 2.0f;
	public float speedRotation = 50.0f;
	public float distance = 0.5f;

	private RaycastHit hit;

	// Use this for initialization
	void Start () {
		Physics.Raycast (transform.position, -transform.up, out hit);
		transform.up = hit.normal;
	}
	
	// Update is called once per frame
	void Update () {

		if (Physics.Raycast (transform.position + new Vector3(0,1,0), -transform.up , out hit, distance)) {

			if (Input.GetKey (KeyCode.W)) {
				transform.position = transform.position + transform.forward * speedMoviment * Time.deltaTime;
			} else if (Input.GetKey (KeyCode.S)) {
				transform.position = transform.position - transform.forward * speedMoviment * Time.deltaTime;
			}

			if (Input.GetKey (KeyCode.D)) {
				transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles + transform.up * speedRotation * Time.deltaTime);
			} else if (Input.GetKey (KeyCode.A)) {
				transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles - transform.up * speedRotation * Time.deltaTime);
			}
		}

	}
		

}
