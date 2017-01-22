using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePathPivot : MonoBehaviour {
	
	public float bottomOffset = -1f;

	[Header("Calculated:")]
	public float radius;

	// Use this for initialization
	void Start () {

		// not good --> force it to default
		if(bottomOffset < 0) { bottomOffset = 0f; }
		radius = transform.position.y - bottomOffset;

	}
	
	// Update is called once per frame
	void Update () {

		// not good --> force it to default
		if(bottomOffset < 0) { bottomOffset = 0f; }
		radius = transform.position.y - bottomOffset;

	}

	void OnDrawGizmos() {
		Gizmos.color = Color.white;
		if(bottomOffset >= 0) {
			Gizmos.DrawWireSphere(transform.position, transform.position.y - bottomOffset);
		} else {
			Gizmos.DrawWireSphere(transform.position, transform.position.y);
		}
	}

}
