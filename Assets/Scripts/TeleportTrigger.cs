using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour {

	public TeleportScript teleport;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			teleport.StartTeleportation (other.transform);
		}
	}
}
