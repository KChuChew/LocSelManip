using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour {

    [SerializeField] private Transform virtualhand;
    private bool was_grabbing = false;

    void grabObject() {
        SpringJoint to_attach = transform.GetComponent<SpringJoint>();

        if(ActionController.grabbing) {
            was_grabbing = true;
            if (HandTrigger.gameobj_hit != null) {
                GameObject obj_hit = HandTrigger.gameobj_hit;
                virtualhand.position = obj_hit.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                to_attach.connectedBody = obj_hit.GetComponent<Rigidbody>();
                to_attach.connectedAnchor = obj_hit.transform.InverseTransformPoint(obj_hit.GetComponent<Collider>().ClosestPointOnBounds(transform.position));
            }
            else {
                virtualhand.position = transform.position;
                to_attach.connectedBody = null;
            }
        }
        else {
            virtualhand.position = transform.position;
            to_attach.connectedBody = null;
        }
    }

    void FixedUpdate () {
        grabObject();
	}
}
