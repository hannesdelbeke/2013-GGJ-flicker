using UnityEngine;
using System.Collections;

public class DeadlyBarrel : MonoBehaviour 
{
	public AudioClip m_destroySound;
	
	private void OnTriggerEnter(Collider other)
	{
		renderer.enabled = false;
		audio.PlayOneShot(m_destroySound);
		Destroy(gameObject, 3f);
	}
}
