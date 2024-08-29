using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float strafeSpeed;
    public float jumpForce;

    public Rigidbody hips;
    public bool isGrounded;

    private void Start()
    {
        hips = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.W))
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                hips.AddForce(hips.transform.forward * speed * 1.5f);
            }
            else
            {
                hips.AddForce(hips.transform.forward * speed);
            }

        }

        if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                hips.AddForce(-hips.transform.right * speed * 1.5f);
            }
            else
            {
                hips.AddForce(-hips.transform.right * speed);
            }

        }

        if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                hips.AddForce(-hips.transform.forward * speed * 1.5f);
            }
            else
            {
                hips.AddForce(-hips.transform.forward * speed);
            }

        }

        if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                hips.AddForce(hips.transform.right * speed * 1.5f);
            }
            else
            {
                hips.AddForce(hips.transform.right * speed);
            }

        }
    }
}
