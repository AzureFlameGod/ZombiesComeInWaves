using UnityEngine;
using System.Collections;

public class ExtendedFlyCam : MonoBehaviour
{

	public float cameraSensitivity = 90;
	public float climbSpeed = 4;
	public float normalMoveSpeed = 10;
	public float slowMoveFactor = 0.25f;
	public float fastMoveFactor = 3;
	
	public float rotationX = 0.0f;
	public float rotationY = 0.0f;
	
	void Start ()
	{
	}

	void OnEnable()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	void OnDisable()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
	void Update ()
	{
		Vector3 forwardVector = transform.rotation * Vector3.forward;
		transform.rotation = Quaternion.LookRotation (forwardVector, Vector3.up);

		transform.rotation *= Quaternion.AngleAxis((Input.GetAxis ("Mouse X")) * cameraSensitivity * Time.unscaledDeltaTime, Vector3.up);
		transform.rotation *= Quaternion.AngleAxis((Input.GetAxis ("Mouse Y")) * cameraSensitivity * Time.unscaledDeltaTime, Vector3.left);

		float forwardBack = 0.0f;
		float leftRight = 0.0f;
		if (Input.GetKey (KeyCode.W)) forwardBack += 1.0f;
		if (Input.GetKey (KeyCode.S)) forwardBack -= 1.0f;
		if (Input.GetKey (KeyCode.A)) leftRight -= 1.0f;
		if (Input.GetKey (KeyCode.D)) leftRight += 1.0f;

		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift))
		{
			transform.position += transform.forward * (normalMoveSpeed * fastMoveFactor) * forwardBack * Time.unscaledDeltaTime;
			transform.position += transform.right * (normalMoveSpeed * fastMoveFactor) * leftRight * Time.unscaledDeltaTime;
		}
		else if (Input.GetKey (KeyCode.LeftControl) || Input.GetKey (KeyCode.RightControl))
		{
			transform.position += transform.forward * (normalMoveSpeed * slowMoveFactor) * forwardBack * Time.unscaledDeltaTime;
			transform.position += transform.right * (normalMoveSpeed * slowMoveFactor) * leftRight * Time.unscaledDeltaTime;
		}
		else
		{
			transform.position += transform.forward * normalMoveSpeed * forwardBack * Time.unscaledDeltaTime;
			transform.position += transform.right * normalMoveSpeed * leftRight * Time.unscaledDeltaTime;
		}
		
		if (Input.GetKey (KeyCode.E)) {transform.position += transform.up * climbSpeed * Time.unscaledDeltaTime;}
		if (Input.GetKey (KeyCode.Q)) {transform.position -= transform.up * climbSpeed * Time.unscaledDeltaTime;}


		if (Input.GetKeyDown(KeyCode.Equals))
		{
			Camera.main.fieldOfView += 5.0f;
		}

		if (Input.GetKeyDown (KeyCode.Minus))
		{
			Camera.main.fieldOfView -= 5.0f;
		}

		if (Input.GetKeyDown (KeyCode.F))
		{
			RenderSettings.fog = !RenderSettings.fog;
		}
	}
}