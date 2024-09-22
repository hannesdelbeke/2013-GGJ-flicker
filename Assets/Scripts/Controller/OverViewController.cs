using UnityEngine;
using System.Collections;

public class OverViewController : MonoBehaviour {
	
	private bool Activated = false;
	private LightController lightController;
	
	public float FreezeTime;
	public float ZoomOutTime;
	public Transform ZoomOutPosition;
	
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
			Activated = true;
			lightController.ActivateDayLight(FreezeTime, ZoomOutTime ,ZoomOutPosition);
		}
    }
}
