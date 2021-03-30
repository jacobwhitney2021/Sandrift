using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEnemyCameraFollow : MonoBehaviour
{
    public Transform enemy;

    private void Update()
    {
        Vector3 wantedPos = enemy.position;
        wantedPos.z = -20.0f;
        transform.position = wantedPos;
    }
}
