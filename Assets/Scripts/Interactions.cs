using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    //variables
    public GameObject notification;
    public PickUp pickUpScript;

    [Header("Dog Interaction Variables")]
    public bool canEatCake = false;
    public bool cannotPickUpCake = false;
    public bool dogEatingCake = false;

    public void Start()
    {
        notification.SetActive(false);
    }

    public void Update()
    {
        //dog eating cake
        if(pickUpScript.hasCake == true && canEatCake == true && Input.GetKeyDown(KeyCode.Space) && gameObject.tag == "Dog")
        {
            cannotPickUpCake = true;
            //place cake in front of dog
            pickUpScript.cake.transform.parent = null;
            pickUpScript.cake.transform.localPosition = new Vector3(0f, 0.3f, 0f);
            pickUpScript.hasItemAlready = false;
            pickUpScript.canPickUpCake = false;
            pickUpScript.hasCake = false;
            pickUpScript.cakeBoxCol.isTrigger = false;
            //include dog eating cake animation

            //player can now open outhouse
            dogEatingCake = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //feed cake to dog
        if(other.tag == "Cake")
        {
            canEatCake = true;
            notification.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        canEatCake = false;
        notification.SetActive(true);

    }
}
