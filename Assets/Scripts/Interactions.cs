using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    [Header("GameObjects & Scripts")]
    public GameObject notification;
    public PickUp pickupScript;

    [Header("Dog Interaction Variables")]
    public bool canEatCake = false;
    public bool fedCakeToDog = false;
    public bool dogEatingCake = false;

    public void Start()
    {
        //TODO dog barking at player
    }

    public void Update()
    {
        /*
         * Player can feed dog the cake if:
         * - Player has cake
         * - Dog can eat cake
         * - Spacebar is pressed
         */
        if (pickupScript.hasCake == true && canEatCake == true && Input.GetKeyDown(KeyCode.Space))
        {
            //used on PickUp script to disable notification onTrigger
            fedCakeToDog = true;

            //drop cake & place next to dog
            pickupScript.cake.transform.parent = null;
            pickupScript.cake.transform.localPosition = new Vector3(-2.45f, 0.47f, 8.32f);

            //TODO animation of dog walking to cake
            //TODO dog eating cake animation

            //dog is distracted so player can now open outhouse!
            dogEatingCake = true;

            /*
             * Set player holding item and having cake to false
             * so that the player can open the outhouse door
             */
            //player cannot pick up cake
            pickupScript.canPickUpCake = false;

            //set cake trigger to false
            pickupScript.cakeBoxCol.isTrigger = false;

            //player doesn't have an item & cake anymore
            pickupScript.hasItemAlready = false;
            pickupScript.hasCake = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //if player has cake near dog
        if(other.tag == "Cake")
        {
            //dog can eat cake
            canEatCake = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        canEatCake = false;

        notification.SetActive(false);

    }
}
