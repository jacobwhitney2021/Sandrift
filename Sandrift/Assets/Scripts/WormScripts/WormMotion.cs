using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class WormMotion : MonoBehaviour
{
    public GameObject head;
    public GameObject bodyPartsPrefab;

    public float moveForce;
    public float rotationSpeed;
    public float turnAngle;
    public float smoothSpeed;
    public int bodyPartsLength;
    public float trailTime;
    public Vector2 decisionTime = new Vector2(10f, 15f);

    private GameObject bodyPartsHolder;

    internal float decisionTimeCount = 0;

    internal Vector3 currDirec;

    private GameObject[] bodyPartsList;

    private Rigidbody headRB;
    private TrailRenderer headTR;
    private MeshCollider headMC;

    //public float slitherSpeed = 90.0f;
    //public float trailAngleLim = 30.0f;
    //private bool trailDirec;

    internal Vector3 backVertexMesh;
    internal Vector3 frontVertexMesh;

    void Start()
    {
        headRB = head.GetComponent<Rigidbody>();
        headMC = head.GetComponent<MeshCollider>();
        headTR = head.GetComponentInChildren<TrailRenderer>();

        headTR.time = trailTime;

        decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);

        currDirec = Vector3.forward;
        ChooseMoveDirection();
        headRB.velocity = Vector3.zero;

        bodyPartsHolder = new GameObject("BodyPartsHolder");
        bodyPartsHolder.transform.parent = headTR.transform;
        bodyPartsHolder.transform.localScale = Vector3.one;

        bodyPartsList = new GameObject[bodyPartsLength];

        //trailDirec = true;
    }

    void Update()
    {
        MoveRotateHead();
        MoveBodyParts();
        //HittingTerrain();
    }

    void MoveRotateHead()
    {
        float angle = (Mathf.Atan2(currDirec.x, currDirec.z) * Mathf.Rad2Deg) + 90f;
        Quaternion wantedAngle = Quaternion.AngleAxis(angle, Vector3.up);

        headRB.rotation = Quaternion.RotateTowards(
                                            headRB.rotation,
                                            wantedAngle,
                                            Time.deltaTime * rotationSpeed);

        if (headRB.rotation == wantedAngle)
        {
            Vector3 currForce = (-1*headRB.transform.right) * moveForce * headRB.mass;
            headRB.AddForce(currForce);
        }

        LimitVelocity();

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

    void MoveBodyParts()
    { 
        int posLength = 20000;
        Vector3[] positions = new Vector3[posLength];
        int length = headTR.GetPositions(positions);
        int bpLength = bodyPartsLength;
        int bpIndex = 0;
        int bpStep = length / bpLength;
        for (int index = length-1; (index > 0 && bpIndex < bpLength); index -= bpStep)
        {
            if (bodyPartsList[bpIndex] == null)
            {
                bodyPartsList[bpIndex] = Instantiate(
                                        bodyPartsPrefab,
                                        positions[index],
                                        Quaternion.identity,
                                        bodyPartsHolder.transform);
                bodyPartsList[bpIndex].transform.localScale = Vector3.one;
            }

            Transform currBP = bodyPartsList[bpIndex].transform;
            Rigidbody currRB = bodyPartsList[bpIndex].GetComponent<Rigidbody>();
            Vector3 currVelocity = currRB.velocity;
            currRB.position = Vector3.SmoothDamp(
                                currBP.position,
                                positions[index],
                                ref currVelocity,
                                Time.deltaTime*smoothSpeed);

            if (bpIndex == 0)
            {
                currRB.rotation = Quaternion.RotateTowards(
                                            currRB.rotation,
                                            headRB.rotation,
                                            Time.deltaTime * rotationSpeed);
            }
            else
            {
                Vector3 currSection = positions[index] - positions[index + 1];
                float currAngle = (Mathf.Atan2(currSection.x, currSection.z) * Mathf.Rad2Deg) + 90f;
                Quaternion wantedRotation = Quaternion.AngleAxis(currAngle, Vector3.up);
                currRB.rotation = Quaternion.RotateTowards(
                                            currRB.rotation,
                                            wantedRotation,
                                            Time.deltaTime * rotationSpeed);
            }
            bpIndex++;
        }
    }

    //void SlitherHead()
    //{
    //    Quaternion leftSide = Quaternion.AngleAxis(trailAngleLim, Vector3.up);
    //    Quaternion rightSide = Quaternion.AngleAxis(-1 * trailAngleLim, Vector3.up);

    //    if (trailDirec)
    //    {
    //        Debug.Log("SLITHER RIGHT");
    //        headRB.rotation = Quaternion.RotateTowards(
    //                                headRB.rotation,
    //                                rightSide,
    //                                Time.deltaTime * slitherSpeed);
    //        if(headRB.rotation == rightSide)
    //        {
    //            trailDirec = false;
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("SLITHER LEFT");
    //        headRB.rotation = Quaternion.RotateTowards(
    //                                headRB.rotation,
    //                                leftSide,
    //                                Time.deltaTime * slitherSpeed);
    //        if (headRB.rotation == leftSide)
    //        {
    //            trailDirec = true;
    //        }
    //    }
    //}

    void ChooseMoveDirection()
    {
        currDirec = (Quaternion.AngleAxis(Random.Range(-1 * turnAngle, turnAngle), Vector3.up) * currDirec).normalized;
    }

    void LimitVelocity()
    {
        float currSpeed = headRB.velocity.magnitude;
        float normSpeed = moveForce;
        if (currSpeed > normSpeed*2)
        {
            headRB.velocity /= 2;
        }
        else if (currSpeed < normSpeed/2)
        {
            headRB.velocity *= 2;
        }
    }

    // void GettingTipsOfMesh()
    // {
    //     Mesh mesh = headMC.sharedMesh;
    //     Bounds bounds = mesh.bounds;
    //     Vector3 extents = bounds.extents;
    //     frontVertexMesh = head.transform.TransformPoint(bounds.center + new Vector3(1 * extents.x, 0, 0));
    //     backVertexMesh = head.transform.TransformPoint(bounds.center + new Vector3(-1 * extents.x, 0, 0));
    // }

    // void HittingTerrain()
    // {
    //     GettingTipsOfMesh();
    //     Vector3 hitVector = head.transform.TransformDirection(Vector3.down);

    //     RaycastHit hit;
    //     if (Physics.Raycast(frontVertexMesh, hitVector, out hit))
    //     {
    //         Debug.DrawRay(frontVertexMesh, hitVector * hit.distance, Color.yellow);
    //         //Debug.Log("Did Hit");
    //     }
    //     else
    //     {
    //         Debug.DrawRay(frontVertexMesh, hitVector * 1000, Color.white);
    //         //Debug.Log("Did not Hit");
    //     }

    //     RaycastHit hit1;
    //     if (Physics.Raycast(backVertexMesh, hitVector, out hit1))
    //     {
    //         Debug.DrawRay(backVertexMesh, hitVector * hit1.distance, Color.yellow);
    //         //Debug.Log("Did Hit");
    //     }
    //     else
    //     {
    //         Debug.DrawRay(backVertexMesh, hitVector * 1000, Color.white);
    //         //Debug.Log("Did not Hit");
    //     }
    // }

    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.magenta;
    //     Gizmos.DrawSphere(frontVertexMesh, 1);
    //     Gizmos.DrawSphere(backVertexMesh, 1);
    // }
}
