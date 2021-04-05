using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEnemyCameraFollow : MonoBehaviour
{
    public Transform enemy;
    public float cameraDist;

    private void Update()
    {
        Vector3 wantedPos = enemy.position;
        wantedPos.z = cameraDist;
        transform.position = wantedPos;
        Debug.Log(Time.time+"----"+transform.position+"----"+enemy.position);
    }
}
