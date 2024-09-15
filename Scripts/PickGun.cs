using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickGun : MonoBehaviour
{
    public GameObject gunInBox;
    public GameObject gunInHand;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        gunInBox.SetActive(false);
        gunInHand.SetActive(true);
        PersistentObjectManager.hasGunInHand = true;
    }
}
