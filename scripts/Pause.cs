using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;

public class Pause : MonoBehaviour {

	public GameObject pausePanel;
	public GameObject info;
	public bool active=true;
	private TestBall ball;

	void Start(){

		ball = GameObject.FindObjectOfType<TestBall> ();
	}

	public void infoBtn(){

		if (active){
			info.SetActive (true);
			active = false;
		} else{
			info.SetActive (false);
			active = true;
		}

	}

	public void PauseBtn(){


		if (Time.timeScale==1){
			pausePanel.SetActive (true);
			ball.push = false;
			Time.timeScale = 0;
		}
		else{
			pausePanel.SetActive (false);
			ball.push = true;
			Time.timeScale = 1;
		}
	}

}
