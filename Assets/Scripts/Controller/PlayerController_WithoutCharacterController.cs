using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Player controller. Processes all the controls of the player.
/// </summary>
public class PlayerController_WithoutCharacterController : MonoBehaviour 
{
	public float m_moveSpeed = 10f;
	public float m_moveAcceleration = 5f;
	public float m_brakeAcceleration = 10f;
	public float m_jumpSpeed = 20f;
	
	public float m_jumpFadeFactor = 0.85f;
	public float m_moveFriction = 0.85f;
	
	public float m_smoothRotationFactor = 10f;
	public float m_batteryInterval = 0.33f;
	
	public Transform m_mesh;
	
	public Material m_flickerShader;
	public List<Texture2D> m_batteryTextures;
	
	public List<AudioClip> m_curseSounds;
	public AudioClip m_breakingSound;
	public AudioClip m_jumpSound;
	public AudioClip m_ridingSound;
	
	public LightController m_lightController;
	
	private Vector3 m_moveDirection;
	private Vector3 m_worldMoveDirection;
	
	private float m_travelledDistance;
	private float m_timer;
	
	private bool m_jumpFade;
	private bool m_isGrounded;
	
	private Transform m_transform;
	private Transform m_zSnapPoint;
	
	private Animator m_animator;
	
	private int m_batteryIndex;
	
	private bool m_previousActive;
	
	private float m_CharacterHeightOffset = 1.5f;
	
	#region setters and getters
	
	public float travelledDistance
	{
		get
		{
			return m_travelledDistance;
		}
		set
		{
			m_travelledDistance = value;
		}
	}
	
	public Transform zSnapPoint
	{
		get
		{
			return m_zSnapPoint;
		}
		set
		{
			m_zSnapPoint = value;
		}
	}
	
	#endregion
	
	/// <summary>
	/// Initialization.
	/// </summary>
	private void Awake () 
	{
		m_jumpFade = false;
		m_moveDirection = Vector3.zero;
		m_transform = transform;
		m_animator = GetComponentInChildren<Animator>();
		m_previousActive = GameController.IsGameActive();
	}
	
	private IEnumerator PlaySoundDelayed()
	{
		yield return new WaitForSeconds(0.5f);
		audio.PlayOneShot(m_curseSounds[Random.Range(0, m_curseSounds.Count)]);
	}
	
	/// <summary>
	/// Update loop, here the input is caught.
	/// </summary>
	private void Update () 
	{
		if(!GameController.IsGamePaused())
		{
			if(m_previousActive != GameController.IsGameActive())
			{
				m_batteryIndex = 0;
				
				m_flickerShader.SetTexture("_Illum", m_batteryTextures[m_batteryIndex]);
				
				if(!m_lightController.GetDaylight())
				{
					//audio.PlayOneShot(m_curseSounds[Random.Range(0, m_curseSounds.Count)]);
					StartCoroutine(PlaySoundDelayed());
				}
			}
				
			RaycastHit hitInfo;
			
			bool isRaycastHit = Physics.Raycast(m_transform.position, m_transform.up * -1f, out hitInfo, m_CharacterHeightOffset+0.05f);
			
			if(isRaycastHit == true && isRaycastHit != m_isGrounded)
				m_animator.SetBool("grounded", true);
			
			m_isGrounded = isRaycastHit;
			
			HorizontalMovement();
			
			if(m_isGrounded)
			{
				m_moveDirection.y = 0f;
				GroundedBehaviour(isRaycastHit, hitInfo);
			}
			else
			{
				AirBehaviour();
			}
			
			// do the actual move
	        Move(m_moveDirection);
			
			if(!GameController.IsGameActive())
			{
				m_moveDirection = Vector3.zero;
				m_timer += Time.deltaTime;
				
				if(m_timer >= m_batteryInterval)
				{
					if(m_batteryIndex < m_batteryTextures.Count - 2)
						++m_batteryIndex;
					else
						m_batteryIndex = 0;
					
					m_flickerShader.SetTexture("_Illum", m_batteryTextures[m_batteryIndex]);
					
					m_timer = 0;
				}
			}
			
			m_previousActive = GameController.IsGameActive();
		}
	}
	
	/// <summary>
	/// Move in the specified moveDirection.
	/// </summary>
	/// <param name='moveDirection'>
	/// Move direction.
	/// </param>
	private void Move(Vector3 moveDirection)
	{
		RaycastHit hitInfo;
		
		if(Physics.Raycast(m_transform.position + m_transform.up * 0.7f, moveDirection.x * m_transform.right, out hitInfo, 0.4f) ||
			Physics.Raycast(m_transform.position + m_transform.up, moveDirection.x * m_transform.right, out hitInfo, 0.5f) ||
			Physics.Raycast(m_transform.position + m_transform.up * -0.7f, moveDirection.x * m_transform.right, out hitInfo, 0.4f))
		{
			moveDirection.x = 0f;
			m_moveDirection.x = 0f;
		}
		if(Physics.Raycast(m_transform.position + m_transform.right * 0.4f, moveDirection.y * m_transform.up, out hitInfo, 0.8f) ||
			Physics.Raycast(m_transform.position, moveDirection.y * m_transform.up, out hitInfo, m_CharacterHeightOffset + 0.5f) ||
			Physics.Raycast(m_transform.position + m_transform.right * -0.4f, moveDirection.y * m_transform.up, out hitInfo, 0.8f))
		{
				moveDirection.y = 0f;
				m_moveDirection.y = 0f;
		}
		
		// apply gravity locally
		//if(!m_isGrounded)
		//    m_moveDirection.y += Physics.gravity.y * Time.deltaTime;
		
		//Debug.Log(m_isGrounded + ", " + m_moveDirection);
		m_transform.position += m_transform.TransformDirection(moveDirection * Time.deltaTime);
		
		m_travelledDistance += moveDirection.x * Time.deltaTime;
		
		// apply gravity in world space
		if(!m_isGrounded)
		{
			m_worldMoveDirection += Physics.gravity * Time.deltaTime;
		    m_transform.position += m_worldMoveDirection * Time.deltaTime;
			
			if(m_worldMoveDirection.y + moveDirection.y <= 0f)
			{
				m_animator.SetBool("goingdown", true);
				m_animator.SetBool("jumpstart", false);
				m_animator.SetBool("idle", false);
				m_animator.SetBool("riding", false);
				m_animator.SetBool("breaking", false);
			}
		}
		else
		{
			m_worldMoveDirection = Vector3.zero;
		}

	}

	/// <summary>
	/// Control of the horizontal movement.
	/// </summary>
	private void HorizontalMovement()
	{
		// get the horizontal input
        float input = 0;
		if(GameController.IsGameActive())
			input = Input.GetAxisRaw("Horizontal");
		
		if(input > 0f)
		{
			m_mesh.localRotation = Quaternion.Slerp(m_mesh.localRotation
								, Quaternion.AngleAxis(90f, Vector3.up)
								, Time.deltaTime * m_smoothRotationFactor);
		}
		else if(input < 0f)
		{
			m_mesh.localRotation = Quaternion.Slerp(m_mesh.localRotation
								, Quaternion.AngleAxis(270f, Vector3.up)
								, Time.deltaTime * m_smoothRotationFactor);
		}
		
        if (input != 0f)
        {
			// todo: set the run/walk animation
			if(Mathf.Sign(input) == Mathf.Sign(m_moveDirection.x))
			{
            	m_moveDirection.x += input * (m_moveAcceleration * Time.deltaTime);
				
				if(m_isGrounded)
				{
					m_animator.SetBool("idle", false);
					m_animator.SetBool("goingdown", false);
					m_animator.SetBool("jumpstart", false);
					m_animator.SetBool("riding", true);
					if(!m_animator.GetBool("riding"))
					{
						audio.loop = true;
						audio.clip = m_ridingSound;
						audio.Play();
					}
					m_animator.SetBool("breaking", false);
				}
			}
			else
			{
            	m_moveDirection.x += input * (m_brakeAcceleration * Time.deltaTime);
				if(m_isGrounded)
				{
					m_animator.SetBool("idle", false);
					m_animator.SetBool("goingdown", false);
					m_animator.SetBool("jumpstart", false);
					m_animator.SetBool("riding", false);
					if(!m_animator.GetBool("breaking"))
					{
						audio.Stop();
						audio.PlayOneShot(m_breakingSound);
					}
					m_animator.SetBool("breaking", true);
					
				}
			}
				
			m_moveDirection.x = Mathf.Clamp(m_moveDirection.x, -m_moveSpeed, m_moveSpeed);
        }
        else
        {
			// control the animation: idle
			m_moveDirection.x *= m_moveFriction;
			if(Mathf.Abs(m_moveDirection.x) < 0.001f)
				m_moveDirection.x = 0f;
			if(m_isGrounded)
			{
				m_animator.SetBool("idle", true);
				m_animator.SetBool("goingdown", false);
				m_animator.SetBool("jumpstart", false);
				m_animator.SetBool("riding", false);
				if(!m_animator.GetBool("breaking"))
				{
					audio.Stop();
					audio.PlayOneShot(m_breakingSound);
				}
				m_animator.SetBool("breaking", true);
			}
        }
	}
	
	/// <summary>
	/// The behaviour of the player on the ground.
	/// </summary>
	private void GroundedBehaviour(bool isRaycastHit, RaycastHit hitInfo)
	{		
		if(isRaycastHit)
		{
			// change the direction of the forward vector
			float angle = Vector3.Angle(m_transform.right, hitInfo.normal) - 90f;
			m_moveDirection.y = m_moveDirection.x * Mathf.Sin(angle * Mathf.Deg2Rad);
			
			// reset the position if it is wrong
			if(Vector3.Distance(m_transform.position, hitInfo.point) != m_CharacterHeightOffset)
			{
				m_transform.localPosition = m_transform.localPosition - m_transform.up
					* (Vector3.Distance(m_transform.position, hitInfo.point) - m_CharacterHeightOffset);
			}
			
			// rotate the player if necessary
			m_transform.localRotation = Quaternion.Slerp(m_transform.localRotation
											, m_transform.localRotation * Quaternion.AngleAxis(angle, Vector3.forward)
											, Time.deltaTime * m_smoothRotationFactor);
		}

		// get the vertical input (both the up arrow and space bar for pc, up arrow and z for Winnitron)
        if (GameController.IsGameActive() && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Space)))
        {
            m_moveDirection.y = m_jumpSpeed;
			m_animator.SetBool("jumpstart", true);
			m_animator.SetBool("goingdown", false);
			m_animator.SetBool("idle", false);
			m_animator.SetBool("riding", false);
			m_animator.SetBool("breaking", false);
			audio.Stop();
			audio.PlayOneShot(m_jumpSound);
		}
	}
	
	/// <summary>
	/// The behaviour of the player in the air.
	/// </summary>
	private void AirBehaviour()
	{		
        m_jumpFade = !(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Space));
		
		float angle = Vector3.Angle(m_transform.up, Vector3.up);
		Vector3 axis = Vector3.Cross(m_transform.up, Vector3.up).normalized;
		// rotate the player if necessary
		m_transform.localRotation = Quaternion.Slerp(m_transform.localRotation
										, m_transform.localRotation * Quaternion.AngleAxis(angle, axis)
										, Time.deltaTime * m_smoothRotationFactor);
		//m_transform.localRotation = m_transform.localRotation * Quaternion.AngleAxis(angle, axis);
		//if(m_zSnapPoint != null)
		//{
		//	m_transform.position = new Vector3(m_transform.position.x, m_transform.position.y,
		//										m_zSnapPoint.position.z);
		//}
		// apply fade of jump if necessary
		if(m_jumpFade && m_moveDirection.y > 0f)
			m_moveDirection.y *= m_jumpFadeFactor;
		
		if(m_moveDirection.y < 0)
		{
			m_animator.SetBool("goingdown", true);
			m_animator.SetBool("jumpstart", false);
			m_animator.SetBool("idle", false);
			m_animator.SetBool("riding", false);
			m_animator.SetBool("breaking", false);
			audio.Stop();
		}
	}
}