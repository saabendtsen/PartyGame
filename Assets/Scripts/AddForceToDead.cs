using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceToDead : MonoBehaviour
{
    private Rigidbody rb;

    public float force = 1000f;
  

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rb.AddForce(-transform.forward * force);
    }

}
