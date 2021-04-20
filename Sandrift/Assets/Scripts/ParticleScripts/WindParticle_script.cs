using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindParticle_script : MonoBehaviour
{
    private Transform craft_transform;


    // Start is called before the first frame update
    void Start()
    {
		craft_transform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = craft_transform.position + new Vector3(0f,0f,-15f);
    }
}
