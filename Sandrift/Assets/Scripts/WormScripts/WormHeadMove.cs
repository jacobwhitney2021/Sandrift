using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormHeadMove : MonoBehaviour
{
    internal new Rigidbody rigidbody;

    internal float moveSpeed = 5.0f;
    internal float rotSpeed = 35.0f;

    internal Vector2 decisionTime = new Vector2(10f, 15f);
    internal float decisionTimeCount = 0;

    internal Vector3[] allMoveDirections = new Vector3[] {
        new Vector3 (0, 0, -1), new Vector3 (0, 0, 1),
        new Vector3 (-1, 0, 0), new Vector3 (1, 0, 0),
        new Vector3 (-1, 0, -1), new Vector3 (-1, 0, 1),
        new Vector3 (1, 0, -1), new Vector3 (1, 0, 1)};

    internal int currentMoveDirection;

    internal Vector3 currDirec;

    void Start()
    {
        decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);
        currDirec = Vector3.forward;
        ChooseMoveDirection();

        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        currDirec = allMoveDirections[currentMoveDirection];

        Vector3 wantedPosition = (currDirec * Time.deltaTime * moveSpeed) + transform.position;

        float angle = (Mathf.Atan2(currDirec.x, currDirec.z) * Mathf.Rad2Deg) - 180f;
        Quaternion wantedAngle = Quaternion.AngleAxis(angle, Vector3.up);

        if (transform.rotation != wantedAngle)
        {
            transform.rotation = Quaternion.RotateTowards(
                                    transform.rotation,
                                    wantedAngle,
                                    Time.deltaTime * rotSpeed);
        }
        else
        {
            //transform.position = Vector3.MoveTowards(
            //                        transform.position,
            //                        wantedPosition,
            //                        Time.deltaTime * moveSpeed);
            rigidbody.AddForce(currDirec * moveSpeed);
        }

        if (decisionTimeCount > 0)
        {
            decisionTimeCount -= Time.deltaTime;
        }
        else
        {
            decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);
            ChooseMoveDirection();
        }
    }

    void ChooseMoveDirection()
    {
        int previousMoveDirection = currentMoveDirection;
        do
        {
            currentMoveDirection = Mathf.FloorToInt(
                                    Random.Range(
                                        0,
                                        allMoveDirections.Length));
        } while (previousMoveDirection == currentMoveDirection);
    }
}
