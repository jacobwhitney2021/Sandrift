using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController_script : MonoBehaviour
{
	// Preliminary controls proposal:

	// the way im thinking of mapping controls is that WASD is non-wind/rocket controls and UpDownLeftRight keys are for the sail controls
	// so A and D will control the rotation of the craft and W and S will increase/decrease rocket thrust
	// left and right will rotate the sail and up and down will raise/lower the sail    

	// whoever works on this next, feel free to change any of this to make it feel better

    public const string NITRO_KEY = "space";

    public float sailInputVertical;
    public float sailInputHorizontal;
    public float rudderInputAxis;
    public float rocketInputAxis;
    public bool nitroInput;


    void Start()
    {

        Debug.Log("jerer");
        Debug.Log(GetComponent<Collider>().name);
    }


    void Update()
    {
        sailInputVertical = Input.GetAxis("SailVertical");
        sailInputHorizontal = Input.GetAxis("SailHorizontal");
        rudderInputAxis = Input.GetAxis("Horizontal");
        rocketInputAxis = Input.GetAxis("RocketVertical");
        nitroInput = Input.GetKey(NITRO_KEY);
    }

    // void updateRudder()
    // {
    //     rawTorque = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
    // }

    // void updateRocket()
    // {
    //     if (Input.GetKey(NITRO_KEY))
    //     {
    //         gameObject.GetComponent<windForce_script>().nitroOn = true;
    //     }

    //     if (Input.GetKey(INCREASE_THRUST))
    //     {
    //         int rt = gameObject.GetComponent<windForce_script>().rocketThrust;
    //         rt += thrustChangePerInput;
    //         if (rt > MAX_THRUST) rt = MAX_THRUST;

    //         gameObject.GetComponent<windForce_script>().rocketThrust = rt;
    //     }

    //     if (Input.GetKey(DECREASE_THRUST))
    //     {
    //         int rt = gameObject.GetComponent<windForce_script>().rocketThrust;
    //         rt -= thrustChangePerInput;
    //         if (rt < 0) rt = 0;
    //         gameObject.GetComponent<windForce_script>().rocketThrust = rt;
    //     }


    // }








}
