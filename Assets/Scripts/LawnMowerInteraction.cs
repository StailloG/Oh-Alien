using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawnMowerInteraction : MonoBehaviour
{
    [Header("Gameobjects & Scripts")]
    public PickUp pickupScript;

    [Header("Interaction Variables")]
    public bool canRide = false;

    // Update is called once per frame
    void Update()
    {
        if (pickupScript.hasKey == true && Input.GetKeyDown(KeyCode.Space) && canRide == true)
        {
            Debug.Log("Player can ride lawn mower!");

            //player doesn't have key anymore
            pickupScript.hasKey = false;
            
            //no more use for key
            Destroy(pickupScript.key);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //if player has key near lawnmower
        if (other.tag == "Player")
        {
            //player can ride lawnmower
            canRide = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        canRide = false;
    }
}
