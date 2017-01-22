using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGroundController : MonoBehaviour {

    public Transform spawnPointLeft;
    public Transform spawnPointMid;
    public Transform spawnPointRight;

    private bool leftGroundSpawned = false;
    private bool midGroundSpawned = false;
    private bool rightGroundSpawned = false;

    private void OnDisable()
    {
        leftGroundSpawned = false;
        midGroundSpawned = false;
        rightGroundSpawned = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (!leftGroundSpawned)
        {
            GroundPoolController.instance.getGround(spawnPointLeft.position);
            leftGroundSpawned = true;
        }

        if (!midGroundSpawned)
        {
            GroundPoolController.instance.getGround(spawnPointMid.position);
            midGroundSpawned = true;    
        }
        if (!rightGroundSpawned)
        {
            GroundPoolController.instance.getGround(spawnPointRight.position);
            rightGroundSpawned = true;
        }
    }
}
