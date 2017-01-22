using UnityEngine;
using System.Collections;

public class WaveGenerator : MonoBehaviour {

	public Transform waveUnitPrefab;

	public int gridWidth;
	public int gridHeight;

	// Use this for initialization
	void Start () {

		// get dimensions of the waveUnits
		float width = waveUnitPrefab.localScale.x;
		float height = waveUnitPrefab.localScale.z;

		// generate 2D grid of waveUnits
		for(int i = 0; i<gridWidth; i++) {
			for(int j = 0; j<gridHeight; j++) {

				// local position
				Vector3 localPos = new Vector3(i*width, 0f, j*height);		// local positoin
				//Vector3 offset = Vector3.Scale(localRotPos, new Vector3(1f,0f,1f));
				//Vector3 localRotPos = transform.localRotation * localPos;	// local roatated position
				//Vector3 pos = transform.position + offset;				// true position

				// load to screen
				Transform waveUnit = (Transform) Instantiate(waveUnitPrefab);
				waveUnit.parent = transform;
				waveUnit.localPosition = localPos;
				waveUnit.localRotation = Quaternion.identity;
				//waveUnit.parent = null;

			}	
		}

	}
	
	// Update is called once per frame
	void Update () {

		// try to face the player

	}

}
