using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour, IMovement
{
    [SerializeField] float mouseSensitivity = 500f;
    public Transform playerBody;
    float xRotation = 0f;

    protected bool m_canMove = true;
    
    public bool CanMove
    {
        get => m_canMove;
        set => m_canMove = value;
    }


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(!CanMove) return;

        if(Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
