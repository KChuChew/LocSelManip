using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerdR : MonoBehaviour {
    public static bool triggered = false;
    public static Vector3 rContact;
    bool contact_point = false;
    Vector3 shoulder_pos;
	public GameObject hit;

    // OPTIONAL
    [SerializeField] private Transform m_RealHand;
    [SerializeField] private Transform m_Body;

    // CORE
    [SerializeField] private float baseDistance = .2f;
    [SerializeField] private float criticalDist = .24f;
    [SerializeField] private float secondCriticalDist = .28f;

    private void Start() {
        shoulder_pos = m_Body.position;
        shoulder_pos.x += 0.2f;
    }

    void OnTriggerEnter(Collider other) {
		//other.gameObject
		Debug.Log(other.gameObject.tag);
		hit = other.gameObject;

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
        contact_point = false;
        triggered = false;
        rContact = Vector3.zero;
		nosing = true;
    }

    void Update() {
        GogoAdjust();   // OPTIONAL
    }

    // OPTIONAL: adjust the hand position
    void GogoAdjust() {
        float distance = Vector3.Magnitude(m_RealHand.position - m_Body.position);
        //Debug.Log(distance);
		transform.localPosition = new Vector3(
            0f,
            0f,
            GogoFunction(distance) + baseDistance
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
