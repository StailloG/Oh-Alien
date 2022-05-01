using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //camera movement variables
    public Transform player;
    public Vector3 offset;
    public float xRotation = 0f;
    public float yRotation = 0f;
    


    // Start is called before the first frame update
    void Start()
    {
        //lock mouse to game
        Cursor.lockState = CursorLockMode.Locked;
    }


    // Update is called once per frame
    void Update()
    {
        //camera offset
        transform.position = player.position + offset;

        //camera clamps up/down & left/right
        yPos();
        xPos();
    }

    public void yPos()
    {
        float mouseY = Input.GetAxis("Mouse Y") * 100f * Time.deltaTime;

        //rotation is flipped
        xRotation -= mouseY;

        //clamp rotation to not be able to look too high up
        xRotation = Mathf.Clamp(xRotation, 17.091f, 31.238f);

        //rotation
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    public void xPos()
    {
        
    }
}
