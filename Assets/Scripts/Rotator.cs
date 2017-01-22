using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	public float angularSpeed = 1f;
	public Vector3 axis;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		// rotate about axis with speed
		transform.Rotate(axis, angularSpeed * Time.deltaTime);

	}

}
