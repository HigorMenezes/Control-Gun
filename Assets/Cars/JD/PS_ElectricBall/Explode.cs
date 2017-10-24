using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour {

	public GameObject explode;

	private GameObject aux;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision){
		if (collision.collider.tag.Equals("Ground")
			|| collision.collider.tag.Equals("JD")
			|| collision.collider.tag.Equals("Tree")) {
			aux = Instantiate (explode, transform.position, Quaternion.Euler(0f,0f,0f));
			Destroy (aux, 5f);
			Destroy (this.gameObject);
		}
	}

}
