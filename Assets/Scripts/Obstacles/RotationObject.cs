using UnityEngine;
using System.Collections;

public class RotationObject : MonoBehaviour {
	
	public Vector3 RotationSpeed;
	public Vector3 Rotation;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!GameController.IsGamePaused())
		{
			Rotation.x += RotationSpeed.x * Time.deltaTime;
			Rotation.z += RotationSpeed.z * Time.deltaTime;
			Rotation.y += RotationSpeed.y * Time.deltaTime;
			if(RotationSpeed.x != 0)
				transform.rotation = Quaternion.AngleAxis(Rotation.x, transform.right);
			if(RotationSpeed.y != 0)
				transform.rotation = Quaternion.AngleAxis(Rotation.y, transform.up);
			if(RotationSpeed.z != 0)
				transform.rotation = Quaternion.AngleAxis(Rotation.z, transform.forward);
		}
	}
}
