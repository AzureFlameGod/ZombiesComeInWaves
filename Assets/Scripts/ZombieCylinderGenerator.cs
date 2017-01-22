using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCylinderGenerator : MonoBehaviour {

	public Transform[] zombiePrefabs;

	public CapsuleCollider capsuleCollider;
	public int zombieCount = 100;
	public float groundLevelY = 0.0f;
	public float upForce = 10.0f;
	private Transform[] zombies;
	private Rigidbody[] zombieRigidbodies;

	// Use this for initialization
	void Start () {
		float radius = capsuleCollider.radius;
		float height = capsuleCollider.height;
		zombies = new Transform[zombieCount];
		zombieRigidbodies = new Rigidbody[zombieCount];
		for (int i = 0; i < zombieCount; i++)
		{
			// assume capsule along Y
			Vector2 circle = Random.insideUnitCircle * radius;
			Vector3 localPos = new Vector3(circle.x,Random.Range(-height/2.0f, height/2.0f), circle.y);
			Quaternion localRot = Quaternion.LookRotation(Random.onUnitSphere);
			Transform prefab = zombiePrefabs[Random.Range(0, zombiePrefabs.Length)];
			Transform zombie = Instantiate(prefab) as Transform;
			zombie.localScale = Vector3.one * Random.Range(0.8f, 1.5f);
			zombie.SetParent(transform);
			zombie.localPosition = localPos;
			zombie.localRotation = localRot;
			zombies[i] = zombie;
			zombieRigidbodies[i] = zombie.GetComponent<Rigidbody>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < zombieCount; i++)
		{
			Transform zombie = zombies[i];
			Vector3 position = zombie.position;
			// make sure we're above the ground
			if (position.y < groundLevelY)
			{
				position.y = groundLevelY;

//				Vector3 velocity = zombieRigidbodies[i].velocity;
//				velocity.y = upForce * Random.value;
//				zombieRigidbodies[i].velocity = velocity;

//				zombie.position = Vector3.Lerp(zombie.position, position, Time.deltaTime * 10.0f);
			}
		}
	}
}
