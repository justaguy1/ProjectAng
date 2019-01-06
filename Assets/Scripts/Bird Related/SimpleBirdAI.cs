using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBirdAI : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform[] restLocations;
    Vector3 newRestLocation;
    GameObject Player;


    Vector3 initialFlightLocation;


    float flightTakeOffSpeed = 10f;         // speed at which bird will fly away when being scared
    float flightSpeed = 8f;                 // speed at which bird will keep on flying in sky

    float distance;                         // distance between player and bird's rest location


    //public float  flightCheckingInterval =3f;

    float flyingStaredTime;
    float currentTime;
    float maxFlightTime;

    
    bool canMoveToRestLocation = false;
    bool FlyingStarted = false;
    bool keepOnFlying = false;


    float Yrot=0;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if isinground false
        // keep checking new location

        if (FlyingStarted)
        {
            InitialFLight();
        }

        if (keepOnFlying)
        {
            continuousFlight();
        }

        if (canMoveToRestLocation)
        {
            flyToRestLocation();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
       

        if (other.gameObject.CompareTag("Player"))
        {
            keepOnFlying = false;
            canMoveToRestLocation = false;

            Player = other.gameObject;
            initialFlightLocation = transform.position + new Vector3(0,10,0);
            FlyingStarted = true;


        }
    }

   

    void InitialFLight()
    {
        if (Mathf.Abs(initialFlightLocation.y-transform.position.y) <1)
        {
            FlyingStarted = false;
            keepOnFlying = true;
            flyingStaredTime = Time.fixedTime;

        }
        transform.position = Vector3.MoveTowards(transform.position, initialFlightLocation, Time.deltaTime * flightTakeOffSpeed);

    }

    void continuousFlight()
    {
        currentTime = Time.fixedTime;
        if (currentTime -flyingStaredTime >10)
        {
            CheckNewRestLocation();
            keepOnFlying = false;
        }


        // Debug.Log("continuous flight");

        // TO DO continuous flight
        float val =Random.Range(1, 10);
       
            transform.rotation = Quaternion.Euler(0, ++Yrot, 0);
       
        
        transform.position += transform.right*flightSpeed*Time.deltaTime;
        
    }

    void CheckNewRestLocation()
    {
        if (!Player) return;

        for (int i = 0; i < restLocations.Length; i++)
        {
            distance = Vector3.Distance(Player.transform.position, restLocations[i].position);
            if (distance < 10)
            {
                continue;
            }
            else
            {
                newRestLocation = restLocations[i].position;
                canMoveToRestLocation = true;
                break;
            }


        }

       
    }


    void flyToRestLocation()
    {
        if (!Player) return;

        if (Vector3.Distance(transform.position,newRestLocation) < 0.1f)
        {
            canMoveToRestLocation = false;

        }
        transform.position = Vector3.MoveTowards(transform.position, newRestLocation, Time.deltaTime * flightTakeOffSpeed);
    }

    


}
