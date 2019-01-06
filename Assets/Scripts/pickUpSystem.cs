using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpSystem : MonoBehaviour {

    // Use this for initialization

    bool canInteract;
    bool isbeingCarried = false;
    bool interact;
    BoxCollider col;


    public Transform playerHand;
    public Vector3 pickUpLocationOffset = new Vector3(1.3f, 1, 0);

    public Transform Myparent;


    void Start () {
        col = GetComponent<BoxCollider>();
       
	}
	
	// Update is called once per frame
	void Update ()
    {
       
        col.enabled = !isbeingCarried;

        if (canInteract )
        {
            getInput();
        }
        if (interact && !isbeingCarried)
        {
            AttachObject();
           // Debug.Log("Player hhand called");
        }

    

        if (isbeingCarried)
        {
            //transform.parent = playerHand.transform;
           Myparent.position = playerHand.rotation*(pickUpLocationOffset)+playerHand.position;

           

            if (!interact)
            {
                detachObject();
            }

            
        }
        
	}

    void getInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            interact = !interact;
        }
       


      //  print("interact :" + interact);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag =="Player")
        {
            canInteract = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            canInteract = false;
         
        }
    }

    // attach this pickup object to the players hand
    void AttachObject()
    {
        if (playerHand != null )
        {

            // Destroy(GetComponentInParent<Rigidbody>());
            GetComponentInParent<Rigidbody>().isKinematic = true;
            GetComponentInParent<BoxCollider>().enabled = false;
            isbeingCarried = true;
            
        }
    }


    // attach this pickup object to the players hand
    void detachObject()
    {
        
        // Myparent.gameObject.AddComponent<Rigidbody>();
         isbeingCarried = false;
        GetComponentInParent<BoxCollider>().enabled = true;
        GetComponentInParent<Rigidbody>().isKinematic = false;
    }

   
}
