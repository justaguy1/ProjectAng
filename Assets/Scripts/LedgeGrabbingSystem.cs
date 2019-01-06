using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrabbingSystem : MonoBehaviour
{
    bool CanGrabLegde = true;

    // this transform determines where the player's  will be when grabbing ledge 
    public Transform ledgeToGrab;

    // this transform determines where the Player's hands will be placed when grabbing ledge
    public Transform LeftHandPlacement, RightHandPlacement;

    GameObject Player;
     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player = other.gameObject;
            //CanGrabLegde = true;
            GrabLedge();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Grabbing");
        if (Input.GetKeyDown(KeyCode.E) && other.gameObject.CompareTag("Player"))
        {
            ///Player = null;
          //  CanGrabLegde = false;
            DropFromLedge();

            Debug.Log("drop called");
        }
    }


    void GrabLedge()
    {
        if (Player  != null)
        {
            GlobalVariables.allowAnyMovement = false;
            Player.GetComponent<Rigidbody>().isKinematic = true;
            Player.transform.position = ledgeToGrab.position;
            
        }
    }

    void DropFromLedge()
    {
        
        
        GlobalVariables.allowAnyMovement = true;
        Player.GetComponent<Rigidbody>().isKinematic = false;
        //Player.transform.position =

    }
    


   

  
}
