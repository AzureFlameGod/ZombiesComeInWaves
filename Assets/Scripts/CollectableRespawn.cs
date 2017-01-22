using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableRespawn : MonoBehaviour {

	public Vector2 xBounds;
	public float spawnDistMin = 100f;
	public float spawnDistMax = 300f;

	public AudioClip[] collectedSounds;

	private SurferController player;
	private GameController game;

	void Start() {
		
		transform.position = new Vector3( Random.Range(xBounds.x, xBounds.y), 
										  5f,
										  Random.Range(spawnDistMin, spawnDistMax*2f));
		
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<SurferController>();
		game = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

	}

	void OnTriggerEnter(Collider other) {

		if(other.tag == "Player") {
			Debug.Log("excellent");
			game.score++;

			// translate back
			transform.position = new Vector3(Random.Range(xBounds.x, xBounds.y), 
				5f,
				transform.position.z + Random.Range(spawnDistMin, spawnDistMax));

			game.UpdateHud();

			AudioClip clip = collectedSounds[Random.Range(0, collectedSounds.Length)];
			AudioSource otherAudioSource = other.GetComponent<AudioSource>();
			if (otherAudioSource != null)
			{
				otherAudioSource.PlayOneShot(clip);
			}

		}

	}

	void Update() {

		if( transform.position.z < player.transform.position.z - 50f ) {
			// translate back
			transform.position = new Vector3(Random.Range(xBounds.x, xBounds.y), 
				5f,
				transform.position.z + Random.Range(spawnDistMin*1.5f, spawnDistMax*1.75f));
		}

	}

}
