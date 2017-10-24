using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmooth : MonoBehaviour {

	private const float MAX_DISTANCE = 40f;
	private const float MIN_DISTANCE = 5f;

	public Transform target;
	public Transform cam;

	public float distance = 23f;
	public float angle = 11.5f;
	public float hightAbove = 5f;

	private float hightGround = 0f;
	private float minDistanceGround = 5f;

	private float scrollWheel = 0f;
	private float scrollSensibility = 10f;

	private float mouseX = 0f;
	private float mouseXSensibility = 2f;

	private bool autoControl = false;

	private RaycastHit hitGround;
	private RaycastHit hitObstacle;

	public float approach = 0f;

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
		RaycastHit hitGround;
		RaycastHit hitObstacle;
		float tLerp = 0.02f;
		//Atualiza posição do Objeto
		transform.position = target.position + Vector3.up * hightAbove;
		if (!autoControl) {
			transform.rotation = Quaternion.Euler (0, target.eulerAngles.y, 0);
			tLerp = 0.02f;
		} else {
			transform.rotation = Quaternion.Euler (0, transform.eulerAngles.y + mouseX*mouseXSensibility, 0);
			tLerp = 0.05f;
		}

		//Verifica a distancia do chão
		if (Physics.Raycast (cam.position, -Vector3.up, out hitGround, minDistanceGround)) {
			hightGround = 5f - hitGround.distance;
		}

		//Verifica colisão com as paredes
		if (Physics.Raycast (cam.position - cam.forward * 2, target.position - cam.position, out hitObstacle, distance)) {
			if (hitObstacle.collider.tag.Equals ("Tree")
			    || hitObstacle.collider.tag.Equals ("Wall")) {
				approach++;
				approach = Mathf.Min (approach, MAX_DISTANCE);
			} else if (Physics.Raycast (target.position + target.up * 2f, cam.position - target.position, out hitObstacle, distance)) {
				if (!hitObstacle.collider.tag.Equals ("Tree")
					&& !hitObstacle.collider.tag.Equals ("Wall")) {
					approach--;
					approach = Mathf.Max (approach, 0);
				}
			} else {
				approach--;
				approach = Mathf.Max (approach, 0);
			}
		}

		//Atualiza posição da camera
		cam.position = Vector3.Lerp (cam.position, 
			-(transform.forward * Mathf.Cos (angle * Mathf.Deg2Rad) * Mathf.Max(distance - approach, MIN_DISTANCE))
			+ (transform.up * ((Mathf.Sin (angle * Mathf.Deg2Rad) * Mathf.Max(distance - approach, MIN_DISTANCE)) + hightGround))
			+ transform.position,
			tLerp);
		cam.LookAt (transform);
	}

	void LateUpdate(){
		//Atualiza a distancia da camera
		float scroll = distance - scrollWheel * scrollSensibility;
		distance = Mathf.Clamp (scroll, MIN_DISTANCE, MAX_DISTANCE);
		angle = distance / 2;
	}
}
