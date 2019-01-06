using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    // Use this for initialization

    public Transform followTarget;
    public Vector3 CamPosOffset = new Vector3(-10, -5.5f, 12);
   // public float camfollowSpeed = 10f;

    private Vector3 CamPosition;
    public  Vector3 CamRotOffset = new Vector3(10,0,0);
    Vector3 CameraRotatedLocation;
    Quaternion CameraRotation;

    Vector3 camVel  =Vector3.zero;
    public float CamSmoothSpeed = 10f;
   


    // rigid body of the target that this camera is currently following
    //used to get the velocity of the followtarget
    public Rigidbody followtargetRigidBody;
    Player_controller p_c;
	void Start ()
    {
        CamPosition = transform.position;
        transform.rotation = Quaternion.Euler(CamRotOffset);
       
        p_c = followTarget.GetComponent<Player_controller>();
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.rotation = Quaternion.Euler(CamRotOffset);

      
	}

    private void FixedUpdate()
    {
        FollowTarget(followTarget);
    }

    void FollowTarget(Transform TargetToFollow)
    {
        if (TargetToFollow == null)
        {
            Debug.Log("Nothing TO Follow");
            return;
        }
        changeCameraLocation();
    }

    void changeCameraLocation()
    {
        //if (followTarget)
        //{
        //    Vector3 newCamLocation = followTarget.rotation * CamPosOffset + followTarget.position;
        //    newCamLocation.z = -CamPosOffset.z;
        //    transform.position = newCamLocation;


        //}


        if (p_c.horizontal_movement > 0.1f)
        {
            CamPosOffset.x = Mathf.Abs(CamPosOffset.x);
           
        }
                

            else if (p_c.horizontal_movement < -0.1f)
            {
                CamPosOffset.x = -Mathf.Abs(CamPosOffset.x);
             
        }








       

           

        
        Vector3 newCamLocation = CameraRotation * CamPosOffset + followTarget.position;
        newCamLocation.z = -CamPosOffset.z;
        //transform.position = newCamLocation;

        //transform.position = Vector3.Lerp(transform.position, newCamLocation, Time.deltaTime * camfollowSpeed);

        transform.position = Vector3.SmoothDamp(transform.position, newCamLocation, ref camVel, Time.deltaTime * CamSmoothSpeed);

       // StartFollowingtarget(newCamLocation);

        //if (p_c.horizontal_movement < 1 && p_c.horizontal_movement>0)
        //{
        //    transform.position = Vector3.Lerp(transform.position, newCamLocation, Time.deltaTime * camfollowSpeed);
        //}
        //else if (p_c.horizontal_movement > -1 && p_c.horizontal_movement <0)
        //{
        //    transform.position = Vector3.Lerp(transform.position, newCamLocation, Time.deltaTime * camfollowSpeed);
        //}

        //else if (p_c.horizontal_movement == 1 || p_c.horizontal_movement == -1)
        //{
        //    transform.position = newCamLocation;
        //}


    }


    void moveCamera()
    {
        //if (p_c.horizontal_movement < 1 && )
        //{
        //    transform.position = Vector3.Lerp(transform.position, newCamLocation, Time.deltaTime * camfollowSpeed);
        //}
        //else if (p_c.horizontal_movement > 0)
        //{
        //    transform.position = Vector3.Lerp(transform.position, newCamLocation, Time.deltaTime * camfollowSpeed);
        //}

        //else if (p_c.horizontal_movement == 0)
        //{

        //}
    }



    void MoveToLocation()
    {

    }

    void StartFollowingtarget(Vector3 newlocation)
    {



        //transform.position = Vector3.Lerp(transform.position, newlocation, Time.deltaTime * camfollowSpeed);
       

           // transform.position = newlocation;
        
    }

    
}
