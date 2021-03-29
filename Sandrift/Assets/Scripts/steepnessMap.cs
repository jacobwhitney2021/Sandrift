using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//dune physics ideas:


//create dune systematically with some class constructor

//height
//thickness
//length / waviness????

//grid system for procedual generation 

public class steepnessMap : MonoBehaviour
{
    private GameObject playerObject;
    private GameObject duneSpine;
    private Vector2 playerObjectPosition;
    public Vector2 steepnessVectorAtPoint;
    
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        duneSpine = GameObject.FindGameObjectWithTag("Dune");
    }

    void Update()
    {
        playerObjectPosition = playerObject.transform.position;
        steepnessVectorAtPoint = new Vector2(Mathf.Sin(playerObjectPosition.x), Mathf.Sin(playerObjectPosition.x));
    }
}
