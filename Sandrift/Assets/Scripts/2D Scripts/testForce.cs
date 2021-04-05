// The sprite will fall under its weight.  After a short time the
// sprite will start its upwards travel due to the thrust force that
// is added in the opposite direction.

using UnityEngine;
using System.Collections;

public class testForce : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float thrust = 1f;
    public Vector2 steepnessVector;

    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        steepnessVector = gameObject.GetComponent<steepnessMap>().steepnessVectorAtPoint;
        rb2D.AddForce(steepnessVector * thrust);
    }


}