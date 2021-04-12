 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 public class cameraFollow_script : MonoBehaviour {
     public Transform target;
     public float y_offset;
     public float z_offset;
     public float tilt_angle_offset;
     public float followSpeed = 3f;
     public float rotationSpeed = 3f;

     Vector3 position;
     Vector3 newPos;
     Quaternion rotation;
     Quaternion newRot;
 
 

 // change to reference the global angle rather than the boats angle to make the camerea more steady 
     // it can still track position, but the angles need to be disconnectedc 

     // maybe make it based off of gloab coordinates???

     // i love video james

     void Start() {
         //y_offset = 10f;
         //z_offset = 10f;
         position = new Vector3(target.position.x, target.position.y + y_offset, target.position.z + z_offset);
         rotation = Quaternion.Euler(new Vector3(tilt_angle_offset, target.rotation.eulerAngles.y, 0));
     }
     
     void FixedUpdate() {
         if (target) {
             newPos = target.position + z_offset*target.forward + y_offset*target.up;
             newRot = Quaternion.Euler(new Vector3(tilt_angle_offset, target.rotation.eulerAngles.y, 0));
             position = Vector3.Lerp(position, newPos, followSpeed * Time.deltaTime);
             rotation = Quaternion.Lerp(rotation, newRot, rotationSpeed * Time.deltaTime);
             transform.position = position;
             transform.rotation = rotation;
         }
     }
 }