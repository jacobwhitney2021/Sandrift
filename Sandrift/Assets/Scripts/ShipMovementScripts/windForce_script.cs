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
    public float magnitude;
    public float minMagnitude;
    public float maxMagnitude;
    public const float maxWind = 15;
    private int windState;
    private const int windStateMax = 60;
    private const int perSecond = 30;

    private const int windChangeDur = 15;

    public Vector3 sailForce;
    public Vector3 sailTorque;

    private Vector3 goalWindVector;
    private float goalMagnitude;

    public const int firstObjectiveAngle = 270;
    public const int secondObjectiveAngle = 60;

    private int activeAngle;
    public bool firstObjectiveDone;

    public void objectiveComplete() {
        firstObjectiveDone = true;
        activeAngle = secondObjectiveAngle;
    }
    
    

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

        
        
        windState = windStateMax * perSecond;
        firstObjectiveDone = false;
        activeAngle = firstObjectiveAngle;

        Vector3 vec = createVector();
        windVector = vec;

    }

    void Update()
    {
        sailInputHorizontal = craft_gameobject.GetComponent<playerController_script>().sailInputHorizontal;
        sailInputVertical = craft_gameobject.GetComponent<playerController_script>().sailInputVertical;
        processInputs();
    }

    void FixedUpdate()
    {
        updateWind();
        sailForce = calculateSailForce(magnitude*windVector, craft_gameobject.GetComponent<Rigidbody>().velocity, transform.forward, sailFullness); 
        sailTorque = calculateSailTorque(magnitude*windVector, craft_gameobject.GetComponent<Rigidbody>().velocity, transform.forward, sailFullness); 
    }


    void updateWind()
    {
        if (windState == 0) {
            createWindGoal();
        } else if (windState < 0) {
            incrementWindtoGoal();
        } else {
            windState--;
        }
    }

    Vector3 createVector() {
        Vector3 vec;
        if (firstObjectiveDone ) {
            vec.x = nonUniformRange(0, maxWind);
            vec.y = 0;//nonUniformRange(-1 * maxWind, maxWind);
            vec.z = nonUniformRange(-1 * maxWind / 2, maxWind);
        } else {
            vec.z = nonUniformRange(-1 * maxWind, maxWind);
            vec.y = 0;//nonUniformRange(-1 * maxWind, maxWind);
            vec.x = nonUniformRange(-1 * maxWind, 0);
            if (Mathf.Abs(vec.z) > Mathf.Abs(vec.x) ) vec.x = -1 * Mathf.Abs(vec.z);

        }


        return vec;
    }
    void createWindGoal()
    {
        
        Vector3 vec = createVector();
        Debug.Log("x = " + vec.x + "   z = " + vec.z);
        float mag = nonUniformRange(minMagnitude, maxMagnitude);

        windState = -1 * windChangeDur * perSecond;

        goalMagnitude = (mag - magnitude) / (float) windState;
        goalWindVector = (vec - windVector) / (float) windState;

    }
    void incrementWindtoGoal()
    {
        magnitude -= goalMagnitude;
        windVector -= goalWindVector; 

        if (windState == -1) {
            windState = windStateMax * perSecond;
        }
        windState++;

    }

float nonUniformRange(float from, float to)
    {
        float rand = Random.Range(from, to);
        float quartile = (to - from) / 4;
        float ten = (to - from) / 10;
        
        if (rand < from + ten || rand > to - quartile) 
        {
            int r = Random.Range(0, 1);
            if (r != 0) {
                return rand;
            }
        }
        
        else if (rand < from + quartile || rand > to - quartile) {
            int r = Random.Range(0, 2);
            if (r != 0) {
                return rand;
            }
        } else {
            return rand;
        }
        return nonUniformRange(from, to);
    }

float nonUniformRangePositiveBiased(float to)
    {
        
        float rand = nonUniformRange(0, to);
        int r = Random.Range(0, 3);
        if (r != 0) return rand;
        else return rand * -1;

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
            transform.parent.localScale += new Vector3(0f,20*fullnessChange,0f);
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

// instead of actually applying a torque, maybe just apply a rocking visual effects instead of a real physics effect
    Vector3 calculateSailTorque(Vector3 windVector, Vector3 playerVelocity, Vector3 sailVector, float sailFullness)
    {
        Vector3 apparentWindVector = windVector - playerVelocity;
        float craftWindAngle = Vector3.Angle(craft_transform.forward, apparentWindVector);
        float sailWindAngle = Vector3.Angle(sailVector, apparentWindVector);
        float sailCraftAngle = Vector3.Angle(sailVector, craft_transform.forward);

        Vector3 sailTorque;
        if (craft_transform.rotation.z >= -40 || craft_transform.rotation.z <= 40) {
        sailTorque = -craft_transform.forward * apparentWindVector.magnitude * Mathf.Cos(sailWindAngle*(Mathf.PI/180f)) * Mathf.Sin(sailCraftAngle*(Mathf.PI/180f)) * sailFullness * 0.01f; // maybe make square of wind speed
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