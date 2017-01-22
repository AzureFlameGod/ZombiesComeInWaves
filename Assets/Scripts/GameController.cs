using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour {

	public static GameController instance = null;
	public int score;
	public Text scoreText;

	public GameObject restartGUI;

	public void OnEnable()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void OnDisable()
	{
		if (instance == this)
		{
			instance = null;
		}
	}

	public void UpdateHud() {
		scoreText.text = "" + score;
	}

	public void ShowRestartGUI()
	{
		restartGUI.SetActive(true);	
	}

	public void RestartLevel()
	{
		SceneManager.LoadSceneAsync(1);
	}
		
	public void Quit()
	{
		Application.Quit();
	}
}
