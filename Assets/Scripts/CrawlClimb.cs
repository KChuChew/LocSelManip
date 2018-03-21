using System.Collections;
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

	public static bool grabbing = false;
    private bool pause_action = false;
    private float timer = 0.0f;
	[SerializeField] private Transform virtual_hand;
	[SerializeField] private Transform gogo_hand;

	/*void grab_pull_object() {
		//Debug.Log (TriggerdR.hit_object_questionmark);
		if (TriggerdR.hit_object_questionmark) {
			gogo_hand.GetComponent<SpringJoint>().connectedBody = TriggerdR.hit.rigidbody;
			gogo_hand.GetComponent<SpringJoint>().connectedAnchor = TriggerdR.hit.transform.position - TriggerdR.hit.point;
			//gogo_hand.GetComponent<SpringJoint>().connectedAnchor = new Vector3(0, 0, 0);
		}
	}*/

	void turn_player() {
		Vector3 rot = player.rotation.eulerAngles;
		rot.y -= (curr_rpos.x - prev_rpos.x) * 180;
		player.rotation = Quaternion.Euler (rot.x, rot.y, rot.z);
		//camera.GetComponent<PostProcessingBehaviour>().enabled = true;
	}

	void get_speed() {
		//if (move) {
		curr_rdisp = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
		curr_ldisp = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);

		float movementr = curr_rdisp.z - prev_rdisp.z;
		float movementl = curr_ldisp.z - prev_ldisp.z;
		if (movementr < 0) {
			player.GetComponent<Rigidbody>().AddForce(player.transform.forward * -movementr * 15, ForceMode.Impulse);
		}
		else if (movementl < 0) {
			player.GetComponent<Rigidbody>().AddForce(player.transform.forward * -movementl * 15, ForceMode.Impulse);
		}

		prev_rdisp = curr_rdisp;
		prev_ldisp = curr_ldisp;
		//}
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
		determine_action();
    }
    
    void focus_FOV() {

        if(ActionController.turning) {
            transform.GetChild(1).GetComponent<PostProcessingBehaviour>().enabled = true;
        }
        else {
            transform.GetChild(1).GetComponent<PostProcessingBehaviour>().enabled = false;
        }
    }

	void determine_action() {
		curr_rpos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        focus_FOV();
        if(ActionController.grabbing) {
            pause_action = true;
        }
        else if(ActionController.turning) { 

			turn_player ();
            pause_action = true;
		} 
		else if(ActionController.running){
            if(pause_action) {
                if(timer <= 1.0f) {
                    timer += Time.deltaTime;
                }
                else {
                    pause_action = false;
                    timer = 0.0f;
                }
            }
            else {
                get_speed();
            }
		}
		prev_rpos = curr_rpos;
	}
}
