using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormHeadMovement : MonoBehaviour
{
    internal float moveSpeed = 2.5f;
    internal float rotSpeed = 70.0f;

    internal Vector2 decisionTime = new Vector2(4f, 8f);
    internal float decisionTimeCount = 0;

    internal Vector3[] allMoveDirections = new Vector3[] {
        new Vector3 (0, -1, 0), new Vector3 (0, 1, 0),
        new Vector3 (-1, 0, 0), new Vector3 (1, 0, 0),
        new Vector3 (-1, -1, 0), new Vector3 (-1, 1, 0),
        new Vector3 (1, -1, 0), new Vector3 (1, 1, 0)};

    internal int currentMoveDirection;

    internal Vector3 currDirec;
    internal Vector3 prevDirec;

    void Start()
    {
        decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);
        currDirec = Vector3.forward;
        ChooseMoveDirection();
    }

    void Update()
    {
        currDirec = allMoveDirections[currentMoveDirection];

        Vector3 wantedPosition = (currDirec * Time.deltaTime * moveSpeed) + transform.position;

        float angle = (Mathf.Atan2(currDirec.y, currDirec.x) * Mathf.Rad2Deg);
        Quaternion wantedAngle = Quaternion.AngleAxis(angle, Vector3.forward);

        if (transform.rotation!=wantedAngle)
        {
            transform.rotation = Quaternion.RotateTowards(
                                    transform.rotation,
                                    wantedAngle,
                                    Time.deltaTime * rotSpeed);
            //transform.rotation = Quaternion.Slerp(
            //                          transform.rotation,
            //                          wantedAngle,
            //                          Time.deltaTime * rotSpeed);
        }
        else
        {
            transform.position = Vector3.MoveTowards(
                                    transform.position,
                                    wantedPosition,
                                    Time.deltaTime * moveSpeed);
        }
            
        if (decisionTimeCount > 0)
        {
            decisionTimeCount -= Time.deltaTime;
        }
        else
        {
            decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);
            ChooseMoveDirection();
            prevDirec = currDirec;
        }
    }

    void ChooseMoveDirection()
    {
        currentMoveDirection = Mathf.FloorToInt(
                                Random.Range(
                                    0,
                                    allMoveDirections.Length));
    }
}
