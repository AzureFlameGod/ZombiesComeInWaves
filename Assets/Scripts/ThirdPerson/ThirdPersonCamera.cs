using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {

	public Transform target;			// target to look at

	public float offsetDist = 10f;		// how far away cam is from target
	public float sensitivityX = 2f;		// how much the camera moves based on mouse movement
	public float sensitivityY = 1.5f;	// how much the camera moves based on mouse movement

	[Header("Limits on Y position")]
	public float yMax = 50f;			// clamps for camPosY
	public float yMin = -50f;

	[Header("Y Zoom Clamps (0 to 1)")]
	public float minZoom = 0.2f;
	public float maxZoom = 0.6f;

	[Header("Where camera is on sphere")]
	public float camPosX = 0.0f;		// where the camera is on the "looking sphere" in degrees
	public float camPosY = 0.0f;



	// Use this for initialization
	void Start () {					
		camPosX = target.rotation.y;  // move behind player

	}

	// Update is called once per frame
	void Update () 
	{

		//lock the mouse inside the game
		Cursor.lockState = CursorLockMode.Locked;

		// get mouse velocity info
		float incX = Input.GetAxis("Mouse X");
		float incY = Input.GetAxis("Mouse Y");

		// add to the camera position "on the sphere" --> in degrees
		camPosX += incX*sensitivityX*Time.deltaTime*60f;
		camPosY -= incY*sensitivityY*Time.deltaTime*60f;

		// clamp camPosY
		camPosY = Mathf.Clamp(camPosY, yMin, yMax);

	}

	// Late update always called after update (also once per frame)
	void LateUpdate () 
	{
		
		// get offset angle in euler degrees
		Quaternion rot = Quaternion.Euler(camPosY, camPosX, 0);

		// get desired distance (by normalizing from 0f to 1f)
		float dist = offsetDist * Mathf.Clamp( (camPosY - yMin) / (yMax - yMin) , minZoom, maxZoom);

		// raycast to check for wall collisions
		/*
		bool isObstacle = false;
		RaycastHit hit;
		if( Physics.Raycast(target.position, transform.position-target.position, out hit, dist) )
		{
			Debug.Log("Obstacle hit: " + hit.distance);
			dist = hit.distance;
			isObstacle = true;
		}
		*/

		// clamped distance (to not go under the ground)
		float distClamped =  dist; //(isObstacle)? dist : Mathf.Clamp(dist, 2.5f, offsetDist);


		// create vector for camera movement
		Vector3 dir = new Vector3(0, 0, -distClamped);

		// move camera --> forced movement
		transform.position = target.position + rot*dir;
		//transform.position = Vector3.Lerp(transform.position, target.position + rot * dir, 10f*Time.deltaTime);

		transform.LookAt(target);

		// draw gizmos
		Debug.DrawRay(target.position, rot*Vector3.right, Color.red);
		Debug.DrawRay(target.position, rot*Vector3.up, Color.green);
		Debug.DrawRay(target.position, rot*Vector3.forward, Color.blue);

	}

}
