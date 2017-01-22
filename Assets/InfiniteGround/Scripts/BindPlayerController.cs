using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindPlayerController : MonoBehaviour {
    private GameObject player;
    public float threshold = 240.0f;

	// Use this for initialization
	void Start () {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Mathf.Abs(transform.position.z - player.transform.position.z);
        if (distance > threshold)
        {
            GroundPoolController.instance.recycleGround(transform);
        }
	}
}
