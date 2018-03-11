using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerdR : MonoBehaviour {
    public static bool triggered = false;
    public static Vector3 rContact;
    bool contact_point = false;

    void OnTriggerEnter(Collider other) {
        if (!contact_point) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit)) {
                rContact = hit.point;
            }
        }
        triggered = true;
    }

    void OnTriggerExit(Collider other) {
        contact_point = false;
        triggered = false;
        rContact = Vector3.zero;
    }
}
