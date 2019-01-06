using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObjectSystem : MonoBehaviour {

    private GameObject Player;

    private bool canInteract;
    private bool isBeingInteracted=false;
    private Vector3 moveVel;
    private Rigidbody rb;
    private Player_controller p_c;
    private RaycastHit Hit;


    float oldRot, NewRot;
   
    

    public Vector3 pussOffset;
    Vector3 NewMovePos = Vector3.zero;
    private LayerMask mylayer;

   // Vector3 velocity; // is being used as ref variable



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb ==null)
        {
            Debug.Log("Please add rigidbody to the movable object");
        }

        oldRot = transform.eulerAngles.z;

        
        mylayer = this.gameObject.layer;
       
    }

    private void Update()
    {
        getInput();


        if (isBeingInteracted)
        {
            
            Debug.Log(Mathf.Abs(p_c.transform.rotation.eulerAngles.z - transform.rotation.eulerAngles.z));
            MoveItem();

        }
      
        
        GlobalVariables.playerCanRotate = !isBeingInteracted;


        // this code is used to disable interaction with moveable object
        // it works by checking if the rotation changes then the interaction with object is broken
        NewRot = transform.eulerAngles.z;
        if (Mathf.Abs(oldRot-NewRot) >5)
        {
            oldRot = NewRot;
            isBeingInteracted = false;
        }
             

       
        

       
        
         
    }

    

    void getInput()
    {
       
            if (Input.GetKeyDown(KeyCode.E))
            {
            if (p_c)
            {
                if (!p_c.IsGrounded)
                {
                    DrawRaycast();
                }
            }
            
                
            }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player = other.gameObject;
            p_c = Player.GetComponent<Player_controller>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag =="Player")
        {
            SetMovableObjectOffset();
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            p_c.maxJumpCount = 2;
            Player = null;
            p_c = null;
            isBeingInteracted = false;

        }
    }




    private void MoveItem()
    {


        if (Player !=null && p_c != null)
        {
            

           
            Debug.Log("move called");
            NewMovePos.x = Player.transform.position.x;
            NewMovePos.y = transform.position.y;
            NewMovePos.z = transform.position.z;

            NewMovePos =  NewMovePos  + pussOffset;
           
            transform.position = NewMovePos;
            
          
            

        }
    }

    void DrawRaycast()
    {

        if (!Player || !p_c )
        {
            return;
        }

        if (Physics.Raycast(Player.transform.position, Player.transform.right, out Hit, 2f))
        {
            if (Hit.transform.gameObject.layer == mylayer)
            {

                isBeingInteracted = !isBeingInteracted;
                if (isBeingInteracted)
                {
                    p_c.maxJumpCount = 0;
                }
                else
                {
                    p_c.maxJumpCount = 2;
                }
            }
           

        }
       

        Debug.DrawRay(Player.transform.position,Player.transform.right * 2, Color.green, 1f);

       
    }

    void SetMovableObjectOffset()
    {
         if (p_c.horizontal_movement > 0)
        {
           

            if (!isBeingInteracted)
            {
                pussOffset.x = Mathf.Abs(pussOffset.x);
            }
          


        }

        else if (p_c.horizontal_movement < 0)
        {

            if (!isBeingInteracted)
            {
                pussOffset.x = -Mathf.Abs(pussOffset.x);
            }

        }
    }

}
