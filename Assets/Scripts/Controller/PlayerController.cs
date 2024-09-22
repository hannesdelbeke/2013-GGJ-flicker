using UnityEngine;
using System.Collections;

/// <summary>
/// Player controller. Processes all the controls of the player.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour 
{
	public float m_moveSpeed = 10f;
	public float m_moveAcceleration = 5f;
	public float m_brakeAcceleration = 10f;
	public float m_jumpSpeed = 20f;
	
	public float m_jumpFadeFactor = 0.85f;
	public float m_moveFriction = 0.85f;
	
	private Vector3 m_moveDirection;
    private CharacterController m_characterController;
	
	private bool m_jumpFade;
	
	/// <summary>
	/// Initialization.
	/// </summary>
	private void Awake () 
	{
		m_jumpFade = false;
		m_moveDirection = Vector3.zero;
		m_characterController = GetComponent<CharacterController>();
	}
	
	/// <summary>
	/// Update loop, here the input is caught.
	/// </summary>
	private void Update () 
	{
		if(!GameController.IsGamePaused())
		{
			HorizontalMovement();
	
			if(m_characterController.isGrounded)
			{
				GroundedBehaviour();
			}
			else
			{
				AirBehaviour();
			}
			
			// apply gravity
	        m_moveDirection.y += Physics.gravity.y * Time.deltaTime;
			
			// apply fade of jump if necessary
			if(m_jumpFade && m_moveDirection.y > 0f)
				m_moveDirection.y *= m_jumpFadeFactor;
	
	        // do the actual move
	        if (m_characterController != null)
	            m_characterController.Move(m_moveDirection * Time.deltaTime);
		}
	}

	/// <summary>
	/// Control of the horizontal movement.
	/// </summary>
	private void HorizontalMovement()
	{
		// get the horizontal input
        float input = Input.GetAxisRaw("Horizontal");
        if (input != 0f)
        {
			// todo: set the run/walk animation
			if(Mathf.Sign(input) == Mathf.Sign(m_moveDirection.x))
            	m_moveDirection.x += input * (m_moveAcceleration * Time.deltaTime);
			else
            	m_moveDirection.x += input * (m_brakeAcceleration * Time.deltaTime);
				
			m_moveDirection.x = Mathf.Clamp(m_moveDirection.x, -m_moveSpeed, m_moveSpeed);
        }
        else
        {
			// control the animation: idle
			m_moveDirection.x *= m_moveFriction;
        }
	}
	
	/// <summary>
	/// The behaviour of the player on the ground.
	/// </summary>
	private void GroundedBehaviour()
	{
		// reset the movedirection y value to reset the falling motion
		m_moveDirection.y = 0f;
		
		// get the vertical input (both the up arrow and space bar for pc, up arrow and z for Winnitron)
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Space))
        {
            m_moveDirection.y = m_jumpSpeed;
		}
	}
	
	/// <summary>
	/// The behaviour of the player in the air.
	/// </summary>
	private void AirBehaviour()
	{		
        m_jumpFade = !(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Space));
	}
}
