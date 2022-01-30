using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class GhostMovement : MonoBehaviour
/*
{
    [SerializeField] float speed;

    void Start()
    {
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float y = Input.GetAxis("YVertical");

        CharacterMovement(x, z, y);
    }

    void CharacterMovement(float x, float z, float y)
    {
        Vector3 move = transform.right * x + transform.forward * z + transform.up * y;
        transform.position += move * speed * Time.deltaTime;
    }
}*/

{
    [SerializeField] float speed;
    private CharacterController controller;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float y = Input.GetAxis("YVertical");

        CharacterMovement(x, z, y);
    }

    void CharacterMovement(float x, float z, float y)
    {
        Vector3 move = transform.right * x + transform.forward * z + transform.up * y;
        //transform.position += move * speed * Time.deltaTime;
        controller.Move(move * speed * Time.deltaTime);
    }
}