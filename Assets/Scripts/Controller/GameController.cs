using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	public Vector3 LastCheckPoint;
	static public GameObject Player;
	public bool PlayerIsAlive = true;
	public float GlobalDeathZoneY = -10;
	static public bool ShowEditorObjects = false;
	private GameObject startPoint;
	public bool CheckPointReached = false;
	private LoopingScript[] loopings;
	private GameObject FadePlane;
	private bool FadeGame = false;
	private float BlackTime = 0;
	static private bool GameIsActive = true; 
	private Vector3 startUpPos ;
	
	static private bool GamePause = false;
	
	// Use this for initialization
	void Start () {
		loopings = GameObject.FindObjectsOfType(typeof(LoopingScript)) as LoopingScript[];
		Player = GameObject.FindGameObjectWithTag("Player");
		startPoint = GameObject.FindGameObjectWithTag("StartPoint");
		if(startPoint)			Player.transform.position = startPoint.transform.position;
		FadePlane = GameObject.FindGameObjectWithTag("FadePlane");
		startUpPos = Player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(!IsGamePaused())
		{
			if(!PlayerIsAlive || Player.transform.position.y < GlobalDeathZoneY)
				PlayerIsDead();
			if(FadeGame)
			{
				FadePlane.renderer.material.color = new Color(1.0f,1.0f,1.0f,-FadePlane.renderer.material.color.a+((1-FadePlane.renderer.material.color.a)*0.5f));	
				if(FadePlane.renderer.material.color.a > 0.99f)
					ResetPlayer();
			}
			else
			{
				if(BlackTime > 1.5f)
				{
					FadePlane.renderer.material.color = new Color(1.0f,1.0f,1.0f,FadePlane.renderer.material.color.a*0.95f);	
				}
				else
					BlackTime += Time.deltaTime;
			}
		}
	}
	
	public void PlayerIsDead()
	{
		//PlayerDead Stuf...
		FadeGame = true;
		BlackTime = 0;
		GameIsActive = false;
		
		//Reset The Player to restart at last save point
		//ResetPlayer();
	}
	
	void ResetPlayer()
	{
		foreach(LoopingScript looping in loopings)
		{
			looping.Reset();
		}
		
		if(!CheckPointReached && startPoint)
		{
			Player.transform.position = startPoint.transform.position;
		}
		if(!CheckPointReached && !startPoint)
		{
			Debug.Log ("no start point");
			//use default
			Player.transform.position =  startUpPos;
		}
		else
		{
			Player.transform.position = LastCheckPoint;
		}
		PlayerIsAlive = true;
		GameIsActive = true;
		FadeGame = false;
	}
	
	static public bool IsGameActive()
	{
		return GameIsActive;	
	}
	
	static public void FreezeGame()
	{
		GameIsActive = true;	
	}
	
	static public void UnfreezeGame()
	{
		GameIsActive = false;	
	}
	
	static public void PauseGame()
	{
		GamePause = true;	
	}
	
	static public void UnPauseGame()
	{
		GamePause = false;
	}
	
	static public bool IsGamePaused()
	{
		return GamePause;	
	}
}
