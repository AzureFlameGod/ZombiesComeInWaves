using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenshotPauser : MonoBehaviour {

	public int superSize = 10;

	public bool paused = false;

	public KeyCode pauseKey = KeyCode.P;
	public KeyCode toggleEnvironmentKey = KeyCode.O;

	public GameObject[] environment;
	public GameObject screenshotPanel;

	public float normalTimeScale = 1.0f;
	[SerializeField]
	public UnityEvent onPause;

	[SerializeField]
	public UnityEvent onResume;

	// Use this for initialization
	void Start () {
		Time.timeScale = normalTimeScale;
	}

	public void Restart() {
		SceneManager.LoadScene(0);
	}
	public void Toggle()
	{
		if (paused)
		{
			paused = false;
			Time.timeScale = normalTimeScale;
			onResume.Invoke();
		}
		else
		{
			paused = true;
			Time.timeScale = 0.0f;
			onPause.Invoke ();
		}
	}

	public void Quit()
	{
		Application.Quit();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(pauseKey))
		{
			Toggle();
		}
		if (takeHiResShot)
		{
			ticks++;
			if (screenshotTaken && ticks > 5)
			{
				takeHiResShot = false;
				screenshotTaken = false;
				ticks = 0;

				if (screenshotPanel != null)
				{
					screenshotPanel.SetActive(true);
				}
			}
		}


		if (paused && !takeHiResShot && Input.GetKeyDown(KeyCode.Return))
		{
			#if UNITY_STANDALONE
				takeHiResShot = true;
				screenshotTaken = false;
				if (screenshotPanel != null)
				{
					screenshotPanel.SetActive(false);
				}
				ticks = 0;
			#else
				if (screenshotLocationText != null)
				{
					screenshotLocationText.text = "Sorry, the screenshot key only works for standalone builds.\nYou'll need to manually take a screenshot.";
				}
			#endif
		}


		if (Input.GetKeyDown (toggleEnvironmentKey))
		{
			for(int i = 0; i < environment.Length; i++)
			{
				environment[i].SetActive (!environment[i].activeSelf);
			}
		}
	}
	int ticks = 0;

	bool takeHiResShot = false;
	bool screenshotTaken = false;

	public static string ScreenShotName() {

		return string.Format("{0}/screenshot_{1}.png", 
			System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop),
		                     System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
	}
	
	public void TakeHiResShot() {
		takeHiResShot = true;
	}

	public AudioClip cameraSound;

	void LateUpdate() {
		if (takeHiResShot && ticks > 5 && !screenshotTaken) {
			string filename = ScreenShotName();
			Application.CaptureScreenshot (filename, superSize);
            if (cameraSound != null)
            {
                AudioSource.PlayClipAtPoint(cameraSound, Camera.main.transform.position, 1.0f);
            }
			Debug.Log(string.Format("Took screenshot to: {0}", filename));

			screenshotTaken = true;
			ticks = 0;

			if (screenshotLocationText != null)
			{
				screenshotLocationText.text = "Captured: " + filename;
			}
		}
	}

	public Text screenshotLocationText;

}
