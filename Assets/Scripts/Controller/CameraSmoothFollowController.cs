using UnityEngine;
using System.Collections;

/// <summary>
/// Camera smooth follow controller. Makes the camera follow a certain target in a smooth way.
/// </summary>
public class CameraSmoothFollowController : MonoBehaviour 
{
	public enum State { NORMAL, ZOOMOUT }
	
	public Transform m_zoomOutTarget;
    public Transform m_target;

    public float m_distance = 10.0f;
    // the height we want the camera to be above the target
    public float m_height = 5.0f;

	public float m_heightDamping = 2.0f;
    //public float rotationDamping = 3.0f;
    public float m_horizontalDamping = 3.0f;
	
	public float m_zoomSpeed = 0.5f;
	
	public State m_state;
	
	/// <summary>
	/// Initialization
	/// </summary>
	private void Awake () 
	{
		m_state = State.NORMAL;
		
		if(!m_target)
			Debug.LogError("No target found for the camera to follow.");
	}
	
	/// <summary>
	/// Late Update loop. Is called after the animations and physics have been processed.
	/// </summary>
	private void LateUpdate () 
	{
		switch(m_state)
		{
		case State.NORMAL:
			NormalBehaviour();
			break;
		case State.ZOOMOUT:
			ZoomBehaviour();
			break;
		}
    }
	
	private void NormalBehaviour()
	{
        // Calculate the current rotation angles
        //float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = m_target.position.y + m_height;

        //float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        // Damp the rotation around the y-axis
        //currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, m_heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        //Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        //transform.position = target.position;
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, m_target.position.x, m_horizontalDamping * Time.deltaTime)
                                        , m_target.position.y, m_target.position.z);
        //transform.position -= currentRotation * Vector3.forward * distance;
        transform.position -= Vector3.forward * m_distance;

        // Set the height of the camera
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        // Always look at the target
        //transform.LookAt(target);
        //float angle = Vector3.Angle(transform.forward, RemoveXValue(m_target.position) - RemoveXValue(transform.position));
        //Vector3 axis = Vector3.Cross(transform.forward, RemoveXValue(m_target.position) - RemoveXValue(transform.position));

        //transform.rotation *= Quaternion.AngleAxis(angle, axis);
	}
	
	private void ZoomBehaviour()
	{
		transform.position = Vector3.MoveTowards(transform.position, m_zoomOutTarget.position, m_zoomSpeed);
	}

    public Vector3 RemoveXValue(Vector3 to_remove)
    {
        return new Vector3(0f, to_remove.y, to_remove.z);
    }
	
	public void ZoomOnPlayer()
	{
		m_state = State.NORMAL;
	}
	
	public void ZoomOut()
	{
		m_state = State.ZOOMOUT;
	}
}
