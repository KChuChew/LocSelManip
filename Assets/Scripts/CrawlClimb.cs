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
    private float timer = 0.0002f;
	[SerializeField] private Transform virtual_hand;
	[SerializeField] private Transform gogo_hand;

	void turn_player() {
		Vector3 rot = player.rotation.eulerAngles;
		rot.y -= (curr_rpos.x - prev_rpos.x) * 180;
		player.rotation = Quaternion.Euler (rot.x, rot.y, rot.z);
	}

	bool first_hand_position = true;
	Vector3 last_controller_position;
	//float distance_moved = 0f;
	float distance_threshold = 0.0f;
	Vector3 move_direction;
	float previous_distance = 0f;

	void get_speed() {
		Vector3 temp_right_pos = right_hand.position - player.position;
		temp_right_pos.y = 0;

		float current_distance = Vector3.Magnitude (temp_right_pos);
		/*Debug.Log ("current distance");
		Debug.Log (current_distance);
		Debug.Log ("previous distance");
		Debug.Log (previous_distance);*/
		//Debug.Log ("curr prev sub");
		//Debug.Log (1000*(previous_distance - current_distance));

		if (first_hand_position) {
			previous_distance = current_distance;
			move_direction = temp_right_pos.normalized;
			first_hand_position = false;
			return;
		}

		if(current_distance > previous_distance) {
			first_hand_position = true;
			return;
		}

		float move_distance = previous_distance - current_distance;
		Debug.Log (move_distance);
		//distance_moved += move_distance;

		if (move_distance > distance_threshold) {
			player.position += move_direction * 5f * (previous_distance - current_distance);
		}
		previous_distance = current_distance;;
	}

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
