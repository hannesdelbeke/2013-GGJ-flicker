using UnityEngine;
using System.Collections;

public class CheckPointScript : MonoBehaviour {
	//h
	public bool oneTimeActivate = true;	
	private bool _oneTimeActivated = true;	
	public GameObject ParticleEffect;
//	public soundinput
	//h
	
	private bool Active = false;
	private GameObject game;
	private GameController gameController;
	
	public Vector3 SpawnOffset;

	// Use this for initialization
	void Start () {
		game = GameObject.FindGameObjectWithTag("GameController");
		gameController = game.GetComponent("GameController") as GameController;
		
	}
	
	// Update is called once per frame
/*	void Update () {
	
	}
	*/
	void OnTriggerEnter(Collider other) {
		if(!Active || (oneTimeActivate && !_oneTimeActivated) )
		{
			if(other.gameObject.CompareTag("Player"))
			{					
				Active = true;
				SpawnOffset += transform.position;
				gameController.LastCheckPoint = SpawnOffset;
				gameController.CheckPointReached = true; 
				
				//h
				
				//activate particleeffect
				// ParticleEffect. instantiate  
				//activate sound
				
				//onetime
				if( _oneTimeActivated)
				{
					_oneTimeActivated = true;
					Destroy(gameObject);
				}
				//h
			}
		}
	}
}
