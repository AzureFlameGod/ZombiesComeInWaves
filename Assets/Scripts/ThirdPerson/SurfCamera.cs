using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfCamera : MonoBehaviour {

	public SurferController surfer;

	private Vector3 desiredPos;

	// Use this for initialization
	void Start () {
		
	}

	private float bobobo;
	private float aPrev;

	// Update is called once per frame
	void LateUpdate () {

		// offset values
		float a = (surfer.turnAngle / surfer.maxRotate); // normalized character rotation
		float absA = Mathf.Abs(bobobo) - Mathf.Abs(a);

		if (absA > 0)
		{
			bobobo = Mathf.Lerp(bobobo, a, Time.deltaTime * 5.0f);
		}
		else
		{
			bobobo = Mathf.Lerp(bobobo, a, Time.deltaTime * 1f);
		}

		float xOffset = bobobo * 3f;
		float yOffset = Mathf.Abs(bobobo) * 3f;
		float zOffset = Mathf.Abs(bobobo) * 8f;
		
		// move left and right
		desiredPos = surfer.transform.position + new Vector3(-xOffset, 1.6f - yOffset, -4f + zOffset);
		transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * 0.51f);

		// force x, y, and z
		transform.position = Vector3.Scale(transform.position, new Vector3(0f,1f,1f)) + Vector3.right*desiredPos.x;
		transform.position = Vector3.Scale(transform.position, new Vector3(1f,0f,1f)) + Vector3.up*desiredPos.y;
		transform.position = Vector3.Scale(transform.position, new Vector3(1f,1f,0f)) + Vector3.forward*desiredPos.z;

		// look at player
		transform.LookAt( surfer.transform.position + lookOffset);

	}

	public Vector3 lookOffset;
}
