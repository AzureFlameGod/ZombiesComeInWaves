using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGenerator : MonoBehaviour {

	public Transform[] zombiePrefabs;
	public int zombies = 100;

	public Vector3 boxSize = new Vector3(20.0f, 3.0f, 20.0f);

	// Use this for initialization
	void Start () {
		Vector3 pos = transform.position;

		for (int i = 0; i < zombies; i++)
		{
			Transform prefab = zombiePrefabs[Random.Range(0, zombiePrefabs.Length)];
			Instantiate(prefab, 
				pos + new Vector3(
					Random.Range(-boxSize.x, boxSize.x), 
					Random.Range(-boxSize.y, boxSize.y), 
					Random.Range(-boxSize.z, boxSize.z)),
				Quaternion.LookRotation(Random.onUnitSphere)
			)
			;
				
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
