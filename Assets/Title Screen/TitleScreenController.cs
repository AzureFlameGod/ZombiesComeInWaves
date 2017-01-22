using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour {

	public AudioClip playSound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartGame()
	{
	
		AudioSource.PlayClipAtPoint(playSound, Vector3.zero);
		SceneManager.LoadSceneAsync(1);
	}
}
