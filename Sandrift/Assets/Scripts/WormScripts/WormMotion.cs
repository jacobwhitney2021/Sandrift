using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class WormMotion : MonoBehaviour
{

    public float moveForce = 40.0f;
    public float rotationSpeed = 45.0f;
    public float turnAngle = 60.0f;
    public Vector2 decisionTime = new Vector2(10f, 15f);
    public float smoothSpeed = 0.3f;

    public GameObject head;
    public GameObject bodyPartsPrefab;
    private GameObject bodyPartsHolder;

    internal float decisionTimeCount = 0;

    internal Vector3 currDirec;

    public int bodyPartsLength = 10;
    private GameObject[] bodyPartsList;

    private Rigidbody headRB;
    private TrailRenderer headTR;
    private MeshCollider headMC;

    internal Vector3 backVertexMesh;
    internal Vector3 frontVertexMesh;

    void Start()
    {
        headRB = head.GetComponent<Rigidbody>();
        headMC = head.GetComponent<MeshCollider>();
        headTR = head.GetComponentInChildren<TrailRenderer>();

        decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);

        currDirec = Vector3.forward;
        ChooseMoveDirection();
        headRB.velocity = Vector3.zero;

        bodyPartsHolder = new GameObject("BodyPartsHolder");
        bodyPartsHolder.transform.parent = headTR.transform;
        bodyPartsHolder.transform.localScale = Vector3.one;

        bodyPartsList = new GameObject[bodyPartsLength];
    }

    void Update()
    {
        MoveRotateHead();
        MoveBodyParts();
        HittingTerrain();
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
            Vector3 currForce = currDirec * moveForce * headRB.mass;
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
        print(length);
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
                                Time.deltaTime* smoothSpeed);

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

    void GettingTipsOfMesh()
    {
        Mesh mesh = headMC.sharedMesh;
        Bounds bounds = mesh.bounds;
        Vector3 extents = bounds.extents;
        frontVertexMesh = head.transform.TransformPoint(bounds.center + new Vector3(1 * extents.x, 0, 0));
        backVertexMesh = head.transform.TransformPoint(bounds.center + new Vector3(-1 * extents.x, 0, 0));
    }

    void HittingTerrain()
    {
        GettingTipsOfMesh();
        Vector3 hitVector = head.transform.TransformDirection(Vector3.down);

        RaycastHit hit;
        if (Physics.Raycast(frontVertexMesh, hitVector, out hit))
        {
            Debug.DrawRay(frontVertexMesh, hitVector * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(frontVertexMesh, hitVector * 1000, Color.white);
            //Debug.Log("Did not Hit");
        }

        RaycastHit hit1;
        if (Physics.Raycast(backVertexMesh, hitVector, out hit1))
        {
            Debug.DrawRay(backVertexMesh, hitVector * hit1.distance, Color.yellow);
            //Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(backVertexMesh, hitVector * 1000, Color.white);
            //Debug.Log("Did not Hit");
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(frontVertexMesh, 1);
        Gizmos.DrawSphere(backVertexMesh, 1);

        //int posLength = 20000;
        //Vector3[] positions = new Vector3[posLength];
        //int length = headTR.GetPositions(positions);
        //int index = 0;
        //print(length);
        //Gizmos.DrawCube(positions[0], Vector3.one * 50);
        //while (positions[index] != null && index <= length)
        //{
        //    Gizmos.DrawCube(positions[index], Vector3.one * 50 * (index / 30));
        //    index += 30;
        //}
    }
}
