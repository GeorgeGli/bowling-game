using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplyNames : MonoBehaviour {

	public InputField p1_Name;
	public InputField p2_Name;
	public static string NameOne="Player1";
	public static string NameTwo="Player2";



	public void SetNames(){

		NameOne = p1_Name.text;
		NameTwo = p2_Name.text;

	}


}
