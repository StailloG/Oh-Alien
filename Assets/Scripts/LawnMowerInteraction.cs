using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LawnMowerInteraction : MonoBehaviour
{
    [Header("Gameobjects & Scripts")]
    public PickUp pickupScript;
    public TextMeshProUGUI needKeyText;
    public Transform lawnmower;
    public Animator movingAnim;

    [Header("Interaction Variables")]
    [SerializeField] private bool canRide = false;
    [SerializeField] private bool needKey = false;
    

    private void Start()
    {
        needKeyText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //tell player they need a key to turn on the lawn mower
        if (pickupScript.hasKey == false && needKey == true && Input.GetKeyDown(KeyCode.Space))
        {
            needKeyText.gameObject.SetActive(true);
        }

        //once player has the key to turn on lawn mower
        if (pickupScript.hasKey == true && Input.GetKeyDown(KeyCode.Space) && canRide == true)
        {
            Debug.Log("Player can ride lawn mower!");

            //player doesn't have key anymore
            pickupScript.hasKey = false;
            
            //no more use for key
            Destroy(pickupScript.key);

            //call lawnmower moving method
            //lawnMowerMoving();
            movingAnim.Play("LawnMowerMoving", 0);
        }
    }

    /*
     * Lawn mower will travel all across the map before
     * eventually crashing into the lake.
     * 
     * This is where oil will then spool out into the lake
     * & fishes will come up!
     */
    public void lawnMowerMoving()
    {
        

    }

    public void OnTriggerEnter(Collider other)
    {
        //if player has key near lawnmower
        if (other.tag == "Player")
        {
            //player can ride lawnmower
            canRide = true;

            //player before having key
            needKey = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        canRide = false;
        needKey = false;
        needKeyText.gameObject.SetActive(false);
    }
}
