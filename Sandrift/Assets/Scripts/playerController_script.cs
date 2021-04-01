using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController_script : MonoBehaviour
{
	// Preliminary controls proposal:

	// the way im thinking of mapping controls is that WASD is non-wind/rocket controls and UpDownLeftRight keys are for the sail controls
	// so A and D will control the rotation of the craft and W and S will increase/decrease rocket thrust
	// left and right will rotate the sail and up and down will raise/lower the sail    

	// whoever works on this next, feel free to change any of this to make it feel better

	public float sailAngle = 0.0f;
	public float sailFullness = 0.0f; 

	public float sailPivotSpeed = 10.0f; // speed of rotation of the sail
	public float sailFullnessSpeed = 1.5f; // speed that the sail changed fullness

	public float fullness_change;

    void Start()
    {
        
    }

    void Update()
    {
        float rotation_change = Input.GetAxis("SailHorizontal") * sailPivotSpeed * Time.deltaTime;
        sailAngle += rotation_change;
        if (sailAngle<0f) {
        	sailAngle += 360f;
        }
        if (sailAngle>360f) {
        	sailAngle -= 360f;
        }


        fullness_change = Input.GetAxis("SailVertical") * sailFullnessSpeed * Time.deltaTime;
        if (sailFullness+fullness_change<0) {
        	sailFullness = 0f;
        }
        else if (sailFullness+fullness_change>1) {
        	sailFullness = 1f;
        }
        else {
        	sailFullness += fullness_change;
        }


        

    }


}
