using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DrawerBehaviour : MonoBehaviour
{
    public GameObject Eye;
    public GameObject CrossHairRegular;
    public GameObject CrossHairTouch;
    public GameObject OpenText;
    public GameObject CloseText;
    public GameObject Chest;
    public List<GameObject> Monsters; // List of monster GameObjects
    private Animator animator; // Drawer's animator
    private AudioSource sound; // Audio source for drawer sounds
    bool isDrawerOpened = false;
    bool isDrawerOpenedOnce = false;
    public GameObject Gate;

    void Start()
    {
        animator = Chest.GetComponent<Animator>();
        sound = Chest.GetComponent<AudioSource>();
    }

    [System.Obsolete]
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Eye.transform.position, Eye.transform.forward, out hit))
        {
            if (hit.collider == GetComponent<Collider>())
            {
                CrossHairRegular.SetActive(false);
                CrossHairTouch.SetActive(true);
                OpenText.SetActive(!isDrawerOpened);
               // CloseText.SetActive(isDrawerOpened);

                if (Input.GetKeyDown(KeyCode.O))
                {
                    ToggleDrawer(true);
                }
               // else //if (Input.GetKeyDown(KeyCode.C))
              //  {
                   // ToggleDrawer(false);
                //}
            }
            else
            {
                CrossHairRegular.SetActive(true);
                CrossHairTouch.SetActive(false);
                OpenText.SetActive(false);
                CloseText.SetActive(false);
            }
        }

        if (isDrawerOpenedOnce)
        {
            UpdateMonstersFollowing();
            Gate.SetActive(false);
        }
    }

    private void ToggleDrawer(bool open)
    {
        animator.SetBool("OpenState", open);
        sound.PlayDelayed(open ? 1 : 1.5f);
        isDrawerOpened = open;
        isDrawerOpenedOnce = open; 
    }

    private void UpdateMonstersFollowing()
    {
        foreach (GameObject monster in Monsters)
        {
            NavMeshAgent agent = monster.GetComponent<NavMeshAgent>();
            Animator monsterAnimator = monster.GetComponent<Animator>();
            if (agent.enabled)
            {
                agent.SetDestination(Eye.transform.position);
                // Ensure the agent is actively moving towards the target
                if (!agent.isStopped && agent.remainingDistance > agent.stoppingDistance)
                {
                    monsterAnimator.SetInteger("State", 1); // Walking state
                }
                else
                {
                    monsterAnimator.SetInteger("State", 0); // Idle state
                }
            }
        }
    }

    public void MonsterShot(GameObject monster)
    {
        Animator monsterAnimator = monster.GetComponent<Animator>();
        if (monsterAnimator != null)
        {
            // Setting the integer parameter that controls the animation states
            monsterAnimator.SetInteger("State", 2);  // Assuming '2' is the 'die' state
            monster.GetComponent<NavMeshAgent>().enabled = false;  // Optionally stop movement

            Debug.Log("Dying animation triggered for: " + monster.name);
        }
        else
        {
            Debug.LogError("Animator not found on shot monster: " + monster.name);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            // Use the new recommended method
            DrawerBehaviour drawerScript = Object.FindAnyObjectByType<DrawerBehaviour>();
            if (drawerScript != null)
            {
                drawerScript.MonsterShot(collision.gameObject);
            }
            else
            {
                Debug.LogError("DrawerBehaviour script not found in scene!");
            }
        }
    }
}