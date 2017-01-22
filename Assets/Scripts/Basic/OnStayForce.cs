using UnityEngine;
using System.Collections;

public class OnStayForce : MonoBehaviour {

	public Vector3 forward;

	public float upIntensity;
	public float forwardIntensity;

	// Use this for initialization
	void Start () {
		forward = transform.forward;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider other) {

//		Debug.Log("Hit!");
		Rigidbody rb = other.attachedRigidbody;
		if(rb) {
			rb.AddForce(forward * forwardIntensity + Vector3.up * upIntensity);

		}

	}
}
