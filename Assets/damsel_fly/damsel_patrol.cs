using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damsel_patrol : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;

    public Transform[] moveSpots;
    private int randomSpots;
    
    void Start()
    {
        waitTime = startWaitTime;
        randomSpots = Random.Range(0, moveSpots.Length);
     }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveSpots[randomSpots].position, speed * Time.deltaTime);

      /*  Vector3 relativePos = moveSpots[randomSpots].position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(moveSpots[randomSpots].position, Vector3.forward);
        transform.rotation = rotation;*/

        if (Vector3.Distance(transform.position, moveSpots[randomSpots].position)<0.2f)
        {
            if(waitTime<0)
            {
                randomSpots = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
