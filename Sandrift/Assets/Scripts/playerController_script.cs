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

	public float turnSpeed = 5.0f;
    public float rawTorque;

    public float sailAngle = 0.0f;
	public float sailFullness = 0.0f; 

	public float sailPivotSpeed = 10.0f; // speed of rotation of the sail
	public float sailFullnessSpeed = 1.5f; // speed that the sail changed fullness

	public float fullnessChange;


    public int thrustChangePerInput = 2;

    private int forwardForce;

    public const string INCREASE_THRUST = "w";
    public const string DECREASE_THRUST = "s";
    public const string ROTATE_LEFT = "d";
    public const string ROTATE_RIGHT = "a";
    public const string NITRO_KEY = "n"; // TODO add nitro key

    void Start()
    {
        
    }


    void Update()
    {
        //updateRudder();
        updateSail();
        updateRocket();
    }

    // void updateRudder()
    // {
    //     rawTorque = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
    // }

    void updateRocket()
    {
        if (Input.GetKey(NITRO_KEY))
        {
            gameObject.GetComponent<windForce_script>().nitroOn = true;
        }

        if (Input.GetKey(INCREASE_THRUST))
        {
            int rt = gameObject.GetComponent<windForce_script>().rocketThrust;
            rt += thrustChangePerInput;
            if (rt > 100) rt = 100;

            gameObject.GetComponent<windForce_script>().rocketThrust = rt;
        }

        if (Input.GetKey(DECREASE_THRUST))
        {
            int rt = gameObject.GetComponent<windForce_script>().rocketThrust;
            rt -= thrustChangePerInput;
            if (rt < 0) rt = 0;
            gameObject.GetComponent<windForce_script>().rocketThrust = rt;
        }


    }

    void updateSail()
    {
        float rotation_change = Input.GetAxis("SailHorizontal") * sailPivotSpeed * Time.deltaTime;
        sailAngle += rotation_change;
        if (sailAngle < 0f)
        {
            sailAngle += 360f;
        }
        if (sailAngle > 360f)
        {
            sailAngle -= 360f;
        }


        fullnessChange = Input.GetAxis("SailVertical") * sailFullnessSpeed * Time.deltaTime;
        if (sailFullness + fullnessChange < 0)
        {
            sailFullness = 0f;
        }
        else if (sailFullness + fullnessChange > 1)
        {
            sailFullness = 1f;
        }
        else
        {
            sailFullness += fullnessChange;
        }
    }







}
