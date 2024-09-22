using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sphincter : MonoBehaviour
{
	public List<Transform> AssBlades;
	public int AssHoleOpening = 0;
	public float OpenSpeed = 1f;
	public int MaxAssOpening = 15;
	public bool AssIsOpening = false;
	public bool AssIsClosing = false;
		
	// Update is called once per frame
	void Update () 
	{
		if(!GameController.IsGamePaused())
		{
			if(AssIsOpening)
			{
				OpenAss();
			}
			else if(AssIsClosing)
			{
				CloseAss();
			}
		}
	}
	
	public void OpenAss()
	{
		if(AssHoleOpening < MaxAssOpening)
		{
			for(int i=0; i < AssBlades.Count; i++)
			{
				AssBlades[i].RotateAround(transform.up, Time.deltaTime * OpenSpeed);
			}
			
			AssHoleOpening++;
		}
	}
	
	public void CloseAss()
	{
		if(AssHoleOpening > 0)
		{
			for(int i=0; i < AssBlades.Count; i++)
			{
				AssBlades[i].RotateAround(transform.up, Time.deltaTime * -OpenSpeed);
			}
			
			AssHoleOpening--;		
		}
	}
}
