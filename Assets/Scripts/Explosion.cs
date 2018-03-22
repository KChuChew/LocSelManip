using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	public float power = 100.0f;
	public float radius = 200.0f;
	public float upforce = 100.0f;
	bool explode = false;

	// Use this for initialization
	/*void Start () {
		
	}*/

	void Detonate() {
		Vector3 explosion_position = transform.position;
		Collider[] colliders = Physics.OverlapSphere (explosion_position, radius);
		foreach (Collider hit in colliders) {
			Rigidbody rb = hit.GetComponent<Rigidbody> ();
			if(rb != null)
				rb.AddExplosionForce (power, explosion_position, radius, upforce, ForceMode.Impulse);
		}
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag != "Untagged") {
			Debug.Log ("Explode");
			explode = true;
		} else {
			explode = false;
		}
	}

	void OnCollisionExit(Collision col) {
		explode = false;
	}
		
	// Update is called once per frame
	void Update () {
		if (explode) {
			Detonate ();
		}
	}
}
