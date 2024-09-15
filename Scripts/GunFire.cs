using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class GunFire : MonoBehaviour
{
    public GameObject Eye;
    public GameObject target;
    public GameObject firePoint;
    public GameObject Monster;
    AudioSource shootingSound;
    LineRenderer line;
    public ParticleSystem muzzleFlash;
    public Animator animator;
    public GameObject gunIcon;
    // Start is called before the first frame update
    private float fireRate = 0.5f; // Time in seconds between shots
    private float nextFireTime = 0f; // Time when the next shot can be fired
    void Start()
    {
        shootingSound = GetComponent<AudioSource>();
        line = GetComponent<LineRenderer>();
        gunIcon.SetActive(true);
    }

    // Update is called once per frame

    void Update()
    {
        animator.SetInteger("State", 0);
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            RaycastHit hit;
            if (Physics.Raycast(Eye.transform.position, Eye.transform.forward, out hit))
            {
                target.transform.position = hit.point;
                StartCoroutine(Fire());

                // Check if hit object is a monster and trigger the Die method
                MonsterBehaviour monsterBehaviour = hit.collider.GetComponent<MonsterBehaviour>();
                if (monsterBehaviour != null)
                {
                    monsterBehaviour.Die();
                }
                muzzleFlash.Play();
                shootingSound.Play();
                animator.SetInteger("State", 1);
            }
        }
    }

    private void SleepTimeout(int v)
    {
        throw new NotImplementedException();
    }

    IEnumerator Fire()
    {
        line.enabled = true;
        line.SetPosition(0, firePoint.transform.position);
        line.SetPosition(1, target.transform.position);
        yield return new WaitForSeconds(0.02f);
        line.enabled = false;
    }
}
