using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;


public class BarTenderBehaviour : MonoBehaviour
{
    public Animator animator; // Animator component
    public GameObject Eye; // Eye GameObject to emit raycast from
    public GameObject Talktext; // UI for "Press E to interact" message
    public GameObject KeyIcon; // UI for key icon (visible when the key is given)
    public GameObject NotEnough; // UI for not enough coins warning
    public GameObject player; // Player GameObject
    public Collider BarmanCollider; // Collider to detect interaction with the bartender
    private int numCoins; // Stores the number of coins the player has
    public static bool hasKey = false; // Indicates if the player has the key
    public float notEnoughDisplayTime = 3.0f; // Display time for the Not Enough Coins message
    public Text collectedCoins; // Text to display the number of coins

    void Start()
    {
        Talktext.SetActive(false);
        KeyIcon.SetActive(false);
        NotEnough.SetActive(false);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        numCoins = CoinBehaviour.numCoins; // Update the coin count from the CoinBehaviour script

        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < 10)
        {
            Vector3 targetDirection = (player.transform.position - transform.position).normalized;
            targetDirection.y = 0;
            transform.rotation = Quaternion.LookRotation(targetDirection);
            animator.SetInteger("State", 1); // Waving
            if (!Talktext.activeInHierarchy && !NotEnough.activeInHierarchy)
            {
                Talktext.SetActive(true); // Show "Press E" only if no other message is active
            }
        }
        else
        {
            animator.SetInteger("State", 0); // Idle
            Talktext.SetActive(false);
            NotEnough.SetActive(false);
            KeyIcon.SetActive(false);
        }
        if (hasKey == true)
        {
            KeyIcon.SetActive(true);
        }

        HandleInteraction();
    }

    private void HandleInteraction()
    {
        if (!isPlayerCloseEnough()) return;

        RaycastHit hit;
        if (Physics.Raycast(Eye.transform.position, Eye.transform.forward, out hit) && hit.collider == BarmanCollider)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Talktext.SetActive(false); // Hide "Press E" immediately upon pressing 'E'

                if (numCoins >= 3 && !hasKey)
                {
                    KeyIcon.SetActive(true);
                    hasKey = true;
                    numCoins -= 3; // Deduct 3 coins
                    collectedCoins.text = "Gold: " + numCoins;
                }
                else if (!NotEnough.activeSelf) // Ensure not showing it redundantly
                {
                    NotEnough.SetActive(true);
                    Invoke("HideNotEnough", notEnoughDisplayTime); // Schedule hiding the message
                }
            }
        }
        else
        {
            Talktext.SetActive(false);
        }
    }

    void HideNotEnough()
    {
        NotEnough.SetActive(false);
    }

    private bool isPlayerCloseEnough()
    {
        return Vector3.Distance(transform.position, player.transform.position) < 10;
    }
}