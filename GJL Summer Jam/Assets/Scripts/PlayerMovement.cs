using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public KeyCode sprintKey = KeyCode.LeftShift;
    public float moveSpeed;
    public float runSpeedMultiplier;
    public float dashForceMultiplier;
    public float dashRate;
    private float prevDashTime;

    public float mouseSensitivity;
    private float xRotation = 0f;

    Rigidbody rb;
    Camera playerCam;

    
    void Start()
    {
        ToggleCursor(false);

        rb = GetComponent<Rigidbody>();
        playerCam = GetComponentInChildren<Camera>();


    }

    void Update()
    {
        MoveCheck();
        CameraCheck();
        DashCheck();
    }

    /// <summary>
    /// Gets input from mouse for rotating camera and player.
    /// </summary>
    private void CameraCheck()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //locks upward rotation to 75 degrees
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -75f, 75f);

        //rotates player left and right, camera up and down
        playerCam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

    }

    /// <summary>
    /// gets input from wasd for moving the player
    /// </summary>
    private void MoveCheck()
    {
        //gets input from wasd or arrow keys. if left shift is active it uses running speed if not regular speed
        float sideInput = Input.GetAxisRaw("Horizontal") * (Input.GetKey(sprintKey) ? moveSpeed * runSpeedMultiplier : moveSpeed);
        float forwardInput = Input.GetAxisRaw("Vertical") * (Input.GetKey(sprintKey) ? moveSpeed * runSpeedMultiplier : moveSpeed);

        Vector3 movePos = transform.right * sideInput + transform.forward * forwardInput; 
        rb.AddForce(movePos * Time.deltaTime,ForceMode.Acceleration);
    }

    /// <summary>
    /// Checks for dash input
    /// </summary>
    private void DashCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time - prevDashTime >= dashRate)
        {
            //store how much forward and sideways is inputted into a vector. 
            Vector3 dashDirection = transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical");

            //create the force and apply to rigidbody
            dashDirection *= dashForceMultiplier;
            rb.AddForce(dashDirection,ForceMode.Impulse);

            //store time of dash to limit to set rate
            prevDashTime = Time.time;
        }
    }

        /// <summary>
        /// toggles whether the cursor can move across screen and be seen.
        /// </summary>
        /// <param name="state"> true to allow it to move and be shown </param>
        public static void ToggleCursor(bool state)
    {
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = state;
    }


}
