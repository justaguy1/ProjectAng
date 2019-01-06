using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    // Start is called before the first frame update

    Vector3 myPos;

    // radius within which the random point should be generated
    public float randomRadius =5;
    public float moveSpeed =10f;
    
    

    


   

    Vector3 MoveVel =Vector3.one;
    Vector3 moveAcceleration;

    Vector3 StartLocation;

    // location where this object should move to
    Vector3 TargetTomoveTo =Vector3.zero;



    void Start()
    {
        StartLocation = transform.position;
        myPos = transform.position;
       
    }

    
    void Update()
    {

        Debug.Log(MoveVel);
        
        MoveVel += moveAcceleration * Time.deltaTime*moveSpeed;
        MoveVel = Vector3.ClampMagnitude(MoveVel, 0.5f);
        transform.position += MoveVel;
        //transform.rotation = Quaternion.LookRotation(MoveVel.normalized);
        if (Input.GetKeyDown(KeyCode.K))
        {
            moveAcceleration = (TargetTomoveTo - transform.position).normalized;
            TargetTomoveTo = GenerateRandomLocation();
            Debug.DrawLine(StartLocation, TargetTomoveTo,Color.yellow,1);
        }
       
    }


   




    bool chanceTodoAnything(float chance)
    {
        float value = Random.Range(0,100);
        if (value <= chance)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

   Vector3 GenerateRandomLocation()
    {
        float ranX = Random.Range(StartLocation.x - randomRadius, StartLocation.x + randomRadius);
        float ranY = Random.Range(StartLocation.y +(randomRadius-2), StartLocation.y + randomRadius);
        float ranz = Random.Range(StartLocation.z - randomRadius, StartLocation.z + randomRadius);

        return  new Vector3(ranX,ranY,ranz);
    }


    private void OnTriggerEnter(Collider other)
    {
        
    }
}
