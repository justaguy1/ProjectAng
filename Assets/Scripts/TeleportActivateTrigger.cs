using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportActivateTrigger : MonoBehaviour {

	public TeleportScript teleport;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
			teleport.ActivateTeleport (true);
	}

}
