using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class RayCast : MonoBehaviour {

    //public Material[] material;
    //Renderer rend;
    //private Color startColor;
    public float myTime = 0f;
    RaycastHit hit;
    GameObject curr_obj = null;
    GameObject prev_obj = null;
    /* materials for objects */
    public Material cursorOff;  // regular brick mat
    public Material cursorOn;  // highlight brick mat
    public Material reset;  // reset ball mat
    //Object[] materials;
    Vector3 prev_pos = new Vector3(0, 0, 0);
    Vector3 curr_pos = new Vector3(0, 0, 0);
    public Transform radial_bar;  // loading bar
    public Transform player;

    // convert vector 3 to int vector 3 for player teleportation
    Vector3 vfloat_to_vint(Vector3 to_convert) {
        Vector3 converted;
        converted.x = Mathf.RoundToInt(to_convert.x);
        converted.y = 8;
        converted.z = Mathf.RoundToInt(to_convert.z);
        return converted;
    }
    
    // Use this for initialization
    void Start () {
        //rend = GetComponent<Renderer>();
        //rend.enabled = true;
        //rend.sharedMaterial = material[1];

        //startColor = rend.material.color;
        //GetComponent<Renderer>().material = cursorOff;
        //materials = Resources.FindObjectsOfTypeAll(typeof(Material));
        radial_bar.GetComponent<Image>().fillAmount = myTime; 
        //Debug.Log(Resources.FindObjectsOfTypeAll(typeof(Material)).Length);
    }

    // Update is called once per frame
    void Update () {
        /* Vector3 fwd = transform.TransformDirection(Vector3.forward);
         RaycastHit hit;
         Ray ray = new Ray(transform.position, transform.forward);

         if (Physics.Raycast(ray, out hit)) {
             if (hit.collider.isTrigger)
                 Debug.Log("There is something in front of the object!");
         }*/
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        myTime += Time.deltaTime;
        if (Physics.Raycast(transform.position, fwd, out hit, 1000)) {

            //Debug.Log("There is something in front of the object!", hit.collider.attachedRigidbody);
            //Debug.Log(hit.collider.gameObject);
            if (hit.collider.gameObject.CompareTag("Brick")) {
                radial_bar.GetComponent<Image>().fillAmount = myTime / 2;

                curr_obj = hit.collider.gameObject;
                if (prev_obj != curr_obj) {
                    myTime = 0f;
                    radial_bar.GetComponent<Image>().fillAmount = myTime / 2;
                    if (prev_obj != null) {
                        if (prev_obj.gameObject.CompareTag("Reset")) {
                            prev_obj.GetComponent<Renderer>().material = reset;
                        }
                        else {
                            prev_obj.GetComponent<Renderer>().material = cursorOff;
                        }
                    }
                    //Debug.Log("look at diff obj");
                }
                else if (myTime >= 2f) {
                    //radial_bar.GetComponent<Image>().fillAmount = myTime;
                    //Debug.Log("looking at same obj for 3s");
                    curr_obj.GetComponent<Renderer>().material = cursorOn;
                    curr_obj.transform.Translate(Vector3.forward * Time.deltaTime);
                }

                prev_obj = curr_obj;
            }
            else if(hit.collider.gameObject.CompareTag("Reset")) {
                radial_bar.GetComponent<Image>().fillAmount = myTime / 2;

                curr_obj = hit.collider.gameObject;
                if (prev_obj != curr_obj) {
                    myTime = 0f;
                    radial_bar.GetComponent<Image>().fillAmount = myTime/ 2;
                    //Debug.Log("look at diff obj");
                }
                else if (myTime >= 2f) {
                    //Debug.Log("looking at same obj for 3s");
                    SceneManager.LoadScene("BrickScene");
                }
                prev_obj = curr_obj;
            }
            else if(hit.collider.gameObject.CompareTag("Terrain")) {
                //Debug.Log("HIT TERRAIN");
                radial_bar.GetComponent<Image>().fillAmount = myTime / 2;

                //curr_obj = hit.collider.gameObject;
                curr_pos = vfloat_to_vint(hit.point);
                if (prev_pos != curr_pos) {
                    myTime = 0f;
                    radial_bar.GetComponent<Image>().fillAmount = myTime / 2;
                    //Debug.Log("look at diff obj");
                }
                if (myTime >= 2f) {
                    //Debug.Log("looking at same obj for 3s");
                    //SceneManager.LoadScene("BrickScene");
                    //Debug.Log("LETS MOVE");
                    player.transform.position = curr_pos;
                }
                prev_pos = curr_pos;
            }
            //Debug.Log(hit.point);
            /*if(myTime >= 3f) {
                Debug.Log("There is something in front of the object!", hit.collider.attachedRigidbody);
                Debug.Log(hit.collider.attachedRigidbody);
            }*/
            //rend.material.color = Color.cyan;
        }
        else {
            myTime = 0f;
            //rend.material.color = startColor;
            radial_bar.GetComponent<Image>().fillAmount = myTime / 2;
        }

        if (Input.GetKeyDown("space")) {
            Debug.Log("space key was pressed");
            SceneManager.LoadScene("BrickScene");
        }
    }
}
