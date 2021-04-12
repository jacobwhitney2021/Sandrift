using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class windForce_script : MonoBehaviour
{
    private Transform craft_transform;
    private GameObject craft_gameobject;

    private float sailInputVertical;
    private float sailInputHorizontal;

    private float rotationTotal; 

    public float sailFullness; 

    public float sailPivotSpeed; // speed of rotation of the sail
    public float sailFullnessSpeed; // speed that the sail changed fullness

    private float fullnessChange;
    private float rotationChange;

    public Vector3 windVector;

    public Vector3 sailForce;
    public Vector3 sailTorque;
    

    // public const int MAXIMUM_WIND = 30;
    // public const int MAXIMUM_WIND_CHANGE = 2;

    // public Vector3 windGoal;

    // public const int MAX_FUEL = 1000;
    // private int fuel;

    // private System.Random rnd;

    void Start()
    {
        craft_transform = transform.parent.parent;
        craft_gameobject = craft_transform.gameObject;
    }

    void Update()
    {
        sailInputHorizontal = craft_gameobject.GetComponent<playerController_script>().sailInputHorizontal;
        sailInputVertical = craft_gameobject.GetComponent<playerController_script>().sailInputVertical;
        processInputs();
    }

    void FixedUpdate()
    {
        sailForce = calculateSailForce(windVector, craft_gameobject.GetComponent<Rigidbody>().velocity, transform.forward, sailFullness); 
        sailTorque = calculateSailTorque(windVector, craft_gameobject.GetComponent<Rigidbody>().velocity, transform.forward, sailFullness); 
    }

    void processInputs()
    {   
        float rotationChange = sailInputHorizontal * sailPivotSpeed * Time.deltaTime;
        if (rotationTotal + rotationChange < -90f)
        {
            rotationTotal = -90f;
        }
        else if (rotationTotal + rotationChange > 90f)
        {
            rotationTotal = 90f;
        }
        else
        {
            rotationTotal += rotationChange;
        }
        //float rotationClamped = Mathf.Clamp(rotationTotal,-90,90);
        transform.localRotation = Quaternion.Euler(0, rotationTotal, 0);



        fullnessChange = sailInputVertical * sailFullnessSpeed * Time.deltaTime;
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

    Vector3 calculateSailForce(Vector3 windVector, Vector3 playerVelocity, Vector3 sailVector, float sailFullness)
    {
        Vector3 apparentWindVector = windVector - playerVelocity;
        float craftWindAngle = Vector3.Angle(craft_transform.forward, apparentWindVector);
        float sailWindAngle = Vector3.Angle(sailVector, apparentWindVector);
        float sailCraftAngle = Vector3.Angle(sailVector, craft_transform.forward);

        Vector3 sailForceVector;
        if (((sailWindAngle <= 90f) || (sailWindAngle >= 270f))) { //&& ((sailCraftAngle <= 90f) || (sailCraftAngle >= 270f)))  ) {  
            sailForceVector = craft_transform.forward * apparentWindVector.magnitude * Mathf.Cos(sailWindAngle*(Mathf.PI/180f)) * Mathf.Cos(sailCraftAngle*(Mathf.PI/180f)) * sailFullness; // maybe make square of wind speed
        }
        else {
            sailForceVector = Vector3.zero;
        }
        return sailForceVector;
    }


    Vector3 calculateSailTorque(Vector3 windVector, Vector3 playerVelocity, Vector3 sailVector, float sailFullness)
    {
        Vector3 apparentWindVector = windVector - playerVelocity;
        float craftWindAngle = Vector3.Angle(craft_transform.forward, apparentWindVector);
        float sailWindAngle = Vector3.Angle(sailVector, apparentWindVector);
        float sailCraftAngle = Vector3.Angle(sailVector, craft_transform.forward);

        Vector3 sailTorque;
        if (craft_transform.rotation.z >= -40 || craft_transform.rotation.z <= 40) {
        sailTorque = -craft_transform.forward * apparentWindVector.magnitude * Mathf.Cos(sailWindAngle*(Mathf.PI/180f)) * Mathf.Sin(sailCraftAngle*(Mathf.PI/180f)) * sailFullness * 0.1f; // maybe make square of wind speed
        }
        else
            sailTorque = Vector3.zero;
        
        return sailTorque;
    }











    // void createWind()
    // {
    //     if (windGoal == windVector)
    //     {
    //         windGoal.x = rnd.Next(-1 * MAXIMUM_WIND, MAXIMUM_WIND);
    //         windGoal.z = rnd.Next(-1 * MAXIMUM_WIND, MAXIMUM_WIND);
    //     } else {
    //         windVector.x = applyWindOnAxis(windGoal.x, windVector.x);
    //         windVector.z = applyWindOnAxis(windGoal.z, windVector.z);

    //     }
    // }

    // float applyWindOnAxis(float goal, float curr)
    // {
    //     float diff = goal - curr;
    //     float change = rnd.Next(MAXIMUM_WIND_CHANGE);

    //     if (change > System.Math.Abs(diff))
    //     {
    //         return goal;
    //     }
    //     else if (diff > 0)
    //     {
    //         return curr + change;
    //     }
    //     else
    //     {
    //         return curr - change;
    //     }
    // }

    // void addTotalForce()
    // {
    //     // TODO calculate total forces
    //     Debug.Log("Rocket force");
    //     Debug.Log(rocketForce);

    //     Debug.Log("Sailwwwwwwwwwwwwwwwwwwwwwwwwwww force");
    //     Debug.Log(sailForce);
    //     rigidbody.AddForce(rocketForce + sailForce); 
    // }

    

    // Also create function to apply some kind of roll rotation if component of the wind vector is pushing into the sail/craft side on

}