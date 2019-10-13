using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class CameraController : MonoBehaviour {

	public GameObject sphere;
	public GameObject SecondCam;
	public GameObject ThirdCam;
	Vector3 offset;
	Vector3 newPos = new Vector3 (9.375011f,2.247001f,1.845001f);
	public bool follow=false;
	public int camNum=1;

	void Start () {
		
		offset = newPos - sphere.transform.position;
	}

	void LateUpdate (){

		PosChange();

		Follow ();

		if (Input.GetKeyUp (KeyCode.B)) {
			
				if (camNum==1){
				   SecondCam.SetActive (true);
				   ThirdCam.SetActive (false);
				   camNum++;
				}
			    else if (camNum==2){
					SecondCam.SetActive (false);
				    ThirdCam.SetActive (true);
				    camNum++;
				}
			   else{
					SecondCam.SetActive (false);
					ThirdCam.SetActive (false);
					camNum=1;
				 }

		}

	}



	void Follow () {
	       if (follow){
		        transform.position = sphere.transform.position + offset;
		         }
	}


	void PosChange(){
		
		transform.position = Vector3.Lerp (transform.position, newPos, Time.deltaTime);
	}
}
