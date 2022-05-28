using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuthouseInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Gameobjects")]
    public GameObject notification;

    [Header("Outhouse Bools")]
    public bool canOpenShedDoor = false;
    public bool doorShedClosed = true;

    [Header("Scripts")]
    public Interactions interactionsScript;
    public PickUp pickupScript;

    [Header("Outhouse Animation")]
    public Animator openDoor;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        OpenOuthouse();
    }

    public void OpenOuthouse()
    {
        //if player interacts with outhouse
        if (canOpenShedDoor == true && Input.GetKeyDown(KeyCode.Space) && pickupScript.hasItemAlready == false && interactionsScript.dogEatingCake == true)
        {
            //already opened door
            doorShedClosed = false;

            //open shed door animation
            openDoor.Play("DoorOpening", 0);

            //cannot open shed door anymore
            canOpenShedDoor = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if player collides with outhouse
        if (other.tag == "Player")
        {
            if (interactionsScript.dogEatingCake == false)
            {
                notification.SetActive(false);
            }

            if (interactionsScript.dogEatingCake == true)
            {
                canOpenShedDoor = true;

                notification.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canOpenShedDoor = false;
    }
}
