using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmooth : MonoBehaviour {

	public Transform target;
	public Transform cam;

	public float distance = 23f;
	public float angle = 11.5f;
	public float hightAbove = 5f;

	private float hightGround = 0f;
	private float minDistanceGround = 5f;

	private float scrollWheel = 0f;
	private float scrollSensibility = 5f;

	private float mouseX = 0f;
	private float mouseXSensibility = 2f;

	private bool autoControl = false;

	private RaycastHit hitGround;

	void Start () {
	}
	

	void Update () {

		//Recupera valor do scroll
		scrollWheel = Input.GetAxis ("Mouse ScrollWheel");
		//Recupera valor de arrastar o mouse
		mouseX = Input.GetAxis ("Mouse X");

		//Seta a variavel para verificar o controle da camera
		if (Input.GetKey (KeyCode.LeftShift)) {
			autoControl = true;		
		} else {
			autoControl = false;
		}

	}

	void FixedUpdate () {
		float tLerp = 0.01f;
		//Atualiza posição do Objeto
		transform.position = target.position + Vector3.up * hightAbove;
		if (!autoControl) {
			transform.rotation = Quaternion.Euler (0, target.eulerAngles.y, 0);
			tLerp = 0.01f;
		} else {
			transform.rotation = Quaternion.Euler (0, transform.eulerAngles.y + mouseX*mouseXSensibility, 0);
			tLerp = 0.05f;
		}

		//Verifica a distancia do chão
		if (Physics.Raycast (cam.position, -Vector3.up, out hitGround, minDistanceGround)) {
			hightGround = 5f - hitGround.distance;
		}

		//Atualiza posição da camera
		cam.position = Vector3.Lerp (cam.position, 
			-(transform.forward * Mathf.Cos (angle * Mathf.Deg2Rad) * distance)
			+ (transform.up * ((Mathf.Sin (angle * Mathf.Deg2Rad) * distance) + hightGround))
			+ transform.position,
			tLerp);
		cam.LookAt (transform);
	}

	void LateUpdate(){
		//Atualiza a distancia da camera
		float scroll = distance - scrollWheel * scrollSensibility;
		distance = Mathf.Clamp (scroll, 10, 40);
		angle = distance / 2;
	}
}
