using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPoolController : MonoBehaviour {
    static public GroundPoolController instance = null;
    public Transform groundPrefab;
    public int poolSize = 20;
    private Transform[] pool; 

    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }
    // Use this for initialization
    void Awake () {
        pool = new Transform[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            Transform ground = Instantiate(groundPrefab, Vector3.zero, Quaternion.identity) as Transform;
            ground.gameObject.SetActive(false);
            pool[i] = ground;
        }
	}
	
    private void OnDisable()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    public Transform getGround (Vector3 position)
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (!pool[i].gameObject.activeSelf)
            {
                pool[i].position = position;
                pool[i].gameObject.SetActive(true);
                return pool[i];
            } 
        }
        return null;
    }

    public void recycleGround (Transform ground)
    {
        ground.position = Vector3.zero;
        ground.gameObject.SetActive(false);
    }
}
