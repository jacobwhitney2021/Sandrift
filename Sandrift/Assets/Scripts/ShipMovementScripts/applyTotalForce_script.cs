using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class applyTotalForce_script : MonoBehaviour
{
	private Rigidbody rigidbody;
    private GameObject sail;
	private Vector3 sailForce;
	private Vector3 rocketForce;
	public Vector3 totalForce;

	private Vector3 sailTorque;
	private Vector3 rudderTorque;
	public Vector3 totalTorque;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        sail = GameObject.FindGameObjectWithTag("Sail");
       }

    // Update is called once per frame
    void FixedUpdate()
    {
        sailForce = sail.GetComponent<windForce_script>().sailForce;
        rocketForce = gameObject.GetComponent<rocketForce_script>().rocketForce;
        totalForce = sailForce + rocketForce;
        
        sailTorque = sail.GetComponent<windForce_script>().sailTorque;
        rudderTorque = gameObject.GetComponent<rudder_script>().rudderTorque;
        totalTorque = rudderTorque + sailTorque;
       
        
        rigidbody.AddForce(totalForce);
        rigidbody.AddTorque(totalTorque);
    }
}
