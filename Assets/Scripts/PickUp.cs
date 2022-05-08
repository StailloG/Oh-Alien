/*
 * Attach script to Player
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [Header("Variables")]
    public Interactions interactionsScript;
    public Transform hand; //player hands
    public GameObject notification;
    public GameObject cake;
    public GameObject key;
    public GameObject shed;

    [Header("Box Colliders")]
    public BoxCollider cakeBoxCol;

    [Header("Pick-up Item Bools")]
    public bool hasItemAlready = false;
    public bool canPickUpCake = false;
    public bool canPickUpKey = false;
    public bool canOpenShedDoor = false;

    [Header("Has Item Bools")]
    public bool hasCake = false;
    public bool hasKey = false;

    [Header("Interaction Bools")]
    public bool doorShedClosed = true;
    public Animator shedDoor;


    private void Start()
    {
        notification.SetActive(false);

        cakeBoxCol = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    public void Update()
    {
        //pickup items
        pickUpItem(ref canPickUpCake, ref hasCake, cake);
        pickUpItem(ref canPickUpKey, ref hasKey, key);

        //if player interacts with outhouse
        if (canOpenShedDoor == true && Input.GetKeyDown(KeyCode.Space) && hasItemAlready == false && interactionsScript.dogEatingCake == true)
        {
            //already opened door
            doorShedClosed = false;

            //open shed door animation
            shedDoor.Play("DoorOpening", 0);
            //shed.transform.localRotation = Quaternion.Euler(0, -73, -90);
            //shed.transform.localPosition = new Vector3(-1.46f, 1.47f, 4.69f);

            //cannot open shed door anymore
            canOpenShedDoor = false;
        }

        /* drop items */
        if (hasItemAlready == true && Input.GetKeyDown(KeyCode.E))
        {
            dropItem(ref hasCake, cake);
            dropItem(ref hasKey, key);
        }

        /* Disable notification when player has item */
        if (hasItemAlready == true)
        {
            //disable notification
            notification.SetActive(false);
        }

    }

    /* Pick up items method */
    public void pickUpItem(ref bool canPickUpItem, ref bool hasItem, GameObject item)
    {
        if(canPickUpItem == true && Input.GetKeyDown(KeyCode.Space) && hasItemAlready == false)
        {
            //has item
            hasItem = true;

            //pickup item
            item.transform.parent = hand.transform;
            item.transform.localPosition = Vector3.zero;

            //cannot pickup another item
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


    public void OnTriggerEnter(Collider other)
    {
        //if player collides with cake
        if (other.tag == "Cake")
        {
            canPickUpCake = true;
            notification.SetActive(true);

            //cannot pick up cake again if given to dog
            if(interactionsScript.cannotPickUpCake == true)
            {
                canPickUpCake = false;
                notification.SetActive(false);
            }
        }
        
        //if player collides with key
        if (other.tag == "Key")
        {
            canPickUpKey = true;
            notification.SetActive(true);
        }
        //if player collides with shed
        if (other.tag == "Shed")
        {
            canOpenShedDoor = true;
            
            if (interactionsScript.dogEatingCake == false)
            {
                notification.SetActive(false);
            }
            if (interactionsScript.dogEatingCake == true)
            {
                notification.SetActive(true);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        canPickUpCake = false;
        canPickUpKey = false;
        canOpenShedDoor = false;

        notification.SetActive(false);
    }

}
