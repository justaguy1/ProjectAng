using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    public  newBirdAI bird;
    

    GameObject owner;
    bool PlayerIsNear = false;

    void Start()
    {
        owner = this.gameObject;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerIsNear = true;
            GameObject Player = other.gameObject;

            if (Vector3.Distance(bird.transform.position,Player.transform.position) < 3)
            {
                Vector3 newLoc = bird.transform.position + new Vector3(0, 20, 0);
                bird.StartCoroutine(bird.StartFlying(newLoc));
            }
            
                if (bird.restlocations[bird.moveIndex] == this.transform)
                {
                    bird.moveIndex = (++bird.moveIndex) % bird.restlocations.Length;
                  
                }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerIsNear = false;
        }
    }

}
