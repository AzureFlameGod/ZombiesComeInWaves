using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurferController : MonoBehaviour {

	public float yWave;
	public float speed = 5f;

	public WavePathPivot pathPivot;

	public float horizontalSpeed = 6f;
	public float turnSpeed = 3.0f;
	public float minRotate = -70.0f;
	public float maxRotate = 70.0f;

	public Vector2 xBounds;

	public float freq = 2f;
	public bool moveAuto = true;

	public ParticleSystem blood;

	public AudioClip deathSound;

	[Header("Calculated:")]
	public float turnAngle;

	private float maxSin = 1f;

	private Vector3 offset;	 // vector between me and pivot
	private float y;		 // screen y and x positions
	private float z;

	private float t;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		maxSin = Mathf.Sin( maxRotate*Mathf.Deg2Rad );
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

		float yInput = Input.GetAxis("Vertical");

		float yPivot = pathPivot.transform.localPosition.y;
		float zPivot = pathPivot.transform.localPosition.z;
		float radius = pathPivot.radius;

		// move
		//yWave -= yInput * (speed / radius) * Time.deltaTime;
		yWave = Mathf.Sin( t )*0.5f + 0.47f;
		t += Time.deltaTime * freq / 10f;

		// boundaries
		if( yWave < -1f) { yWave = -1f; }
		if( yWave > Mathf.PI*0.5f) { yWave = Mathf.PI*0.5f; }

		float angle = yWave + Mathf.PI*0.5f;

		// traversal: allow bottom traversal
		if ( yWave > 0 ) {
			y = yPivot - Mathf.Sin( angle ) * radius;
			z = zPivot + Mathf.Cos( angle ) * radius;
		}else{
			y = yPivot - radius;
			z = zPivot + Mathf.Cos( angle ) * radius;
		}

		// get angle: get angle to look upwards
		if(yWave > 0) {
			offset = pathPivot.transform.localPosition - transform.localPosition;
			offset = Vector3.Scale(offset, new Vector3(0f, 3f, 1f)).normalized;
		} else {
			offset = Vector3.up;
		}

		// turning
		float xInput = Input.GetAxis("Horizontal");
		float targetAngle = Mathf.LerpAngle(minRotate, maxRotate, (xInput * 0.5f) + 0.5f);
		turnAngle = Mathf.Lerp(turnAngle, targetAngle, Time.deltaTime * turnSpeed);

	}

	void FixedUpdate() {

		// set values
		transform.localPosition = new Vector3(transform.localPosition.x, y, z);
		transform.up = offset;

		// set rotation values (localRotation * up = local up aixs)
		transform.localRotation *= Quaternion.AngleAxis(turnAngle, transform.localRotation * Vector3.up);

		// set horizontal values (Modified from transform.localPosition)
		transform.localPosition += Vector3.right * (Mathf.Sin( turnAngle*Mathf.Deg2Rad) / maxSin) * horizontalSpeed * Time.deltaTime;

		// boundaries
		if(transform.localPosition.x > xBounds.y) {
			transform.localPosition = Vector3.Scale(transform.localPosition, new Vector3(0f,1f,1f)) + Vector3.right*xBounds.y;
		}
		if(transform.localPosition.x < xBounds.x) {
			transform.localPosition = Vector3.Scale(transform.localPosition, new Vector3(0f,1f,1f)) + Vector3.right*xBounds.x;
		}

	}

	void OnCollisionEnter(Collision other) {
		//Debug.Log("test");

		if(other.gameObject.tag == "Obstacle") {
			die();
//			Debug.Log("Owch!");
		}

	}

	void die() {

		transform.parent = null;
		rb.constraints = RigidbodyConstraints.None;
		rb.useGravity = true;

		if (blood != null)
		{
			ParticleSystem bloodInstance = Instantiate(blood) as ParticleSystem;
			bloodInstance.transform.position = transform.position;

			AudioSource.PlayClipAtPoint(deathSound, transform.position);
		}

		GameController.instance.ShowRestartGUI();

		this.gameObject.SetActive(false);


		/*
		rb.AddForce( new Vector3( (2f*(Random.value-0.5f))* 100f, 
			((Random.value))*1000f, 
			((Random.value))*-100f) );
		rb.AddTorque( Vector3.right * 3000f );
		*/
	}


}
