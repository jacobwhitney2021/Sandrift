using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketUnlock : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "objective1")
        {
        	gameObject.GetComponent<rocketForce_script>().enabled = true;
        }
    }
}
