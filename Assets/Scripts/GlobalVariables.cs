using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables {

    // Use this for initialization

    public  static bool allowAnyMovement = true;
    public  static bool HorizontalMovement = true;
    public  static bool playerCanRotate =true;
    
   
    public static bool playerGroundCollision = false;

    public static float Get_Y_Rot_from_Velocity(Vector3 velocity)
    {
        Vector3 vel = velocity.normalized;
        return (Mathf.Atan2(vel.z, vel.x)) * 180 / Mathf.PI;
       
    }
   
}
