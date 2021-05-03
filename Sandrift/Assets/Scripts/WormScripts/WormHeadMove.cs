using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormHeadMove : MonoBehaviour
{
    internal new Rigidbody rigidbody;
    internal new TrailRenderer trailRenderer;

    public float moveSpeed = 40.0f;
    public float rotationSpeed = 45.0f;
    public float turnAngle = 45.0f;

    internal Vector2 decisionTime = new Vector2(20f, 25f);
    internal float decisionTimeCount = 0;

    internal Vector3 currDirec;

    void Start()
    {
        decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);
        currDirec = Vector3.forward;
        ChooseMoveDirection();

        rigidbody = GetComponent<Rigidbody>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        float currSpeed = rigidbody.velocity.magnitude;
        if(currSpeed > moveSpeed*2) {
            rigidbody.velocity /= 2;
        } else if (currSpeed < moveSpeed/2) {
            rigidbody.velocity *= 2;
        }

        float angle = (Mathf.Atan2(rigidbody.velocity.x, rigidbody.velocity.z) * Mathf.Rad2Deg) + 90f;
        Quaternion wantedAngle = Quaternion.AngleAxis(angle, Vector3.up);

        if (transform.rotation != wantedAngle) {
            transform.rotation = Quaternion.RotateTowards(
                                    transform.rotation,
                                    wantedAngle,
                                    Time.deltaTime * rotationSpeed);
        }
        else {
            Vector3 currForce = currDirec * (moveSpeed * rigidbody.mass);
            Debug.Log("HITS");
            Debug.Log(currForce + " VS " + currForce * Time.deltaTime);
            rigidbody.AddForce(currForce * Time.deltaTime);
        }

        if (decisionTimeCount > 0) {
            decisionTimeCount -= Time.deltaTime;
        }
        else {
            decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);
            ChooseMoveDirection();
        }
    }

    void ChooseMoveDirection()
    {
        currDirec = Quaternion.AngleAxis(Random.Range(-1 * turnAngle, turnAngle), Vector3.up) * currDirec;
    }
}
