using UnityEngine;
using System.Collections;

public class GameStartScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKey)
		{
			audio.Play();
			Application.LoadLevel (1); 	
		}
	}
}
