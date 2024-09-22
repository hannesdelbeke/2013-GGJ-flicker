using UnityEngine;
using System.Collections;

public class DeadZoneScript : MonoBehaviour {
	
	private GameObject game;
	private GameController gameController;
	
	// Use this for initialization
	void Start () {
		game = GameObject.FindGameObjectWithTag("GameController");
		gameController = game.GetComponent("GameController") as GameController;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		if(other.gameObject.CompareTag("Player"))
		{
			gameController.PlayerIsDead();
		}
	}
}
