using UnityEngine;
using System.Collections;

public class Lift_controller : MonoBehaviour {

    // Use this for initialization
    public bool isAutomatic = false;
    public float moveSpeed = 10f;




    bool automaticSwitchPressed = false;
    bool manualSwitchPressed = false;
    bool IsInteracting = false;
    bool IsColliding = false;

    int moveIndex = 1;


    // Specifies where the lift or this moving object should move to
    public Transform [] MoveLocations;

    // here target is lift which is being controlled by switch
    public Transform TargetTOMove; 
   
    
	void Start () {

        if (MoveLocations.Length <= 0)
        {
            Debug.Log("EndLocation or Start Location Not Found");
            Debug.Log("Please add start location and end location");

        }
	}

    void Update()
    {
        GetInput();
        if (automaticSwitchPressed || manualSwitchPressed)
        {

            MoveLift();
        }

        if (IsColliding && IsInteracting)
        {
            manualSwitchPressed = true;
        }
        
    }
	
	

    void MoveLift()
    {
       /// Debug.Log("Lift is being Called");

        TargetTOMove.position = Vector3.MoveTowards(TargetTOMove.position, MoveLocations[moveIndex].position, moveSpeed*Time.deltaTime);
        

        if ((TargetTOMove.position - MoveLocations[moveIndex].position).magnitude < 0.2f)
        {
            automaticSwitchPressed = false;
            manualSwitchPressed = false;
            moveIndex++;
            moveIndex = moveIndex % MoveLocations.Length;
        }
    }


    void OnTriggerEnter( Collider col)
    {

       
        if (col.gameObject.tag =="Player")
        {
            IsColliding = true;
            if (isAutomatic == true)
            {
                automaticSwitchPressed = true;
                Debug.Log("automatic Trigger called");
            }
            
        }
    
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            IsColliding = false;
        }

        Debug.Log("Trigger Exited");
    }

    void GetInput()
    {
        IsInteracting = Input.GetKeyDown(KeyCode.E);
    }



    





}
