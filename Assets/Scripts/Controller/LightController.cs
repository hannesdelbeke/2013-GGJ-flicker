using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour {
	//All Lights in scene collected in 1 array
	private ArrayList SceneLights;
	private ArrayList SceneLightIntensity;
	private ArrayList ChangingSceneLights;
	private ArrayList ChangingSceneLightsIntensity;
	//timing interval
	public float defTimeIntervalOn = 1.0f;
	private float TimeIntervalOn;
	public float defTimeIntervalOff = 1.5f;
	private float TimeIntervalOff;
	//StopWatch -> LightTimings
	private Stopwatch LightTimer;
	private bool DayLight = true;
	//Light Switch parameters
	public float LightSwitchFactor = 0.95f;
	//Ambient Light parameters
	private Color AmbientLightDay = new Color(0.0f,0.0f,0.0f,1.0f);
	private Color curAmbientLight;
	public CameraSmoothFollowController m_camera;
	
	// Use this for initialization
	void Start () {
		Light[] sceneLights = (Light[])FindObjectsOfType(typeof(Light));
		SceneLights = new ArrayList();
		SceneLightIntensity = new ArrayList();
		ChangingSceneLights = new ArrayList();
		ChangingSceneLightsIntensity = new ArrayList();
		foreach(Light obj in sceneLights)
		{
			if(!obj.CompareTag("no EMP"))
			{
				SceneLightIntensity.Add(obj.intensity);
				SceneLights.Add(obj);
			}
		}
		TimeIntervalOn = defTimeIntervalOn;
		TimeIntervalOff = defTimeIntervalOff;
		//Start Timer (stopwatch for lighting)
		LightTimer = gameObject.AddComponent("Stopwatch") as Stopwatch;
		LightTimer.ResetStopwatch(TimeIntervalOn,false);
		//SetSceneLights
		curAmbientLight = AmbientLightDay;
		SetSceneLights();
	}
	
	// Update is called once per frame
	void Update () {
		if(!GameController.IsGamePaused())
		{
			if(LightTimer.TimeReached())
			{
				if(DayLight)
				{
					LightTimer.ResetStopwatch(TimeIntervalOff,false);
					DayLight = false;
					GameController.UnfreezeGame();
					//CameraZomeIn (lieven->TODO)	
				}
				SetSceneLights();
			}
			
			if(DayLight)
			{
				if(ChangingSceneLights.Count > 0)
				{
					for(int i = 0 ; i < ChangingSceneLights.Count ;)
					{
						Light obj = (Light)ChangingSceneLights[i];
						obj.intensity += 0.05f;
						if(obj.intensity - (float)ChangingSceneLightsIntensity[i] < 0.1)
						{
							obj.intensity =	(float)ChangingSceneLightsIntensity[i];
							ChangingSceneLights.RemoveAt(i);
							ChangingSceneLightsIntensity.RemoveAt(i);
						}
						else
							++i;
						if(ChangingSceneLights.Count == 0)
							break;
					}
				}
				curAmbientLight += (AmbientLightDay - curAmbientLight) * LightSwitchFactor;
			}
			else
			{
				if(ChangingSceneLights.Count > 0)
				{
					for(int i = 0 ; i < ChangingSceneLights.Count ;)
					{
						Light obj = (Light)ChangingSceneLights[i];
						obj.intensity *= LightSwitchFactor;
						if(obj.intensity < 0.1)
						{
							obj.intensity =	0;
							ChangingSceneLights.Remove(obj);
						}
						else
							++i;
					}
				}
				curAmbientLight *= LightSwitchFactor;
			}
			RenderSettings.ambientLight = curAmbientLight;
		}
	}
	
	private void SetSceneLights()
	{
//		if(on)
//		{
//			int i = 0;
//			foreach(Light obj in SceneLights)
//			{
//				obj.intensity = (float)SceneLightIntensity[i];
//				++i;
//			}
//		}
//		else
//		{
//			foreach(Light obj in SceneLights)
//				obj.intensity = 0;
//		}
		ChangingSceneLights.Clear();
		foreach(Light obj in SceneLights)
			ChangingSceneLights.Add(obj);
		if(DayLight)
		{
			ChangingSceneLightsIntensity.Clear();
			foreach(float obj in SceneLightIntensity)
			{
				ChangingSceneLightsIntensity.Add(obj);
			}
		}
	}
	
	public void ActivateDayLight(float nightTimer, float zoomInTimer, Transform zoomOutPos)
	{
		Debug.Log("ActivateDayLight");
		GameController.FreezeGame();
		LightTimer.ResetStopwatch(nightTimer,true);
		DayLight = true;
		
		m_camera.m_zoomOutTarget = zoomOutPos;
		m_camera.ZoomOut();
		
		Invoke("ZoomIn", zoomInTimer);
	}
	
	public void ActivateDayLight()
	{
		DayLight = true;
		SetSceneLights();
	}
	
	private void ZoomIn()
	{
		m_camera.ZoomOnPlayer();
	}
	
	public bool GetDaylight()
	{
		return DayLight;
	}
}
