using UnityEngine;
using System.Collections;

public class ThirdPersonPlayerController : MonoBehaviour {

	public float speed = 10f;
	public float jumpPower = 10f;
	public float rotSpeed = 1000f;
	public Transform thirdPersonCamera;

	private Rigidbody rb;
	private Vector3 straight;
	private Vector3 right;

	private Vector3 moveDir;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate() 
	{
		// movement
		rb.AddForce( moveDir*speed );
	}

	// Update is called once per frame
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
			Quaternion rotStep = Quaternion.RotateTowards(transform.rotation, desiredRot, rotSpeed * Time.deltaTime);
			rb.MoveRotation( rotStep );
		}

		// get moveDir and clamp magnitude
		moveDir = (straight*Input.GetAxis("Vertical") + right*Input.GetAxis("Horizontal"));
		if(moveDir.magnitude > 1f) { moveDir.Normalize(); }

		/*
		// jumping
		// if button down and there is something collider with the bottom of me
		if( Input.GetButtonDown("Jump") && canJump) 
		{
			rb.AddForce( Vector3.up*jumpPower*100f );
			canJump = false;
		}
		*/

		//constrain camera

		// draw gizmos
		Debug.DrawRay(transform.position, camDir, Color.blue);
		Debug.DrawRay(transform.position, straight, Color.black);
		Debug.DrawRay(transform.position, right, Color.red);

		// draw some basic gizmos
		Vector3 myStraight = this.transform.rotation * Vector3.forward;
		Vector3 myRight = this.transform.rotation * Vector3.right;
		Vector3 myUp = this.transform.rotation * Vector3.up;
		Debug.DrawRay(transform.position, myStraight, Color.white);
		Debug.DrawRay(transform.position, myRight, Color.yellow);
		Debug.DrawRay(transform.position, myUp, Color.magenta);


	}

}
