using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newBirdAI : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform[] restlocations;
    public float moveSpeed=10f;
    public Vector3 newMoveLocation;

    bool canMoveToLocation = false;
    public bool isOnGround ;
    public int moveIndex = 0;

    BoxCollider col;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // startFlying();
    }
   

    void startFlying()
    {
        Debug.Log("should not be called right now");
        transform.position = Vector3.MoveTowards(transform.position,newMoveLocation,moveSpeed*Time.deltaTime);
        if (Vector3.Distance(transform.position,newMoveLocation) < 0.5f)
        {
            moveTowardsRestLocation();
        }

    }

    void moveTowardsRestLocation()
    {
        //newMoveLocation = restlocations[(++moveIndex) % restlocations.Length].position;
        newMoveLocation = restlocations[moveIndex].position;
    }

    public IEnumerator StartFlying(Vector3 newLoc)
    {
        while (transform.position != newMoveLocation)
        {
            transform.position = Vector3.MoveTowards(transform.position, newLoc, moveSpeed * Time.deltaTime);
        }
        //startFlying();
        yield return null;
    }

    


}
