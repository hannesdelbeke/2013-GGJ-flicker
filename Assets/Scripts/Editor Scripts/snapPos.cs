using UnityEngine;
using System.Collections;

[ExecuteInEditMode]

public class snapPos : MonoBehaviour {
	
	public Vector3 snapPosWorld = new Vector3(0,0,0);
	public bool snapX = false;
	public bool snapY = false;
	public bool snapZ = false;
	
	
	// Update is called once per frame
	void Update () {
	if(snapX)
		{
			Vector3 pos = transform.position;
			pos.x = (snapPosWorld.x);
			transform.position = pos;
		}
	if(snapY)
		{
			Vector3 pos = transform.position;
			pos.y = (snapPosWorld.y);
			transform.position = pos;
		}
	if(snapZ)
		{
			Vector3 pos = transform.position;
			pos.z = (snapPosWorld.z);
			transform.position = pos;
		}
	}
}
