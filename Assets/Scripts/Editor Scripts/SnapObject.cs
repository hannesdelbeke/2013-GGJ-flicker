using UnityEngine;
using System.Collections;

// Make the script also execute in edit mode.
[ExecuteInEditMode]

public class SnapObject : MonoBehaviour {
	
	public float SnapRotation;
	public float SnapTranslation;

	
	// Update is called once per frame
	void Update () {
		int times;
		if(SnapTranslation != 0)
		{
			Vector3 pos = transform.position;
			times = (int)(pos.x/SnapTranslation);
			pos.x = times * SnapTranslation;
			times = (int)(pos.y/SnapTranslation);
			pos.y = times * SnapTranslation;
			times = (int)(pos.z/SnapTranslation);
			pos.z = times * SnapTranslation;
			transform.position = pos;
		}
		if(SnapRotation != 0)
		{
			//TODO : ROTATION!
//			Quaternion rot = transform.rotation;
//			times = (int)(rot.x/SnapRotation);
//			rot.x = times * SnapRotation;
//			times = (int)(rot.y/SnapRotation);
//			rot.y = times * SnapRotation;
//			times = (int)(rot.z/SnapRotation);
//			rot.z = times * SnapRotation;
//			//transform.rotation = Quaternion.identity;
//			transform.Rotate(rot.x,rot.y,rot.z);
		}
	}
}
