using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Variables")]
    public Transform player;
    public Vector3 offset;
    public float rotationSpeed = 4.0f;
    private float mouseSensitivity = 100f;
    private float xUpDown = 0f;
   
    // Start is called before the first frame update
    void Start()
    {
        //lock mouse to game
        Cursor.lockState = CursorLockMode.Locked;

        //setting camera position
        //offset = new Vector3(player.position.x, player.position.y + 8.0f, player.position.z + 7.0f);

    }


    // Update is called once per frame
    void Update()
    {
        //look at player
        transform.LookAt(player.position);

        //rotateAroundPlayer();
        //upAndDown();
    }

    void rotateAroundPlayer()
    {
        //defining offset to move along x axis rotation around player
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up) * offset;

        //new camera position
        transform.position = player.position + offset;
    }

    void upAndDown()
    {
        //call unity's mouse input
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //up/down rotation is flipped
        xUpDown -= mouseY;

        //clamp
        xUpDown = Mathf.Clamp(xUpDown, 19.41f, 41f);

        //rotation
        transform.localRotation = Quaternion.Euler(xUpDown, 0f, 0f);
    }

}
