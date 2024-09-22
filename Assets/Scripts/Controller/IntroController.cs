using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IntroController : MonoBehaviour
{
	public List<float> m_timings;
	
	public List<Texture2D> m_textures;
	
	public List<AudioClip> m_clips;
	
	private int m_index;
	
	// Use this for initialization
	private void Start ()
	{
		m_index = 0;
		
		float height = Screen.height;
		float width = 16f/9f * height;
		
		guiTexture.pixelInset = new Rect(-width * 0.5f, -height * 0.5f, width, height);

		StartCoroutine(ShowNextSlide(m_timings[m_index], m_textures[m_index], m_clips[m_index]));
	}
	
	private IEnumerator ShowNextSlide(float delay, Texture2D texture, AudioClip clip)
	{
		if(clip != null)
			audio.PlayOneShot(clip);

		guiTexture.texture = texture;

		yield return new WaitForSeconds(clip.length + 0.5f);
		
		if(m_index < m_timings.Count - 1)
		{
			++m_index;
			StartCoroutine(ShowNextSlide(m_timings[m_index], m_textures[m_index], m_clips[m_index]));
		}
		else
		{
			Application.LoadLevel (2); 	
		}
	}
	
	void Update()
	{
		if(Input.anyKey)
		{
			Application.LoadLevel (2); 	
		}	
	}
}

