using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GogoHand : MonoBehaviour {

	// OPTIONAL
	[SerializeField] private Transform m_RealHand;
	[SerializeField] private Transform m_Body;

	// CORE
	[SerializeField] private float baseDistance = .2f;
	[SerializeField] private float criticalDist = 1.44f;
	[SerializeField] private float secondCriticalDist = 1.52f;

	void Start () {

	}

	void Update () {
		GogoAdjust ();	// OPTIONAL
	}

	// OPTIONAL: adjust the hand position
	void GogoAdjust () {
		float distance = Vector3.Magnitude(m_RealHand.position -  m_Body.position);
		transform.localPosition = new Vector3 (
			0f,
			0f,
			GogoFunction(distance) + baseDistance
		);
	}

	// CORE: get the adjusted position
	float GogoFunction (float distance) {
		if (distance < criticalDist)
			return 0;

		float firstDist = (distance - criticalDist) * 20f;
		if (distance < secondCriticalDist)
			return firstDist;

		return firstDist + (distance - secondCriticalDist) * 40f;
	}
}