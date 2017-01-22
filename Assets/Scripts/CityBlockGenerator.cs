using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityBlockGenerator : MonoBehaviour {

	public int numBuildings = 10;
	public Transform plane;
	public Transform[] buildingPrefabs;
	public Transform polePrefab;
	public Transform signPrefab;

	[Header("Building Dimensions:")]
	public float buildingWidthMin  = 10f;
	public float buildingDepthMin  = 10f;
	public float buildingHeightMin = 20f;

	public float buildingWidthMax  = 20f;
	public float buildingDepthMax  = 30f;
	public float buildingHeightMax = 50f;

	private float blockWidth;
	private float blockHeight;
	private Transform playerRef;

	// Use this for initialization
	void Start () {

		// get player
		playerRef = GameObject.FindGameObjectWithTag("Player").transform;

		// dimensions of block
		blockWidth = plane.localScale.x  * 10f;
		blockHeight = plane.localScale.z * 10f;

		// generate buildings in random locations
		for(int i = 0; i <numBuildings; i++ ) {
			
			int b = Random.Range(0, buildingPrefabs.Length);
			Transform building = (Transform) Instantiate(buildingPrefabs[b]);

			// stack building
			for(int j = 1; j < 7; j++) {
				Transform subBuilding = (Transform) Instantiate(buildingPrefabs[b]);
				subBuilding.parent = building;

				float stackVal = (b == 0)? 0.6f : 0.75f ; 

				subBuilding.localPosition = new Vector3(0f, j*stackVal, 0f);
			}

			// scale the building to different sizes
			building.localScale = new Vector3( Random.Range(buildingWidthMin, buildingWidthMax),
											   Random.Range(buildingHeightMin, buildingHeightMax),
											   Random.Range(buildingDepthMin, buildingDepthMax));

			// get location
			Vector3 localPos = new Vector3((blockWidth*Random.value - blockWidth*0.5f), 
										   0f, 
										   (blockHeight*Random.value - blockHeight*0.5f));

			// random chance for a pole
			Transform pole = (Transform) Instantiate(polePrefab);
			pole.parent = transform;
			pole.localPosition = localPos + new Vector3(8f + Random.value*10f, 0f, 5f + Random.value*3f);
			pole.localRotation = (Random.value > 0.5f)? Quaternion.Euler(-90f, 90f, 0f) : Quaternion.Euler(-90f, 90f, 90f);  // jank hard coded

			// random chance for a sign
			Transform sign = (Transform) Instantiate(signPrefab);
			sign.parent = transform;
			sign.localPosition = localPos + new Vector3(-8f - Random.value*10f, 0f, -5f - Random.value*3f);
			sign.localRotation = (Random.value > 0.5f)? Quaternion.Euler(-90f, 90f, 0f) : Quaternion.Euler(-90f, 90f, 90f);  // jank hard coded

			// perform placement
			building.parent = transform;
			building.localPosition = localPos;
			building.localRotation = Quaternion.identity;

		}

	}

}
