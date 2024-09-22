using UnityEngine;
using System.Collections;

public class Stopwatch : MonoBehaviour {
	private float currentTime = 0.0f;
	private float TargetTime = 0.0f;
	private bool Active = false;
	
	public Stopwatch(float targetTime, bool startWatch = false)
	{
		TargetTime = targetTime;
		Active = startWatch;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!GameController.IsGamePaused())
		{
			if(Active)
				currentTime += Time.deltaTime;
		}
	}
	
	public bool TimeReached()
	{
		return Active && currentTime >= TargetTime;
	}
	
	public void ResetStopwatch(bool restartWatch = false)
	{
		currentTime = 0.0f;
		Active = restartWatch;
	}
	
	public void ResetStopwatch(float targetTime, bool restartWatch = false)
	{
		TargetTime = targetTime;
		ResetStopwatch(restartWatch);
	}
	
	public void SetActive(bool active = true)
	{
		Active = active;	
	}
	
	public void SetTargetTime(float targetTime)
	{
		TargetTime = targetTime;
	}
}
