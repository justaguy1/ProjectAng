using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportScript : MonoBehaviour {

	public bool isOpen;
	public TeleportScript otherSide;
	public Transform teleportPosition;
	public GameObject activateTrigger;


	private BoxCollider boxCol;
	private Animator anim;
	private Transform playerTrans;
	private bool isActive = false;

	void Awake()
	{
		boxCol = GetComponent<BoxCollider> ();
		//anim = GetComponent<Animator> ();
	}

	void Start()
	{
		activateTrigger.SetActive (false);

		if (isOpen) 
		{
			OpenTeleport ();
			ActivateTeleport (true);
		}

	}

	public void StartTeleportation(Transform player)
	{
		if (isActive) 
		{
			playerTrans = player;

			//playerCant move

			StartCoroutine (Teleport ());

			CloseTeleport ();
		}
	}

	IEnumerator Teleport()
	{
		ActivateTeleport (false);

		yield return new WaitForSeconds (0f);
		otherSide.OpenTeleport ();
		otherSide.ActivateTeleport (false);


		yield return new WaitForSeconds (0.5f);
		playerTrans.position = otherSide.teleportPosition.position;
		StartCoroutine (ColliderSwitch (true));

		yield return new WaitForSeconds (0.2f);
		//player can move
		
	}


	public void OpenTeleport()
	{
	//	anim.SetBool ("OpenTeleport", true);
		StartCoroutine (ColliderSwitch (false));
	}

	public void CloseTeleport()
	{
	//	anim.SetBool ("OpenTeleport", false);
		StartCoroutine(ColliderSwitch (true));
	}

	IEnumerator ColliderSwitch(bool state)
	{
		yield return new WaitForSeconds (0.5f);
		boxCol.enabled = state;
		activateTrigger.SetActive (!state);
	}

	public void ActivateTeleport(bool state)
	{
		isActive = state;
	}
}
