using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinBehaviour : MonoBehaviour
{
    public static int numCoins = 0;
    public GameObject coins;
    public Text collectedCoins;
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
        numCoins++;
        collectedCoins.text = "Gold: " + numCoins;

        if(gameObject == PersistentObjectManager.instance.coin.gameObject)
        {
            PersistentObjectManager.isCoinActive = false;
        }

        gameObject.SetActive(false);
        AudioSource sound = coins.GetComponent<AudioSource>();
        sound.Play();
    }
}
