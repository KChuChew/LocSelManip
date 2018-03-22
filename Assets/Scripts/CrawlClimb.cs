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

	void turn_player() {
		Vector3 rot = player.rotation.eulerAngles;
		rot.y -= (curr_rpos.x - prev_rpos.x) * 180;
		player.rotation = Quaternion.Euler (rot.x, rot.y, rot.z);
	}

	/*bool right_hand_position_init = true;
	bool left_hand_position_init = true;
	Vector3 last_controller_position;
	float distance_threshold = 0.0f;
	Vector3 move_direction;
	float previous_right_distance = 0f;
	float previous_left_distance = 0f;

	void get_speed() {
		Vector3 temp_right_pos = right_hand.position - player.position;
		temp_right_pos.y = 0;
		Vector3 temp_left_pos = left_hand.position - player.position;
		temp_left_pos.y = 0;

		float current_right_distance = Vector3.Magnitude (temp_right_pos);
		float current_left_distance = Vector3.Magnitude (temp_left_pos);

		if (right_hand_position_init) {
			previous_right_distance = current_right_distance;
			move_direction = temp_right_pos.normalized;
			right_hand_position_init = false;
			left_hand_position_init = false;
			return;
		} 
		else if (left_hand_position_init) {
			previous_left_distance = current_left_distance;
			move_direction = temp_left_pos.normalized;
			left_hand_position_init = false;
			right_hand_position_init = false;
			return;
		}

		if (current_right_distance > previous_right_distance) {
			right_hand_position_init = true;
			return;
		} 
		else if (current_left_distance > previous_left_distance) {
			left_hand_position_init = true;
			return;
		}

		float move_right_distance = previous_right_distance - current_right_distance;
		float move_left_distance = previous_left_distance - current_left_distance;

		if (move_right_distance > distance_threshold) {
			player.position += move_direction * 5f * (previous_right_distance - current_right_distance);
		}
		else if(move_left_distance > distance_threshold) {
			//player.position += move_direction * 5f * (previous_left_distance - current_left_distance);
		}
		previous_right_distance = current_right_distance;
		previous_left_distance = current_left_distance;;
	}*/
	float previous_right_distance = 0f;
	float previous_left_distance = 0f;
	float distance_threshold = 0.0005f;

	void get_speed() {
		Vector3 temp_right_pos = right_hand.position - player.position;
		temp_right_pos.y = 0;
		Vector3 temp_left_pos = left_hand.position - player.position;
		temp_left_pos.y = 0;

		float current_right_distance = Vector3.Magnitude (temp_right_pos);
		float current_left_distance = Vector3.Magnitude (temp_left_pos);

		float move_right_distance = previous_right_distance - current_right_distance;
		float move_left_distance = previous_left_distance - current_left_distance;
		/*Debug.Log ("right dist");
		Debug.Log (move_right_distance * 10000);
		Debug.Log ("left dist");
		Debug.Log (move_left_distance * 10000);
		Debug.Log ("right prev");
		Debug.Log (previous_right_distance);
		Debug.Log ("right curr");
		Debug.Log (current_right_distance);
		Debug.Log ("left prev");
		Debug.Log (previous_left_distance);
		Debug.Log ("left curr");
		Debug.Log (current_left_distance);*/

		Vector3 move_r_direction = temp_right_pos.normalized;
		Vector3 move_l_direction = temp_left_pos.normalized;


		/*if (move_right_distance < 0 || move_left_distance < 0)
			return;*/

		if (move_right_distance > distance_threshold) {
			player.position += move_r_direction * 7f * (previous_right_distance - current_right_distance);
		}
		else if(move_left_distance > distance_threshold) {
			player.position += move_l_direction * 7f * (previous_left_distance - current_left_distance);
		}
		previous_right_distance = current_right_distance;
		previous_left_distance = current_left_distance;
	}
	/*void get_speed() {
		curr_rdisp = OVRInput.GetLocalControllerPosition (OVRInput.Controller.RTouch);
		curr_ldisp = OVRInput.GetLocalControllerPosition (OVRInput.Controller.LTouch);
		float movementr = curr_rdisp.z - prev_rdisp.z;
		float movementl = curr_ldisp.z - prev_ldisp.z;
		if (movementr < 0) {
			player.GetComponent<Rigidbody> ().AddForce (player.transform.forward * -movementr * 15, ForceMode.Impulse);
		}
		else if (movementl < 0) {
			player.GetComponent<Rigidbody> ().AddForce (player.transform.forward * -movementl * 15, ForceMode.Impulse);
		}

		prev_rdisp = curr_rdisp;
		prev_ldisp = curr_ldisp;
	}*/

	void Start () {
        prev_rpos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        curr_rpos = prev_rpos;
        prev_lpos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        curr_lpos = prev_lpos;

        prev_rhand = right_hand.transform.localPosition;
        prev_lhand = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);

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

	void Update () {
		determine_action();
	}
}
