using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIFader : MonoBehaviour {

	public Color targetColor;
	public Color currentColor;
	public float lerpRate = 1.0f;
	public bool useUnscaledTime = false;

	public Graphic ui = null;

	public const float EQUAL_THRESHOLD = 0.001f;

	void Awake()
	{
        if (ui == null)
        {
            ui = GetComponent<Graphic>();
        }
	}
	// Use this for initialization
	void Start () {
        ui.color = currentColor;		
	}
	
	// Update is called once per frame
	void Update () {
		if (currentColor != targetColor)
		{
			currentColor = Color.Lerp (currentColor, targetColor, (useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime) * lerpRate);
			if (CloseEnough(currentColor, targetColor))
			{
				currentColor = targetColor;
			}
		}

		if (ui.color != currentColor)
		{
			ui.color = currentColor;
		}
	}

	public void SetTargetAlpha(float alpha)
	{
		targetColor.a = alpha;
	}
	public void SetCurrentAlpha(float alpha)
	{
		currentColor.a = alpha;
	}

	public static bool CloseEnough(Color c1, Color c2)
	{
		return 
			Mathf.Abs (c1.r - c2.r) < EQUAL_THRESHOLD
			&& Mathf.Abs (c1.g - c2.g) < EQUAL_THRESHOLD
			&& Mathf.Abs (c1.b - c2.b) < EQUAL_THRESHOLD
			&& Mathf.Abs (c1.a - c2.a) < EQUAL_THRESHOLD;

	}
}
