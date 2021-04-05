using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rudder_script : MonoBehaviour
{
    public float torque;

    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float turn = Input.GetAxis("Horizontal");
        rigidbody.AddTorque(transform.up * torque * turn);
    }
}
