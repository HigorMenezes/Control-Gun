using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fire : MonoBehaviour {
	

	private const float MAX_POWER = 70f;

	public Slider chargerBar;
	public float power { get; set; }

	public float chargeSpeed = 15f;
	public GameObject prepareShot;
	public GameObject shot;
	public GameObject explode;

	private GameObject particleDisponse;

	private bool charge = false;
	private GameObject cannon;

	private bool pulseButon = true;

	// Use this for initialization
	void Start () {
		cannon = transform.GetChild(2).gameObject;
		power = 0f;
		chargerBar.value = calculateChargerBar();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Space) && pulseButon) {
			pulseButon = false;
			if (charge) {
				//Atirar
				shoot ();
			}else {
				charge = true;
				activePrepare (true);
			}
		} 

		if (Input.GetKeyUp (KeyCode.Space))
			pulseButon = true;
		
		if (Input.GetKey (KeyCode.Escape)) {
			charge = false;
			power = 0;
			activePrepare (false);
		}
	}

	void FixedUpdate () {
		if ( charge ) {
			//Carregando
			power = Mathf.Min( power + chargeSpeed * Time.deltaTime, MAX_POWER);
			if (power == MAX_POWER)
				shoot ();
		}
		chargerBar.value = calculateChargerBar();
	}

	void activePrepare(bool b){
		cannon.transform.GetChild (0).gameObject.SetActive (b);
	}

	float calculateChargerBar(){
		return power / MAX_POWER;
	}

	void shoot(){
		charge = false;
		activePrepare (false);
		particleDisponse = Instantiate (shot, cannon.transform);

		Rigidbody rb = particleDisponse.GetComponent<Rigidbody> ();
		rb.AddForce(cannon.transform.forward * power, ForceMode.Impulse);

		Destroy (particleDisponse, 20f);

	}
}
