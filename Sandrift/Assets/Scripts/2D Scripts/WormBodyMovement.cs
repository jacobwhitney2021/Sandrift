using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormBodyMovement : MonoBehaviour
{
    public int length;
    public LineRenderer lineRend;
    public Vector3[] segmentPoses;
    private Vector3[] segmentV;

    public Transform targetDir;
    public float targetDist;
    public float smoothSpeed;

    public float wiggleSpeed;
    public float wiggleMagnitude;
    public Transform wiggleDir;
    public Transform[] bodyParts;

    private void Start()
    {
        lineRend.positionCount = length;
        segmentPoses = new Vector3[length];
        segmentV = new Vector3[length];
    }

    private void Update()
    {
        float wiggleStep = Time.time * wiggleSpeed;
        wiggleDir.localRotation = Quaternion.Euler(
                                    0,
                                    0,
                                    Mathf.Sin(wiggleStep)*wiggleMagnitude);

        segmentPoses[0] = targetDir.position;

        for (int i = 1; i <length; i++)
        {
            segmentPoses[i] = Vector3.SmoothDamp(
                                segmentPoses[i],
                                segmentPoses[i-1]+targetDir.right*targetDist,
                                ref segmentV[i],
                                smoothSpeed);

            
        }

        lineRend.SetPositions(segmentPoses);

        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].transform.position = segmentPoses[i];
        }

        
    }
}
