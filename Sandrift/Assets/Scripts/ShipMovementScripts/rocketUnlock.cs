using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class rocketUnlock : MonoBehaviour
{
    public GameObject worm;
    public Vector3 spawn1 = new Vector3(50, 60, 250);
    public Vector3 spawn2 = new Vector3(350, 60, 450);
    public Vector3 spawn3 = new Vector3(650, 60, 650);

    private int instantiate;

    private float time;
    private void Start()
    {
        time = 0;
        instantiate = -1;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "objective1")
        {
        	gameObject.GetComponent<rocketForce_script>().enabled = true;
            GameObject.Find("sailChild").GetComponent<windForce_script>().objectiveComplete();
            if (instantiate == -1) {

                Instantiate(worm, spawn1,  Quaternion.identity);
                time = 0;
                instantiate = 0;
            }


        }
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > 10 && instantiate == 0) {
            instantiate++;
            Instantiate(worm, spawn2,  Quaternion.identity);
        }
        if (time > 20 && instantiate == 1) {
            instantiate++;
            Instantiate(worm, spawn3,  Quaternion.identity);
        }
    }
}
