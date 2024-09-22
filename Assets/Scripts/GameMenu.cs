using UnityEngine;
using System.Collections;

public class GameMenu : MonoBehaviour {
	
	public GameObject Continue;
	public GameObject Restart;
	public GameObject Background;
	
	public bool ContinueActive = true;
	
	private Color SelectionColor = new Color(0.73f,0.55f,0.16f);
	
	private float delayer = 0;
	
	// Use this for initialization
	void Start () {
		ResetMenu();
	}
	
	// Update is called once per frame
	void Update () {
		if(!GameController.IsGamePaused() && Input.GetButton("PauseGame"))
		{
			GameController.PauseGame();
		}
		if(GameController.IsGamePaused())
		{
			Vector3 newPos;
			if(Restart.transform.position.x < transform.position.x)
			{
				newPos = Restart.transform.position;
				newPos.x += 15 * Time.deltaTime;
				Restart.transform.position = newPos;
			}
			else
			{
				newPos = Restart.transform.position;
				newPos.x = transform.position.x;
				Restart.transform.position = newPos;
			}
			if(Continue.transform.position.x > transform.position.x)
			{
				newPos = Continue.transform.position;
				newPos.x -= 15 * Time.deltaTime;
				Continue.transform.position = newPos;
			}
			else
			{
				newPos = Continue.transform.position;
				newPos.x = transform.position.x;
				Continue.transform.position = newPos;
			}
			Background.renderer.material.color = SetAlpha(Background.renderer.material.color,Background.renderer.material.color.a+((0.85f-Background.renderer.material.color.a)*0.55f));	
		
			//Input
			delayer += Time.deltaTime;
			if(delayer > 0.2f && Input.GetAxisRaw("Vertical") != 0)
			{
				ContinueActive = !ContinueActive;
				SetColor();
				delayer = 0;
			}
		    if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Space))
        	{
				if(ContinueActive)
				{
					GameController.UnPauseGame();
					ResetMenu();
				}
				else
				{
					ResetMenu();
					GameController.UnPauseGame();
					Application.LoadLevel ("Level1"); 
				}
			}
		}
	}
	
	private Color SetAlpha(Color color, float alpha)
	{
		color.a = alpha;
		return color;
	}
	
	private Vector3 SetPosition(Vector3 pos, Vector3 offset)
	{
		pos += offset;
		return pos;
	}
	
	private void ResetMenu()
	{
		ContinueActive = true;	
		Background.renderer.material.color = SetAlpha(Background.renderer.material.color,0);
		Restart.transform.position = SetPosition (Restart.transform.position , new Vector3(-10,0,0));
		Continue.transform.position = SetPosition (Continue.transform.position , new Vector3(10,0,0));
		SetColor();
	}
	
	void SetColor()
	{
		if(ContinueActive)
		{
			Continue.renderer.material.SetColor("_Emission", new Color(1,1,1));
			Restart.renderer.material.SetColor("_Emission", SelectionColor);
		}
		else
		{
			Restart.renderer.material.SetColor("_Emission", new Color(1,1,1));
			Continue.renderer.material.SetColor("_Emission", SelectionColor);
		}
	}
}
