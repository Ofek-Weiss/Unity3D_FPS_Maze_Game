using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// implement singletone
public class PersistentObjectManager : MonoBehaviour
{
    public static PersistentObjectManager instance = null;
    private static int gold = 0;
    public static bool isCoinActive = true;
    public static bool hasGunInHand = false;
    public GameObject player;
    public GameObject gunInHand;
    private static Vector3 SpawnPointPosition;// = new Vector3();
    public GameObject coin;
    public Text goldText;
    private void Awake() // runs before Start
    {
        // for the first time
        if (instance == null)
        {
            instance = this;
        }
        else // instance in not null
        {
            if(SceneManager.GetActiveScene().buildIndex == 0) // we enter scene 0
            {
                player.transform.position = SpawnPointPosition;
                player.transform.Rotate(new Vector3(0,-90,0));
            }
            Destroy(gameObject);
            if (hasGunInHand)
            {
                gunInHand.SetActive(true);
            }
            else
            {
                gunInHand.SetActive(false);
            }
        }
        DontDestroyOnLoad(gameObject); // in any case
        goldText.text = "Gold: " + 0; // update text
        coin.SetActive(isCoinActive);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetGold(int g)
    {
        gold = g;
    }
    public void setSpawnPointPos(Vector3 pos)
    {
        SpawnPointPosition = pos;
    }
}
