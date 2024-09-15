using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class PortalBehaviour : MonoBehaviour
{
    public GameObject Fade;
    public GameObject SpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(StartSceneTransition());
    }

    IEnumerator StartSceneTransition() 
    {
        // start animation
        Animator anim = Fade.GetComponent<Animator>();
        anim.SetBool("StartFadeIn", true);

        // wait some seconds
        yield return new WaitForSeconds(3);

        // now start scene transition

        PersistentObjectManager.instance.SetGold(CoinBehaviour.numCoins);
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            PersistentObjectManager.instance.setSpawnPointPos(SpawnPoint.transform.position);
            SceneManager.LoadScene(1);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(0);
        }
    }
}
