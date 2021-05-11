using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormBodyRotation : MonoBehaviour
{
    public float rotSpeed;

    private Vector3 direction;

    public Transform target;

    void Update()
    {
        direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg - 180f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
        float step = rotSpeed * Time.deltaTime;

        transform.rotation = Quaternion.Slerp(
                                transform.rotation,
                                rotation,
                                step);
    }
}
