using UnityEngine;
using System.Collections;

public class PlayerLight : MonoBehaviour {
	
	public float m_interval = 1;
	private float m_time;
	private bool m_on;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!GameController.IsGamePaused())
		{
			m_time += Time.deltaTime;
			
			if(m_on)
				light.intensity = Mathf.Lerp(0f, 1.5f, m_time / m_interval);
			else
				light.intensity = Mathf.Lerp(1.5f, 0f, m_time / m_interval);
			
			if(m_time >= m_interval)
			{
				m_time = 0f;
				m_on = !m_on;
			}
		}
	}
}
