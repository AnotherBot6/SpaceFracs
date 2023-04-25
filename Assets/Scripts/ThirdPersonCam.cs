using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;
    public float rSpeed;

    public Transform combatLookAt;
    public CameraStyle currentStyle;

    public enum CameraStyle
    {
        Basic,
        Combat,
        TopDown
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible= false;
    }

    // Update is called once per frame
    void Update()
    {
        //rotate orientation

        Vector3 dir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward= dir.normalized;

        //rotate player 
        if(currentStyle == CameraStyle.Basic)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
            if (inputDir != Vector3.zero)
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, rSpeed * Time.deltaTime);
        }
        else if(currentStyle== CameraStyle.Combat || currentStyle == CameraStyle.TopDown)
        {
            Vector3 dirCombat = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            orientation.forward = dirCombat.normalized;
            playerObj.forward = dirCombat.normalized; 
        }
      
    }
}
