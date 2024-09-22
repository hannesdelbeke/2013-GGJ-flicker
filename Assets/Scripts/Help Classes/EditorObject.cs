using UnityEngine;
using System.Collections;

//Class used for objects that should be visible in editor, while hidden in game. 

public class EditorObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
