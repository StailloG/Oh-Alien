/*
 * Attach script to Player
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [Header("Gameobjects to Interact With")]
    public Transform hand;
    public GameObject notification;
    public GameObject cake;
    public GameObject key;

    [Header("Scripts")]
    public Interactions interactionsScript;

    [Header("Box Collider")]
    public BoxCollider cakeBoxCol;

    [Header("Can Pick-up Item Bools")]
    public bool hasItemAlready = false;
    public bool canPickUpCake = false;
    public bool canPickUpKey = false;
    public bool canOpenOuthouseDoor = false;
    public bool doorShedClosed = true; //DEBUGGING

    [Header("Has Item Bools")]
    public bool hasCake = false;
    public bool hasKey = false;

    [Header("Animation for GameObjects")]
    public Animator outhouseDoorOpening;


    private void Start()
    {
        notification.SetActive(false);

        //set trigger to false based off Interactions script
        cakeBoxCol = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    public void Update()
    {
        //player can pickup these items
        pickUpItem(ref canPickUpCake, ref hasCake, cake);
        pickUpItem(ref canPickUpKey, ref hasKey, key);

        //player can open outhouse
        outhouseNowOpen();

        /* drop items */
        if (hasItemAlready == true && Input.GetKeyDown(KeyCode.E))
        {
            dropItem(ref hasCake, cake);
            dropItem(ref hasKey, key);
        }

        /* Disable notification when player has item */
        if (hasItemAlready == true)
        {
            notification.SetActive(false);
        }

    }

    /* Pick up items method */
    public void pickUpItem(ref bool canPickUpItem, ref bool hasItem, GameObject item)
    {
        if(canPickUpItem == true && Input.GetKeyDown(KeyCode.Space) && hasItemAlready == false)
        {
            //player has an item
            hasItem = true;

            //pickup item
            item.transform.parent = hand.transform;
            item.transform.localPosition = Vector3.zero;

            //player has an item - cannot pickup another item
            hasItemAlready = true;

            //set pickup to false
            canPickUpItem = false;
        }
    }

    /* Drop items method */
    public void dropItem(ref bool hasItem, GameObject item)
    {
        if(hasItem == true)
        {
            item.transform.parent = null; //release object from hand
            hasItemAlready = false; //to be able to pick up another item
            hasItem = false; //does not have item anymore
        }
    }

    public void outhouseNowOpen()
    {
        //if player interacts with outhouse
        if (canOpenOuthouseDoor == true && Input.GetKeyDown(KeyCode.Space) && hasItemAlready == false && interactionsScript.dogEatingCake == true)
        {
            //already opened door
            doorShedClosed = false; //DEBUGGING

            //open shed door animation
            outhouseDoorOpening.Play("DoorOpening", 0);

            //cannot open shed door anymore
            canOpenOuthouseDoor = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //if player collides with cake
        if (other.tag == "Cake")
        {
            canPickUpCake = true;
            notification.SetActive(true);
        }
        
        //if player collides with key
        if (other.tag == "Key")
        {
            canPickUpKey = true;

            notification.SetActive(true);
        }

        //if player collides with outhouse
        if (other.tag == "Outhouse")
        {
            if (interactionsScript.dogEatingCake == false)
            {
                notification.SetActive(false);
            }

            if (interactionsScript.dogEatingCake == true)
            {
                canOpenOuthouseDoor = true;

                notification.SetActive(true);
            }
        }

    }

    public void OnTriggerExit(Collider other)
    {
        canPickUpCake = false;
        canPickUpKey = false;
        canOpenOuthouseDoor = false;

        notification.SetActive(false);
    }

}
