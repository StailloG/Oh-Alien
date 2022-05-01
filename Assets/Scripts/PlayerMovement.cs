using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //player movement variables
    private float speed = 5;
    private float rotateSpeed = 3.0f;
    private float horizontalInput, verticalInput;
    public CharacterController playerController;

    // Update is called once per frame
    void Update()
    {
        movePlayer();
    }

    void movePlayer()
    {
        //call horizontal & vertical movements
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //rotate around y axis
        transform.Rotate(0, horizontalInput * rotateSpeed, 0);

        //move forwards + backwards
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * verticalInput;
        playerController.SimpleMove(forward * curSpeed);
    }
}
