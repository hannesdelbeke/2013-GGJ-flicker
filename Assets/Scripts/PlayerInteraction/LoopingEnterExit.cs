using UnityEngine;
using System;
using System.Collections;

public class LoopingEnterExit : MonoBehaviour
{
	public event EventHandler<EventArgs> Entered;
	public event EventHandler<EventArgs> Exited;
	
	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			if(Entered != null)
				Entered(this, new EventArgs());
		}
	}
	
	private void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			if(Exited != null)
				Exited(this, new EventArgs());
		}
	}
}

