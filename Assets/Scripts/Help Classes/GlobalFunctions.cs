using UnityEngine;
using System.Collections;

public class GlobalFunctions : MonoBehaviour {
	
	static public float Rad90 = 1.57f;
	static public float Rad180 = 3.14f;
	static public float Rad270 = 4.71f;
	static public float Rad360 = 6.28f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	static public Vector3 ComponentVectorMultiply(Vector3 v1, Vector3 v2)
	{
		Vector3 v = v1;
		v.x *= v2.x;
		v.y *= v2.y;
		v.z *= v2.z;
		return v;
	}
	
	static public float Max(float x, float y)
	{
		return x > y ? x : y;	
	}
	
	static public float Min(float x, float y)
	{
		return x < y ? x : y;	
	}
}
