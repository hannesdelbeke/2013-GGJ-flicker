using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		float height = Screen.height;
		float width = 16f/9f * height;
		
		guiTexture.pixelInset = new Rect(-width * 0.5f, -height * 0.5f, width, height);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
