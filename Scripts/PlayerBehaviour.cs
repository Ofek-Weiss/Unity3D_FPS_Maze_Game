using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    CharacterController controller;
    float speed = 8;
    float angularSpeed = 50;
    public GameObject aCamera; // must be connected to some object in Unity
    AudioSource footStepSound;
    public Text openText;
    public GameObject drawer;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>(); // connect controller to component in Unity
        footStepSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float rotation_about_Y;
        float rotation_about_X;

        rotation_about_Y = Input.GetAxis("Mouse X")* angularSpeed * Time.deltaTime;
        transform.Rotate(new Vector3(0, rotation_about_Y, 0));
        rotation_about_X = Input.GetAxis("Mouse Y") * angularSpeed * Time.deltaTime;
        aCamera.transform.Rotate(new Vector3(-rotation_about_X,0,0));


        float dx = speed*Time.deltaTime, dz= speed* Time.deltaTime;
        // basic (primitive) motion
        //transform.Translate(new Vector3(0,0, 0.05f));

        dz *= Input.GetAxis("Vertical");// can be -1, 0 or 1
        dx *= Input.GetAxis("Horizontal");// can be -1, 0 or 1

        Vector3 motion = new Vector3(dx, -0.5f, dz); // in LOCAL coordinates
        motion = transform.TransformDirection(motion); // transforms coordinates to GLOBAL
        controller.Move(motion); // in GLOBAL coordinates
        if(dx!=0 || dz!=0)// play sound if the player is moving and if the sound is not played
        {
            if(!footStepSound.isPlaying)
            {
                footStepSound.Play();
            }
        }
        // check if the player looks at drawer
        RaycastHit hit;
        if (Physics.Raycast(aCamera.transform.position, aCamera.transform.forward, out hit) == true) 
        {
            // check if player looks on drawer
        }
    }
}
