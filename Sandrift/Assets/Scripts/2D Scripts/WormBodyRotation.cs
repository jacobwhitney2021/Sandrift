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
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        float step = rotSpeed * Time.deltaTime;

        transform.rotation = Quaternion.Slerp(
                                transform.rotation,
                                rotation,
                                step);
    }
}
