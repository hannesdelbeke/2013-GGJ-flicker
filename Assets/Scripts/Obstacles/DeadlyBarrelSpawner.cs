using UnityEngine;
using System.Collections;

public class DeadlyBarrelSpawner : MonoBehaviour 
{
	public GameObject m_deadlyBarrel;
	public Sphincter m_sphincter;
	
	public float m_delay = 1f;
	public float m_interval = 1f;
	
	// Use this for initialization
	private void Start () 
	{
		InvokeRepeating("Spawn", m_delay, m_interval);
	}
	
	private void Spawn()
	{
		m_sphincter.AssIsOpening = true;
		audio.Play();
		m_sphincter.AssIsClosing = false;
		Instantiate(m_deadlyBarrel, transform.position, Quaternion.Euler(90f, 0f, 0f));
		
		StartCoroutine(CloseAss());
	}
	
	private IEnumerator CloseAss()
	{
		yield return new WaitForSeconds(1f);
		
		audio.Play();
		m_sphincter.AssIsOpening = false;
		m_sphincter.AssIsClosing = true;
	}
}