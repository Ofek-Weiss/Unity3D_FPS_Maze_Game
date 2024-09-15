using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvivitBehaviour : MonoBehaviour
{
    Animator animator;
    public GameObject player; // connect in Unity
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance;

        distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance < 10)
        {
            // rotate NPC towards player
            Vector3 target_dir = (player.transform.position - transform.position);
            Vector3 new_dir = Vector3.RotateTowards(transform.forward,target_dir,3*Time.deltaTime,0);
            new_dir.y = 0;
            transform.rotation = Quaternion.LookRotation(new_dir);
            animator.SetInteger("State", 1);// talking
        }
        else
        {
            animator.SetInteger("State", 0);// idle
        }

    }
}
