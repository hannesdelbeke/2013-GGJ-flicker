using UnityEngine;
using System.Collections;

public class CopLight : MonoBehaviour
{
	public Transform CamTR;
	public float m_rotationSpeed = 3f;
	public int distance = 5;
	public float sideRotationSpeed = 2f;
	public Light light;
	private LightController lightController;
	
	void Start()
	{
		CamTR = Camera.main.transform;
		
		lightController = GameObject.FindObjectOfType(typeof(LightController)) as LightController;
		
	}
	
	// Update is called once per frame
	void Update()
	{
		if(!GameController.IsGamePaused())
		{
			if(!lightController.GetDaylight())
			{
				transform.position = CamTR.position + (CamTR.forward * -distance);
				transform.RotateAround(transform.up, Time.deltaTime * m_rotationSpeed);
				transform.RotateAround(transform.forward, Time.deltaTime * sideRotationSpeed);
			}
			light.enabled = !lightController.GetDaylight();
		}
	}
}
