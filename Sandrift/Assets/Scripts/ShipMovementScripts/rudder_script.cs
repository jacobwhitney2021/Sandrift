using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rudder_script : MonoBehaviour
{
    private Rigidbody rigidbody;
    float rudderInputAxis;
    public float torque;
    public Vector3 rudderTorque;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        rudderInputAxis = gameObject.GetComponent<playerController_script>().rudderInputAxis;
    }

    void FixedUpdate()
    {
        rudderTorque = transform.up * rudderInputAxis * torque;
    }
}
