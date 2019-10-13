using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public Text[] text;
	public Text[] frame;
	public GameObject green1;
	public GameObject green2;

	public void FillRolls (List<int> rolls,bool turn){
		
		string scoresString = FormatRolls (rolls);


		if (!turn) {
			

			for (int i = 0; i <  scoresString.Length; i++) {
				text [i].text = scoresString[i].ToString ();

			    }
	   }
		else{
			

			for (int i = 0; i < scoresString.Length; i++) {
				text [i+21].text = scoresString [i].ToString ();
			}
		}
	}


	public void FillFrame (List<int> frames,bool turn){
		
		if (!turn) {

			for (int i = 0; i < frames.Count; i++) {
				frame [i].text = frames [i].ToString ();

			}
		}
		else{

			for (int i = 0; i < frames.Count; i++) {
				frame [i+10].text = frames [i].ToString ();
			}
		}
	}


	public static string FormatRolls (List<int> rolls){

		string output = "";

		for (int i = 0; i < rolls.Count; i++) {
			int box = output.Length + 1;							// Score box 1 to 21 

			if (rolls[i] == 0) {									// Always enter 0 as -
				output += "-";
			} else if ((box%2 == 0 || box == 21) && rolls[i-1]+rolls[i] == 10) {	// SPARE
				output += "/";	
			} else if (box >= 19 && rolls[i] == 10)	{				// STRIKE in frame 10
				output += "X";
			} else if (rolls[i] == 10) {							// STRIKE in frame 1-9
				output += "X ";
			} else {
				output += rolls[i].ToString();						// Normal 1-9 bowl
			}
		}

		return output;
	}



}
