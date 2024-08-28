using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollPhysics : MonoBehaviour
{
    [SerializeField] private Rigidbody spenrigidbody;
    public float z = 100f;
    public float y = 100f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spenrigidbody.AddForce(new Vector3(0f, z, y));
        }
    }
}
