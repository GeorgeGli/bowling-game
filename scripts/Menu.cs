using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	public InputField p1_NameAI;
	public InputField p1_Name;
	public InputField p2_Name;
	public Text p1_Text;
	public Text p2_Text;
	public static string NameOne="Player1";
	public static string NameTwo="AI";
	public GameObject firstPanel;
	public GameObject PlayPanel;
	public GameObject keysPanel;
	public GameObject AI_PlayPanel;

	void Update(){

		p1_Text.text= NameOne;
		p2_Text.text= NameTwo;
	}

	public void ChangeScene(string sceneName){
		Time.timeScale = 1;
		Application.LoadLevel (sceneName);
	}

	public void ClickExit()

	{  
		Application.Quit();
	}

	public void PauseBtn(){

		if (Time.timeScale==1){
			
			Time.timeScale = 0;
		}
		else{
			Time.timeScale = 1;
		}
	}

	public void AI_Btn(){
		firstPanel.SetActive (false);
		AI_PlayPanel.SetActive (true);

	}

	public void PlayBtn(){
		firstPanel.SetActive (false);
		PlayPanel.SetActive (true);
	
	}

	public void instructionBtn(){
		firstPanel.SetActive (false);
		keysPanel.SetActive (true);

	}

	public void BackBtn(){
		firstPanel.SetActive (true);
		keysPanel.SetActive (false);
		PlayPanel.SetActive (false);
		AI_PlayPanel.SetActive (false);
	}

	public void SetNames(){

		NameOne = p1_Name.text;
		NameTwo = p2_Name.text;

	}

	public void SetName(){

		NameOne = p1_NameAI.text;
		NameTwo ="AI";

	}
}
