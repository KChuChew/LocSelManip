using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour {

    [SerializeField] private Transform virtualhand;
    private bool was_grabbing = false;
    //[SerializeField] private Transform gogohand;
    //[SerializeField] 
    
    // Use this for initialization
    /*void Start () {
		
	}*/

    void grabObject() {
        SpringJoint to_attach = transform.GetComponent<SpringJoint>();
        /*if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) >= 0.95 &&
            OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) >= 0.95) {*/
        if(ActionController.grabbing) {
            was_grabbing = true;
            if (HandTrigger.gameobj_hit != null) {
                //Debug.Log("Detecting Obj Hit");
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
            /*if(was_grabbing) {
                was_grabbing = false;
                Debug.Log("before grab");
                Debug.Log("after grab");
            }*/
            //Debug.Log("default");
            virtualhand.position = transform.position;
            to_attach.connectedBody = null;
        }
    }
	// Update is called once per frame
	void FixedUpdate () {
        grabObject();
	}
}
