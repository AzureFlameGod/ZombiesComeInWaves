using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// respawns zombies to be near the respawn point
public class ZombieRecycler : MonoBehaviour {

	public Transform respawnPoint;
	public Vector3 respawnOffsetEllipsoid;

	void OnTriggerStay(Collider other)
	{
		//Debug.Log("Respawning zombie");
		other.transform.position = respawnPoint.position + Vector3.Scale(Random.insideUnitSphere, respawnOffsetEllipsoid);
	}
}
