using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockedDoorMotion : MonoBehaviour
{
    Animator animator;
    AudioSource sound;
    private bool hasKey;
    public GameObject LockedText;
    public float delayTime = 1.0f; // Delay time in seconds before showing locked message

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // Connection to animator in Unity
        sound = GetComponent<AudioSource>();
        LockedText.SetActive(false);
        hasKey = false;
    }

    // Update is called once per frame
    void Update()
    {
        hasKey = BarTenderBehaviour.hasKey;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasKey == false)
        {
            LockedText.SetActive(true);
            //Invoke("ShowLockedMessage", delayTime); // Call ShowLockedMessage after a delay
        }
        else
        {
            animator.SetBool("OpenState", true);
            sound.PlayDelayed(0.8f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (hasKey)
        {
            animator.SetBool("OpenState", false);
            sound.PlayDelayed(0.8f);
        }
        else
        {
            LockedText.SetActive(false);
        }
    }

    void ShowLockedMessage()
    {
        LockedText.SetActive(true); // Method to show locked message
    }
}