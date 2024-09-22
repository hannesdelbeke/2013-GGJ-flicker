using UnityEngine;
using System.Collections;

/// <summary>
/// Looping script.
/// </summary>
public class LoopingScript : MonoBehaviour
{
	public enum Direction { NONE, RIGHT, LEFT }
	
	private PlayerController_WithoutCharacterController m_player;
	public Direction m_direction;
	
	public LoopingEnterExit m_enter;
	public LoopingEnterExit m_exit;
	
	public float m_distanceToDo = 2f;
	
	/// <summary>
	/// Initialization.
	/// </summary>
	private void Awake ()
	{
		m_direction = Direction.NONE;
		
		m_player = FindObjectOfType(typeof(PlayerController_WithoutCharacterController)) as PlayerController_WithoutCharacterController;
		m_enter.Entered += HandleM_enterEntered;
		m_enter.Exited += HandleM_enterExited;
		m_exit.Entered += HandleM_exitEntered;
		m_exit.Exited += HandleM_exitExited;
	}

	private void HandleM_exitExited (object sender, System.EventArgs e)
	{
		//Debug.Log(m_player.travelledDistance);
	}

	private void HandleM_enterExited (object sender, System.EventArgs e)
	{
		//Debug.Log(m_player.travelledDistance);
	}

	private void HandleM_exitEntered (object sender, System.EventArgs e)
	{
		m_player.travelledDistance = m_distanceToDo;
		if(m_direction == Direction.NONE)
		{
			m_player.zSnapPoint = m_exit.transform;
			if(m_enter.transform.InverseTransformPoint(m_player.transform.position).x > 0f)
				m_direction = Direction.LEFT;
			else
				m_direction = Direction.RIGHT;
		}
		else
		{
			m_player.zSnapPoint = null;
			m_direction = Direction.NONE;
		}
	}

	private void HandleM_enterEntered (object sender, System.EventArgs e)
	{
		m_player.travelledDistance = 0f;
		if(m_direction == Direction.NONE)
		{
			m_player.zSnapPoint = m_enter.transform;
			if(m_enter.transform.InverseTransformPoint(m_player.transform.position).x < 0f)
				m_direction = Direction.RIGHT;
			else
				m_direction = Direction.LEFT;
		}
		else
		{
			m_player.zSnapPoint = null;
			m_direction = Direction.NONE;
		}
	}
	
	// Update is called once per frame
	private void FixedUpdate ()
	{
		switch(m_direction)
		{
		case Direction.NONE:
			break;
		case Direction.RIGHT:
			m_player.transform.position = new Vector3(m_player.transform.position.x, m_player.transform.position.y,
													Mathf.Lerp(m_enter.transform.position.z, m_exit.transform.position.z
													, m_player.travelledDistance / m_distanceToDo));
			break;
		case Direction.LEFT:
			m_player.transform.position = new Vector3(m_player.transform.position.x, m_player.transform.position.y,
													Mathf.Lerp(m_enter.transform.position.z, m_exit.transform.position.z
													, m_player.travelledDistance / m_distanceToDo));
			break;
		}
	}
	
	public void Reset()
	{
		m_direction = Direction.NONE;
	}
}

