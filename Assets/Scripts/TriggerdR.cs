﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerdR : MonoBehaviour {
    public static bool triggered = false;
    public static Vector3 rContact;
	public GameObject gameobj_hit;
	public static RaycastHit hit;
	float gogo_distance = 0;
	public static bool hit_object_questionmark = false;


    // OPTIONAL
    [SerializeField] private Transform m_RealHand;
    [SerializeField] private Transform m_Body;

    // CORE
    [SerializeField] private float baseDistance = .2f;
    [SerializeField] private float criticalDist = .24f;
    [SerializeField] private float secondCriticalDist = .28f;

	[SerializeField] private Transform virtual_hand;

    private void Start() {
    }

    void OnTriggerEnter(Collider other) {
		//other.gameObject
		//Debug.Log(other.gameObject.tag);
		gameobj_hit = other.gameObject;

		//Debug.Log ("nyolo");
        /*if (!contact_point) {
            RaycastHit hit;
			if (Physics.Raycast (transform.position, transform.forward, out hit)) {
				if (hit.transform.tag != "player") {
					/*SpringJoint nyolo = transform.GetComponentInParent<SpringJoint> ();
					nyolo.connectedBody = hit.rigidbody;
					nyolo.anchor = transform.GetComponentInParent<Transform> ().position;
					nyolo.connectedAnchor = hit.point;
					rContact = hit.point;
					Debug.Log ("Colliding with OBJECT");
					dank = hit;
					nosing = false;
				}
			} else {
				nosing = true;
			}
        }
        triggered = true;
    	*/
	}

    void OnTriggerExit(Collider other) {
        triggered = false;
        rContact = Vector3.zero;
		//nosing = true;
    }

    void Update() {
		display_virtual_hand();
        GogoAdjust();   // OPTIONAL
    }

	void display_virtual_hand() {
		//Debug.Log (CrawlClimb.grabbing);
		if (!CrawlClimb.grabbing) {
			if (Physics.Raycast (transform.parent.position, transform.parent.forward, out hit, gogo_distance)) {
				virtual_hand.position = hit.point;
				Debug.Log (hit.transform.localPosition);
				hit_object_questionmark = true;
			} 
			else {
				virtual_hand.position = transform.position;
				hit_object_questionmark = false;
			}
		}
	}

    // OPTIONAL: adjust the hand position
    void GogoAdjust() {
        float distance = Vector3.Magnitude(m_RealHand.position - m_Body.position);
        //Debug.Log(distance);
		gogo_distance = GogoFunction(distance) + baseDistance;
		transform.localPosition = new Vector3(
            0f,
            0f,
            gogo_distance
        );
    }

    // CORE: get the adjusted position
    float GogoFunction(float distance) {
        if (distance < criticalDist)
            return 0;

        float firstDist = (distance - criticalDist) * 20f;
        //Debug.Log("firstdist " + firstDist);
        if (distance < secondCriticalDist)
            return firstDist;

        //Debug.Log("secondDist  " + (firstDist + (distance - secondCriticalDist) * 40f));
        return firstDist + (distance - secondCriticalDist) * 40f;
    }

}
