using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damsel_random_flight : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;

    public Transform moveSpot;
    public float minX, minY, minZ, maxX, maxY, maxZ;
    Vector3 direction;
    Quaternion rotation;

    void Start()
    {
        waitTime = startWaitTime;

        direction = moveSpot.position - transform.position;
        rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;

        moveSpot.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
    }

    void Update()
    {
        //Vector3 eulerAngles = transform.rotation.eulerAngles;
        //eulerAngles = new Vector3(0, eulerAngles.y, 0);
        //transform.rotation = Quaternion.Euler(eulerAngles);

        transform.position = Vector3.MoveTowards(transform.position, moveSpot.position, speed * Time.deltaTime);
        direction = moveSpot.position - transform.position;
        //rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, moveSpot.position)<0.2f)
        {           
            if (waitTime<0)
               {
                moveSpot.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
                waitTime = startWaitTime;
               }
               else
               {
                   waitTime -= Time.deltaTime;
               }
        }
    }
}
