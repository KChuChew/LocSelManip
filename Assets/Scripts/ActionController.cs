using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour {

    //OVRInput.Controller.RTouch;
    public static bool grabbing = false;
    public static bool turning = false;
    public static bool running = false;
    /*void Start () {
		
	}*/
	
	// Update is called once per frame
	void FixedUpdate () {
        if(OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) >= 0.95 &&
           OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) >= 0.95) {

            grabbing = true;
            turning = false;
            running = false;
        }
        else if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) >= 0.95 &&
                 OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) == 0) {
            grabbing = false;
            turning = true;
            running = false;
        }
        else if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) == 0 &&
                 OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) == 0) {
            grabbing = false;
            turning = false;
            running = true;
        }
    }
}
