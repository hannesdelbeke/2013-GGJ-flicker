using UnityEngine;
using System.Collections;

public class ElevatorController : MonoBehaviour {
	
	public Vector3 Speed;
	public Vector3 StartPosition;
	public Vector3 EndPosition;
	
	public Vector3 ReverseScalar = new Vector3(1.0f,1.0f,1.0f);
	
	private bool PlayerSnapped = false;
	
	public float RotationSpeed;
	public float Rotation;
	
	//Nodes
	public GameObject[] PathNodes;
	private int curNodeIndex = 0;
	public bool CurvedPath = false;
	private Vector2 PathCenter;
	private Vector3 TargetNode;
	private float curAngle;
	private float targetangle;
	private float SoloDistance;
	
	// Use this for initialization
	void Start () {
		if(PathNodes.Length != 0)
		{
//				TargetNode = PathNodes[curNodeIndex-1].transform.position;
//				PathCenter.x = PathNodes[curNodeIndex].transform.position.x;
//				PathCenter.y = PathNodes[curNodeIndex-1].transform.position.y;
//				curAngle = GlobalFunctions.Rad270;
//				targetangle = GlobalFunctions.Rad360;
			if(PathNodes.Length == 1)
				SoloDistance = (transform.position - PathNodes[0].transform.position).magnitude;	
			else
			{
				transform.position = PathNodes[curNodeIndex].transform.position;
				TargetNode = transform.position;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(!GameController.IsGamePaused())
		{
			//Classical Method for Moving platforms
			if(PathNodes.Length == 0)
			{
				Vector3 position =  transform.position;
				Vector3 speed = GlobalFunctions.ComponentVectorMultiply(Speed,ReverseScalar) *  Time.deltaTime;
				position += speed;
				if(PlayerSnapped)
				{
					Vector3 playerPos = GameController.Player.transform.position;
					playerPos += speed;
					GameController.Player.transform.position = playerPos;
				}
				transform.position = position;
				if(Speed.x != 0 && (transform.position.x >= EndPosition.x || transform.position.x <= StartPosition.x))
					ReverseScalar.x *= -1;
				if(Speed.y != 0 && (transform.position.y >= EndPosition.y || transform.position.y <= StartPosition.y))
					ReverseScalar.y *= -1;
				if(Speed.z != 0 && (transform.position.z >= EndPosition.z || transform.position.z <= StartPosition.z))
					ReverseScalar.z *= -1;
			}
			else // Or folow a fixed path
			{
				//Next Node + Logic
				if(PathNodes.Length != 1 && 
					((TargetNode - transform.position).magnitude < 0.1f || Mathf.Abs(curAngle-targetangle) < 0.2f))
				{
					if(Speed.x >= 0)
					{
						++curNodeIndex;
						if(curNodeIndex >= PathNodes.Length)
						{
							curNodeIndex = 0;
						}
						if(curNodeIndex == PathNodes.Length-1)
							TargetNode = PathNodes[0].transform.position;
						else
							TargetNode = PathNodes[curNodeIndex+1].transform.position;
					}
					else
					{
						--curNodeIndex;
						if(curNodeIndex < 0)
							curNodeIndex = PathNodes.Length-1;	
						if(curNodeIndex == 0)
							TargetNode = PathNodes[PathNodes.Length-1].transform.position;
						else
							TargetNode = PathNodes[curNodeIndex-1].transform.position;
					}
					//Get Center;
					if(TargetNode.x > PathNodes[curNodeIndex].transform.position.x)
					{
						if(TargetNode.y > PathNodes[curNodeIndex].transform.position.y)
						{
							curAngle = GlobalFunctions.Rad90;
							targetangle = 0;
						}
						else
						{
							curAngle = GlobalFunctions.Rad180;
							targetangle = GlobalFunctions.Rad90;
						}
					}
					else
					{
						if(TargetNode.y > PathNodes[curNodeIndex].transform.position.y)
						{
							curAngle = GlobalFunctions.Rad360;
							targetangle = GlobalFunctions.Rad270;
						}
						else
						{
							curAngle = GlobalFunctions.Rad270;
							targetangle = GlobalFunctions.Rad180;
						}
					}
					if(Speed.x >= 0)
					{
						PathCenter.x = GlobalFunctions.Min(PathNodes[curNodeIndex].transform.position.x,TargetNode.x);
						PathCenter.y = GlobalFunctions.Max(PathNodes[curNodeIndex].transform.position.y,TargetNode.y);
					}
					else
					{
						PathCenter.x = GlobalFunctions.Max(PathNodes[curNodeIndex].transform.position.x,TargetNode.x);
						PathCenter.y = GlobalFunctions.Min(PathNodes[curNodeIndex].transform.position.y,TargetNode.y);
					}
				}
				//HardLined path!
				if(PathNodes.Length != 1 && !CurvedPath)
				{
					//Direction Vector
					Vector3 dirVec = TargetNode - transform.position;
					dirVec.Normalize();
					Vector3 newPos = transform.position;
					Vector3 addPos = dirVec * Mathf.Abs(Speed.x) * Time.deltaTime;
					newPos += addPos;
					transform.position = newPos;
					if(PlayerSnapped)
					{
						Vector3 playerPos = GameController.Player.transform.position;
						playerPos += addPos;
						GameController.Player.transform.position = playerPos;
					}
				}
				//Smooth path...
				else if(Speed.x != 0)
				{
					if(PathNodes.Length == 1)
					{
						curAngle += Speed.x * Time.deltaTime;
						if(curAngle < 0)
							curAngle += GlobalFunctions.Rad360;
						else if(curAngle > GlobalFunctions.Rad360)
							curAngle -= GlobalFunctions.Rad360;
						Vector3 newPos = PathNodes[0].transform.position;
						newPos.x += Mathf.Cos(curAngle) * SoloDistance;//*distance;
						newPos.y += Mathf.Sin(curAngle) * SoloDistance;//*distance;
						if(PlayerSnapped)
						{
							Vector3 playerPos = GameController.Player.transform.position;
							playerPos += newPos - transform.position;
							GameController.Player.transform.position = playerPos;
						}
						transform.position = newPos;
					}
					else
					{
		//				float distance = (TargetNode - transform.position).magnitude;
		//				float distance = (PathNodes[curNodeIndex].transform.position - transform.position).magnitude - 
		//									(TargetNode - transform.position).magnitude;
		//				float distance = Mathf.Abs(targetangle-curAngle)/90.0f ;
		//				Debug.Log (distance);
		//				distance = (((TargetNode - transform.position).magnitude)*distance) +
		//							(((PathNodes[curNodeIndex].transform.position - transform.position).magnitude)*(1-distance));
						Vector3 newPos = new Vector3(PathCenter.x,PathCenter.y,GameController.Player.transform.position.z);
						Debug.Log(PathCenter);
						//Debug.Log(Mathf.Cos(3.14f) + " && " + Mathf.Sin(3.14f));
						float disH = Mathf.Abs(TargetNode.x-PathNodes[curNodeIndex].transform.position.x);
						float disV = Mathf.Abs(TargetNode.y-PathNodes[curNodeIndex].transform.position.y);
						float scale = GlobalFunctions.Min(disH,disV)/GlobalFunctions.Max (disH,disV);
						
						float speed = Speed.x * Time.deltaTime;
						if(Mathf.Abs(targetangle-curAngle) > Mathf.Abs(speed))
							curAngle += speed * scale;
						
						newPos.x += Mathf.Cos(curAngle) * disH;//*distance;
						newPos.y += Mathf.Sin(curAngle) * disV;//*distance;
						if(PlayerSnapped)
						{
							Vector3 playerPos = GameController.Player.transform.position;
							playerPos += newPos- transform.position;
							GameController.Player.transform.position = playerPos;
						}
						transform.position = newPos;
					}
				}
			}
				
			Rotation += RotationSpeed * Time.deltaTime;
			transform.rotation = Quaternion.AngleAxis(Rotation, transform.forward);
		}
	}
	
	void OnTriggerEnter(Collider collision) {
		if(collision.gameObject.CompareTag("Player"))
			PlayerSnapped = true;
    }
	
	void OnTriggerExit(Collider collision) {
		if(collision.gameObject.CompareTag("Player"))
			PlayerSnapped = false;
    }
	
	
}
