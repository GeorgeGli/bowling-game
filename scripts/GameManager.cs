using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public enum StateType
	{
		endBowl,
		endRound,
		lastRound,
		endGame
	};



	private int bowl = 0;
	private int _bowl = 0;
	private bool turn=false;

	public Text Name1;
	public Text Name2;
	public Text Result;
	public PinManager pinMngr;
	private TestBall tball;
	private Score score;

	public List<int> printList = new List<int> (); 
	public List <int> pointsPerRoll = new List<int> ();
	public List <int> pointsPerRoll_p2 = new List<int> ();


	public GameObject endGamePanel;
	public GameObject cam2;
	public GameObject cam1;
	private AudioSource click;


	void Start () {
		
		pinMngr = GameObject.FindObjectOfType<PinManager> ();
		score = GameObject.FindObjectOfType<Score> ();
		tball = GameObject.FindObjectOfType<TestBall> ();


		score.green1.SetActive (true);
		score.green2.SetActive (false);
	}
	

	public void Round (int pinFallNum) {

	if (!turn) {
			
					pointsPerRoll.Add (pinFallNum);
			//foreach (int ppr2 in pointsPerRoll) {
			//	print ("pprPro:"+ppr2);  
			//}
			//print ("ppr[count-1]: " + pointsPerRoll [pointsPerRoll.Count - 1]);
			//print ("bowl:" + bowl);

					if (bowl == 20) {  
				
				PerfomAction (StateType.endRound);
					} 
				    else if (bowl >= 18 && pointsPerRoll [pointsPerRoll.Count-1] == 10) {    
				     
					    bowl++;
				PerfomAction (StateType.lastRound);
					}
				    else if (bowl == 19) {  
				        
									if ((pointsPerRoll [pointsPerRoll.Count-1] + pointsPerRoll [pointsPerRoll.Count-2]) >= 10) {
														
										bowl++;			
										PerfomAction (StateType.lastRound);
				                       } else {
					                  
					                    PerfomAction (StateType.endRound);
									}
					} 
				    else if (bowl % 2 == 0) {                        // prwth volh tou round
								
									if (pinFallNum == 10) {
										bowl += 2;
										PerfomAction (StateType.endRound);
									} else {
										bowl++;
										PerfomAction (StateType.endBowl);
									}

					} 
				    else if (bowl % 2 != 0) {                      //deuterh volh tou round
											
								bowl++;
								PerfomAction (StateType.endRound);
							}
		   
	}
	else{

					pointsPerRoll_p2.Add (pinFallNum);


					if (_bowl == 20) { 

						PerfomAction (StateType.endGame);

					} 
					else if (_bowl >= 18 && pointsPerRoll_p2 [pointsPerRoll_p2.Count-1] == 10) { 
						_bowl++;
						PerfomAction (StateType.lastRound);

					}
					else if (_bowl == 19) {

						if ((pointsPerRoll_p2 [pointsPerRoll_p2.Count-1] + pointsPerRoll_p2 [pointsPerRoll_p2.Count-2]) >= 10) {  //19
							_bowl++;			
							PerfomAction (StateType.lastRound);
						} else {
							PerfomAction (StateType.endGame);
						}

					} 
					else if (_bowl % 2 == 0) {                            // prwth volh tou round

						if (pinFallNum == 10) {

							_bowl += 2;
							PerfomAction (StateType.endRound);

						} else {
							_bowl++;
							PerfomAction (StateType.endBowl);
						}

					} 
					else if (_bowl % 2 != 0) {                         //deuterh volh tou round

						_bowl++;
						PerfomAction (StateType.endRound);
					}
		}
  }





	public static List<int> TotalPoints(List<int> rollPoints){


		List<int> frameSum = new List<int> ();

		int sum = 0;

		foreach (int frameScore in FrameScore(rollPoints)) {
			sum += frameScore;
		
			frameSum.Add (sum);
		}

	
		foreach (int score in frameSum) {

		
		}
		return frameSum;
	}

	public static List<int> FrameScore(List<int> rollPoints){

		List<int> frames = new List<int> ();

		for (int i=1;i<rollPoints.Count;i+=2){

			if (frames.Count==10){
				break;
			}


			if (rollPoints[i]+rollPoints[i-1] < 10){

				frames.Add (rollPoints [i] + rollPoints [i - 1]);
			}


			if (rollPoints.Count-i <= 1){
				
				break;
			}


			if (rollPoints[i-1]==10){
				i--;

				frames.Add (10 + rollPoints [i + 1] + rollPoints [i + 2]);
			}
			else if (rollPoints[i-1]+rollPoints[i] == 10){  
				


				frames.Add (10 + rollPoints [i + 1]);
			}



		}

		return frames;
	}




	public void PerfomAction(StateType action){

		if (action == StateType.endBowl) {
				
					if (!turn) {
						score.FillRolls (pointsPerRoll, turn);
						score.FillFrame (TotalPoints (pointsPerRoll), turn);
					} else {
						score.FillRolls (pointsPerRoll_p2, turn);
						score.FillFrame (TotalPoints (pointsPerRoll_p2), turn);
					}

					foreach (pin pin in GameObject.FindObjectsOfType<pin>()) {
						pin.standingReset ();
					}



					cam1.SetActive (true);
					cam2.SetActive (false);
			       
					tball.res2 ();

		} else if (action == StateType.endRound) {
			
					if (!turn) {  
						score.FillRolls (pointsPerRoll, turn);
						score.FillFrame (TotalPoints (pointsPerRoll), turn);
						score.green1.SetActive (false);
						score.green2.SetActive (true);
						turn = true;
				      
					} else {
						score.FillRolls (pointsPerRoll_p2, turn);
						score.FillFrame (TotalPoints (pointsPerRoll_p2), turn);
						score.green1.SetActive (true);
						score.green2.SetActive (false);
						turn = false;
					}
				
					foreach (pin pin in GameObject.FindObjectsOfType<pin>()) {
							pin.standingReset ();		
							pin.reset ();
					}

					click = GetComponent<AudioSource> ();
					click.Play ();

			cam1.SetActive (true);
			cam2.SetActive (false);

			pinMngr.Reset ();
			tball.res2 ();

					



		} else if (action == StateType.lastRound) {
			
					if (!turn) {
					
						score.FillRolls (pointsPerRoll, turn);
					} else {
						score.FillRolls (pointsPerRoll_p2, turn);
					}
										
					foreach (pin pin in GameObject.FindObjectsOfType<pin>()) {
							pin.standingReset ();		
							pin.reset ();
					}

					cam1.SetActive (true);
					cam2.SetActive (false);
			        pinMngr.Reset ();
					tball.res2 ();

		} else if (action == StateType.endGame) {


				
					
				
					score.FillRolls (pointsPerRoll_p2, turn);
					score.FillFrame (TotalPoints (pointsPerRoll_p2), turn);
					score.green1.SetActive (false);
					score.green2.SetActive (false);
					
					
			List<int> totalPlayer2 = new List<int> ();
			List<int> totalPlayer1 = new List<int> ();
			totalPlayer2 = TotalPoints (pointsPerRoll_p2);
			totalPlayer1 = TotalPoints (pointsPerRoll);


			if (totalPlayer1[totalPlayer1.Count - 1]>totalPlayer2 [totalPlayer2.Count - 1]){
				
				Result.text=Name1.text+" is the winner!";

			}else if (totalPlayer1[totalPlayer1.Count - 1]<totalPlayer2 [totalPlayer2.Count - 1]){

				Result.text=Name2.text+" is the winner!";

			}
			else {
				Result.text="It's a draw!";
			}

			pointsPerRoll.Clear ();	
			pointsPerRoll_p2.Clear ();
			tball.push = false;
			endGamePanel.SetActive (true);

		}
	}
}
