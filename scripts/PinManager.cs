using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PinManager : MonoBehaviour {
	
	public static int pinum = 0;
	public bool cnt=false;
	public int prefall = -1;
	public float pretime;
	public float timeLimit=3f;
	private int pinNum = 10;
	private GameManager gmMngr;

	void Start(){


		gmMngr = GameObject.FindObjectOfType<GameManager> ();
	}


	public void OnTriggerExit(Collider col){
		
		if (col.gameObject.name == "Ball") {
			
			cnt = true;

		}

	}


	public void OnTriggerEnter(Collider col){
		if (col.gameObject.name == "Ball") {
			gmMngr.cam1.SetActive (false);
			gmMngr.cam2.SetActive (true);

		}

	}

	public void Update(){
		
		if (cnt){
			pinCounterManager ();

		}
	
	}


	public void pinCounterManager(){

		int fall = pinStand ();


		if (fall!=prefall){

			pretime = Time.time;
			prefall = fall;
			return;
		}

		if (Time.time-pretime>timeLimit){
			pinfall ();
		}

	}



	public int pinStand (){
		
		 int pinStanding = 0;

		foreach(pin pin in GameObject.FindObjectsOfType<pin>()){

			if (pin.standing()){
				pinStanding++;
			}

		}
		return pinStanding;
	}

	public void pinfall(){

		int StandingPinsNum = pinStand ();

		int pinfallNum = pinNum - StandingPinsNum;
		pinNum = StandingPinsNum;


		print ("pinFallNum:" + pinfallNum);
		gmMngr.Round (pinfallNum);

		cnt = false;
		prefall = -1;
	}

	public void Reset(){
		
		pinNum = 10;
	}


}
