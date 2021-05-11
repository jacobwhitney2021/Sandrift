using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindParticle_script : MonoBehaviour
{
    private Transform craft_transform;
    private GameObject sail;
    public Vector3 windVector;
    public float windAngle;

    public float distance_lag;


    // Start is called before the first frame update
    void Start()
    {
		craft_transform = GameObject.FindGameObjectWithTag("Player").transform;
        sail = GameObject.FindGameObjectWithTag("Sail");
    }

    void Update() 
    {
        windVector = sail.GetComponent<windForce_script>().windVector;
        windAngle = -1 * Vector3.SignedAngle(Vector3.forward, windVector, Vector3.up);
        // Debug.Log("wind " + windAngle);
        // Debug.Log("old " + Vector3.Angle(Vector3.forward, windVector));

        transform.rotation = Quaternion.Euler(new Vector3(0f, -windAngle, 0f));
        transform.position = craft_transform.position - (windVector.normalized*distance_lag);
     }
}
