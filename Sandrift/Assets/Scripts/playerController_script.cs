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

	public float sailAngle = 0.0f;
	public float sailFullness = 0.0f; 

	public float sailPivotSpeed = 10.0f; // speed of rotation of the sail
	public float sailFullnessSpeed = 1.5f; // speed that the sail changed fullness

	public float fullnessChange;

    private int rocketThrust; // between 0 and 100
    public int thrustChangePerInput = 3;
    private bool nitroOn; // TODO: add cooldown for nitro // TODO cooldown does not finish
    public const int nitroThrust = 150;

    public const int MAX_FUEL = 1000;
    private int fuel;

    private int forwardForce;

    public const string INCREASE_THRUST = "w";
    public const string DECREASE_THRUST = "s";
    public const string ROTATE_LEFT = "d";
    public const string ROTATE_RIGHT = "a";
    public const string NITRO_KEY = "n"; // TODO add nitro key

    void Start()
    {
        rocketThrust = 10;
        nitroOn = false;
        fuel = MAX_FUEL;
        forwardForce = rocketThrust;
    }

    // for physics changes
    void FixedUpdate()
    {
        fixedUpdateRocket();

    }

    void Update()
    {
        updateSail();
        updateRocket();
    }

    void fixedUpdateRocket()
    {
        consumeFuel();

    }

    void updateRocket()
    {
        if (Input.GetKey(NITRO_KEY))
        {
            nitroOn = true;
        }

        if (Input.GetKey(INCREASE_THRUST))
        {
            rocketThrust += thrustChangePerInput;
            if (rocketThrust > 100) rocketThrust = 100;
        }

        if (Input.GetKey(DECREASE_THRUST))
        {
            rocketThrust -= thrustChangePerInput;
            if (rocketThrust < 0) rocketThrust = 0;
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

    void calculateForce()
    {
        int force = nitroOn ? nitroThrust : rocketThrust;
        return; // TODO
    }

    void consumeFuel()
    {
        int fuelConsumed = nitroOn ? (int) (nitroThrust * 1.5f) : rocketThrust; // TODO do we want the fuel function to be linear or not
        fuel -= fuelConsumed; 

    }



}
