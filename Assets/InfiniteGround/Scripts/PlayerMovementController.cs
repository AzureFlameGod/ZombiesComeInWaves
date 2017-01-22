using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {

    public float movementSpeed = 10.0f;
    public float minRotate = -60.0f;
    public float maxRotate = 60.0f;
    public float rotationSpeed = 10.0f;

    private float angle = 0.0f;

	// Update is called once per frame
	void Update () {
        float xInput = Input.GetAxis("Horizontal");
        float targetAngle = Mathf.LerpAngle(minRotate, maxRotate, (xInput * 0.5f) + 0.5f);
        angle = Mathf.Lerp(angle, targetAngle, Time.deltaTime * rotationSpeed);
        transform.eulerAngles = new Vector3(0, angle, 0);
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    }
}
