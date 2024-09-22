using UnityEngine;
using System.Collections;

public class LaserBeam : MonoBehaviour 
{
	public float m_delay = 3f;
	public float m_repeatRate = 3f;
	public float m_enabledTime = 1f;
	public GameController m_gameController;
	
	//H
	public bool keepEnabled = false;
	public float smoothTime = 1f; 
	private float smooth = 1f;
	private float alpha = 1f;
	private bool enabled = true;
	private bool startSmooth = true;
	public float clipSmooth = 0.4f ; //decides when between 0 and 1.0 laser should enable damage (var enabled = true)
	//H
	
	// Use this for initialization
	private void Start () 
	{
		m_gameController = GameObject.FindObjectOfType(typeof(GameController)) as GameController;
		if(!keepEnabled)
		{
		InvokeRepeating("ToggleEnabled", m_delay, m_repeatRate);	
		InvokeRepeating("ToggleDisabled", m_delay + m_enabledTime, m_repeatRate);	
		}
		if(keepEnabled)
		ToggleEnabled();
	}
	void Update () {
		if(!GameController.IsGamePaused())
		{
			//Calculate smooth
			//==============================================
		if(startSmooth && smooth <1)
			{
				smooth += Time.deltaTime*smoothTime ;
				if(smooth-clipSmooth <0 )
				enabled = true;
			}
		if(!startSmooth && smooth > 0)
			{
				smooth -= Time.deltaTime*smoothTime ;
				if(smooth<clipSmooth )
				enabled = false;
			}
			
			if( smooth > 1 ) smooth=1;
			if( smooth < 0 ) smooth=0;
			//==============================================
			
		alpha=smooth;
		//	alpha = renderer.material.color.a * smooth;
		renderer.material.color = new Color( gameObject.renderer.material.color.r,gameObject.renderer.material.color.g,gameObject.renderer.material.color.b,alpha);	
		//renderer.material.color.a = alpha;
		}
	}
	
	private void ToggleEnabled()
	{
		//gameObject.SetActive(true);
		startSmooth = true;
	}
	
	private void ToggleDisabled()
	{
		//gameObject.SetActive(false);
		if(!keepEnabled)
		startSmooth=false;
		
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player" && enabled)
		{
			m_gameController.PlayerIsDead();
		//	Debug.Log("Player dies");
		}
	}
	
}
