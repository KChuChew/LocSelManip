using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTrigger : MonoBehaviour {

    public static Vector3 rContact;
	public static GameObject gameobj_hit;
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
		gameobj_hit = other.gameObject;
	}

    void OnTriggerExit(Collider other) {
        rContact = Vector3.zero;
        gameobj_hit = null;
    }

    void Update() {
        GogoAdjust();   // OPTIONAL
    }

    // OPTIONAL: adjust the hand position
    void GogoAdjust() {
        float distance = Vector3.Magnitude(m_RealHand.position - m_Body.position);
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

        float firstDist = (distance - criticalDist) * 30f;
        if (distance < secondCriticalDist)
            return firstDist;

        return firstDist + (distance - secondCriticalDist) * 60f;
    }

}
