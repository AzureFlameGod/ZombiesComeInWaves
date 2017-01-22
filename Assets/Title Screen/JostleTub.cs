using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JostleTub : MonoBehaviour {

	public Vector3 ellipsoid;
	public float minInterval = 0.2f;
	public float maxInterval = 1.0f;
	public float jostleAngle = 2.0f;
	private Vector3 basePosition;
	private Vector3 startPosition;
	private Vector3 endPosition;

	private float nextTime = 0.0f;
	private float startTime = 0.0f;

	private Quaternion baseRotation, startRotation, endRotation;

	// Use this for initialization
	void Start () {
		basePosition = transform.position;
		baseRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextTime)
		{
			startTime = Time.time;
			nextTime = Time.time + Random.Range(minInterval, maxInterval);
			startPosition = transform.position;
			endPosition = basePosition + Vector3.Scale(ellipsoid, Random.insideUnitCircle);
			startRotation = transform.rotation;
			endRotation = baseRotation * Quaternion.Euler(
				Random.Range(-jostleAngle, jostleAngle),
				Random.Range(-jostleAngle, jostleAngle),
				Random.Range(-jostleAngle, jostleAngle));
		}

		float lerp = Mathf.InverseLerp(startTime, nextTime, Time.time);
		transform.position = Vector3.Lerp(startPosition, endPosition, lerp);
		transform.rotation = Quaternion.Slerp(startRotation, endRotation, lerp);
	}
}
