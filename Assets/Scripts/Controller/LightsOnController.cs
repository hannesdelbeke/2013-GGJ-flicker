using UnityEngine;
using System.Collections;

public class LightsOnController : MonoBehaviour {
	
	private bool Activated = false;
	private LightController lightController;
	
	// Use this for initialization
	void Start () {
		lightController = GameObject.FindObjectOfType(typeof(LightController)) as LightController;
	}
	
	// Update is called once per frame
	void Update () {
		if(!GameController.IsGamePaused())
		{
		}
	}
	
	void OnTriggerEnter(Collider collision) {
		if(!Activated && collision.gameObject.CompareTag("Player"))
		{
			Debug.Log("ActivateDaylight");
			Activated = true;
			lightController.ActivateDayLight();
		}
    }
}
