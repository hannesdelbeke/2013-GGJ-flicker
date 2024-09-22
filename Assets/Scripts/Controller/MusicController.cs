using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {
	public LightController lightController;
	
	public AudioClip nightClip;
	public AudioClip dayClip;
	public AudioClip nightEffect;
	public AudioClip dayEffect;
	
	private bool previousDayLight;
	
	// Use this for initialization
	void Start () 
	{
		previousDayLight = lightController.GetDaylight();
		audio.clip = dayClip;
		audio.Play();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(lightController.GetDaylight())
		{
			if(lightController.GetDaylight() != previousDayLight)
			{
				audio.clip = dayClip;
				audio.Play();
				audio.PlayOneShot(dayEffect);
			}
		}
		else
		{
			if(lightController.GetDaylight() != previousDayLight)
			{
				audio.clip = nightClip;
				audio.Play();
				audio.PlayOneShot(nightEffect);
			}
		}
		
		previousDayLight = lightController.GetDaylight();
	}
}
