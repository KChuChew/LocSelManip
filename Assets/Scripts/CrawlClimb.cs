﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class CrawlClimb : MonoBehaviour {

    public Transform left_hand;
    public Transform right_hand;
    public Transform player;
    public Transform center_eye;
    public Transform camera;

    Vector3 prev_lpos;
    Vector3 prev_rpos;
    Vector3 curr_lpos;
    Vector3 curr_rpos;

    Vector3 prev_ldisp;
    Vector3 prev_rdisp;
    Vector3 curr_ldisp;
    Vector3 curr_rdisp;

    Vector3 prev_lhand;
    Vector3 curr_lhand;
    Vector3 prev_rhand;
    Vector3 curr_rhand;

    void get_speed(bool move) {
        if (move) {
            curr_rdisp = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
            curr_ldisp = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);

            float movementr = curr_rdisp.z - prev_rdisp.z;
            float movementl = curr_ldisp.z - prev_ldisp.z;
            if (movementr < 0) {
                player.GetComponent<Rigidbody>().AddForce(player.transform.forward * -movementr * 10, ForceMode.Impulse);
            }
            else if (movementl < 0) {
                player.GetComponent<Rigidbody>().AddForce(player.transform.forward * -movementl * 10, ForceMode.Impulse);
            }

            prev_rdisp = curr_rdisp;
            prev_ldisp = curr_ldisp;
        }
    }

	void Start () {
        prev_rpos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        curr_rpos = prev_rpos;
        prev_lpos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        curr_lpos = prev_lpos;

        prev_rhand = right_hand.transform.localPosition;
        prev_lhand = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
    }
	
	void FixedUpdate () {

        /* HAND VELOCITY MOVEMENT */
        get_speed(true);

        curr_rpos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);

        /* TURN PLAYER */
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) >= 0.95 &&
            OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) == 0) {

            Vector3 rot = player.rotation.eulerAngles;
            rot.y -= (curr_rpos.x - prev_rpos.x) * 180;
            player.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);
            camera.GetComponent<PostProcessingBehaviour>().enabled = true;
        }
        else {
            camera.GetComponent<PostProcessingBehaviour>().enabled = false;
        }

        /* FOLLOWING IS FOR CLIMBING CLIMBING CLIMBING CLIMBING CLIMBING CLIMBING CLIMBING CLIMBING CLIMBING CLIMBING CLIMBING CLIMBING CLIMBING */
        /*
        Vector3 rvel = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
        
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) >= 0.95 && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) >= 0.95) {
            if (TriggerdL.triggered) {
                //player.GetComponent<Collider>().isTrigger = true;
                //Debug.Log("Triggered Left Hand");
                //left_hand.position = TriggerdL.lContact;
                Debug.Log(prev_lhand - OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch));
                player.GetComponent<Rigidbody>().useGravity = false;
                player.GetComponent<Rigidbody>().isKinematic = true;

                player.transform.position += prev_lhand - OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
                //Debug.Log(OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch));
            }
        }
        else if(OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) >= 0.95 && OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) >= 0.95) {
            if(TriggerdR.triggered) {
                //player.GetComponent<Collider>().isTrigger = true;
                //Debug.Log("Triggerd Right Hand");
                player.GetComponent<Rigidbody>().useGravity = false;
                player.GetComponent<Rigidbody>().isKinematic = true;
                //right_hand.position = TriggerdR.rContact;
                player.transform.position += prev_rhand - OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);

                //OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
                //player.transform.position += OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);

            }
        }
        else {
            player.GetComponent<Rigidbody>().useGravity = true;
            player.GetComponent<Rigidbody>().isKinematic = false;
        }
        prev_lhand = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        prev_rhand = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        */
        prev_lpos = curr_lpos;
        prev_rpos = curr_rpos;
    }
}
