using UnityEngine;
using System.Collections;

public class TextGameStart : MonoBehaviour {
	
	private Stopwatch timer;
	
	// Use this for initialization
	void Start () {
		guiText.enabled = false;
		timer = gameObject.AddComponent("Stopwatch") as Stopwatch;
		timer.ResetStopwatch(0.65f,true);
	}
	
	// Update is called once per frame
	void Update () {
		if(timer.TimeReached())
		{
			timer.ResetStopwatch(0.5f,true);	
			guiText.enabled = !guiText.enabled;
		}
	}
}
