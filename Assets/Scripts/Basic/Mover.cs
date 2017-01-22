using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {
	
	public Vector3 direction;
	public float speed;

	public bool moveForward;

	// Use this for initialization
	void Start () {
		direction = direction.normalized;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(moveForward) {
			transform.position += (transform.localRotation * Vector3.forward) * speed * Time.deltaTime;
		}else{
			transform.position += direction * speed * Time.deltaTime;
		}
	}

}
