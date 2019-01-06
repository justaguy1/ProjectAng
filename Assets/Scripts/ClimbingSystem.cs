using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingSystem : MonoBehaviour {

    GameObject Player;
    Player_controller P_c;
    RaycastHit Hit;
    bool stairLayerCollision = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GlobalVariables.HorizontalMovement = false;
            
            Player = collision.gameObject;
            P_c = Player.GetComponent<Player_controller>();

          
            
          
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GlobalVariables.HorizontalMovement = true;
           
        }
    }

    private void OnTriggerStay(Collider other)
    {
        DrawRayCast();
        if (other.gameObject.tag =="Player")
        {
            
            if (!P_c.IsGrounded && !stairLayerCollision)
            {
                GlobalVariables.HorizontalMovement = true;

                Debug.Log("called from ladder script");
            }
            else
            {
                GlobalVariables.HorizontalMovement = false;
            }

        }
    }


    void DrawRayCast()
    {

        if (!P_c || !Player)
        {
            return;
        }
        if (P_c.horizontal_movement > 0)
        {
            if (Physics.Raycast(Player.transform.position, Vector3.right, out Hit, 2.5f))
            {
                stairLayerCollision = true;
               
                if (Hit.transform.gameObject.layer == this.gameObject.layer)
                {
                    //Debug.Log("Right coll");
                    P_c.verticalMovementMultiplier = 1;
                }
                Debug.DrawRay(transform.position, Vector3.right * 2.5f, Color.green, 1);
            }
            else
            {
                stairLayerCollision = false;
            }
            // Debug.Log(Hit.transform.gameObject.name);



        }

        else if (P_c.horizontal_movement < 0)
        {

            if (Physics.Raycast(Player.transform.position, -Vector3.right, out Hit, 2.5f))
            {
                stairLayerCollision = true;
                
                if (Hit.transform.gameObject.layer == this.gameObject.layer)
                {
                    //Debug.Log("left coll");
                    P_c.verticalMovementMultiplier = -1;
                }
                Debug.DrawRay(transform.position, -Vector3.right * 2.5f, Color.green, 1);
            }
            else
            {
                stairLayerCollision = false;
            }




        }
    }


}
