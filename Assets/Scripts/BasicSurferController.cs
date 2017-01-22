using UnityEngine;
using System.Collections;

public class BasicSurferController : MonoBehaviour {

	public float movementSpeed = 6f;
	public float minRotate = -45.0f;
	public float maxRotate = 45.0f;
	public float rotationSpeed = 3.0f;

	private float angle = 0.0f;
	private float maxSin = 1f;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		maxSin = Mathf.Sin( maxRotate*Mathf.Deg2Rad );
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {
		
		float xInput = Input.GetAxis("Horizontal");
		float targetAngle = Mathf.LerpAngle(minRotate, maxRotate, (xInput * 0.5f) + 0.5f);
		angle = Mathf.Lerp(angle, targetAngle, Time.deltaTime * rotationSpeed);
		transform.localRotation = Quaternion.Euler(new Vector3(0, angle, 0));
		transform.localPosition += Vector3.right * (Mathf.Sin( angle*Mathf.Deg2Rad) / maxSin) * movementSpeed * Time.deltaTime;

	}

	void OnCollisionEnter(Collision other) {
		Debug.Log("test");

		if(other.gameObject.tag == "Obstacle") {
			die();
			Debug.Log("Owch!");
		}

	}

	void die() {

		transform.parent = null;
		rb.constraints = RigidbodyConstraints.None;
		rb.useGravity = true;

		rb.AddForce( new Vector3( (2f*(Random.value-0.5f))* 100f, 
					 			  ((Random.value))*1000f, 
					 			  ((Random.value))*-100f) );
		rb.AddTorque( Vector3.right * 3000f );

	}

}

/*
// more advanced moving script
void Update () {

	// get camera directions
	Quaternion camRot = thirdPersonCamera.rotation;
	Vector3 camDir = camRot*Vector3.forward;
	Vector3 camRight = camRot*Vector3.right;

	// get 2d vectors
	straight = new Vector3(camDir.x, 0, camDir.z).normalized;
	right = new Vector3(camRight.x, 0, camRight.z).normalized;

	// adjust rotation (don't adjust local up direction)
	if(Mathf.Abs(Input.GetAxis("Vertical")) > 0.25f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.25f) {
		Vector3 desiredDir = Input.GetAxis("Vertical") * straight + Input.GetAxis("Horizontal") * right;
		Quaternion desiredRot = Quaternion.LookRotation(desiredDir, transform.up);
		transform.localRotation = desiredRot; // FIXME: Change to be more smooth
	}

}
*/