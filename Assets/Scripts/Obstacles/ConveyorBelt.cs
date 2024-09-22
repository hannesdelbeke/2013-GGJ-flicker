using UnityEngine;
using System.Collections;

public class ConveyorBelt : MonoBehaviour 
{
	public float m_moveSpeed = 1f;
	public float m_direction = 1f;
	
	private bool m_movePlayer;
	private PlayerController_WithoutCharacterController m_playerTransform;
	
	private void Start()
	{
		m_playerTransform = GameObject.FindObjectOfType(typeof(PlayerController_WithoutCharacterController)) as PlayerController_WithoutCharacterController;
	}
	
	private void Update()
	{
		if(!GameController.IsGamePaused())
		{
			if(m_movePlayer)
			{
				m_playerTransform.transform.position += Vector3.right * m_direction * m_moveSpeed * Time.deltaTime;
			}
		}
	}
	
	private void OnTriggerEnter(Collider collision)
	{
		Debug.Log("OnCollisionEnter");
		if(collision.tag == "Player")
		{
			m_movePlayer = true;
		}
	}
	
	private void OnTriggerExit(Collider collision)
	{
		if(collision.tag == "Player")
		{
			m_movePlayer = false;
		}
	}
}
