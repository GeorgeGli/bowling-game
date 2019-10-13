using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Utility;


public class TestBall : MonoBehaviour {

	public GameObject powerBar;
	public GameObject spinBar;
	public GameObject positionBar;
	public GameObject needle;
	public GameObject rightArrow;
	public GameObject leftArrow;
	private Vector3 Position;
	private Vector3 v;
	private Rigidbody rb;
	public Slider powerbar;
	private AudioSource ballRelease;
	public CameraController cam;
	private float powerbarvalue=0f;
	public bool push = true;
	public bool horFlag=true;
	public float fspin;
	public bool forceFlag=false;
	public bool pushSpin=true;
	bool flag=true;
	int flag2=0;
	public bool start=false;
	bool spinMeterStart=true;

	void Start () {
		
		cam=GameObject.FindObjectOfType<CameraController> ();
		 v = needle.transform.eulerAngles;

		Position = transform.position;	

		rb = GetComponent<Rigidbody> ();
		rb.maxAngularVelocity = 30;
		rb.useGravity = false;

	    powerbar.minValue = 3f;
		powerbar.maxValue = 10f;
		powerbar.value =  powerbarvalue;
	}
	

	void Update () {


		if (push) {

			  HorizontalPos ();

				if (pushSpin) {	
					StartSpin ();
					if (Input.GetKeyUp (KeyCode.C)) {
					  
					if (spinMeterStart){
						spinBar.SetActive (true);
						start = true;
						horFlag = false;
						spinMeterStart = false;
					}
					else {
						start = false;
						forceFlag = true;
						fspin = SpinConvert (needle.transform.eulerAngles.z);
						spinMeterStart = true;
					}


					}
				   
				}





					

					float val = forcev ();

					if (val != 0 && fspin!=0) {
						
						finalForce (val,fspin);

					}
		}

	}




	public void HorizontalPos(){



		rightArrow.GetComponent<RawImage> ().color = new Color32 (255,255,255,255);
		leftArrow.GetComponent<RawImage> ().color = new Color32 (255,255,255,255); 

		float limit = transform.position.x;

		if (horFlag) {
			
					if (Input.GetKey (KeyCode.RightArrow)) {
			        	if (limit < 9.981) {
							transform.Translate (Input.GetAxis ("Horizontal") * Time.deltaTime, 0f, 0f);
							rightArrow.GetComponent<RawImage> ().color = new Color32 (39, 223, 56, 255);
						}


					}

					if (Input.GetKey (KeyCode.LeftArrow)) {
				       if (limit > 8.907) {
							transform.Translate (Input.GetAxis ("Horizontal") * Time.deltaTime, 0f, 0f);
							leftArrow.GetComponent<RawImage> ().color = new Color32 (39, 223, 56, 255);
						}


					}

		}


	}

	public float forcev(){

		if (forceFlag){
			pushSpin = false;
			powerBar.SetActive (true);
				if (Input.GetKey (KeyCode.Space)) {
				    
					powerbarvalue += 10 * Time.deltaTime;
					powerbar.value = powerbarvalue;

				}
				if (Input.GetKeyUp (KeyCode.Space)) {
							

					return powerbar.value;
				}
		}
		return 0;
	}




	public void finalForce(float f,float _fspin)
		{
		push = false;
		cam.follow = true;

		rb.AddForce (transform.forward*300*f);
		rb.AddTorque (transform.forward * 10*_fspin);
		rb.useGravity = true;

		powerBar.SetActive (false);
		spinBar.SetActive (false);
		positionBar.SetActive (false);

		ballRelease = GetComponent<AudioSource> ();
		ballRelease.Play ();
		}



	public void StartSpin(){

		if (start){
			if (flag){
				needle.transform.Rotate (Vector3.forward, Time.deltaTime*100);
				if (needle.transform.eulerAngles.z < 40) {
					flag2 = 0;
				}
				if (needle.transform.eulerAngles.z>=98 && flag2==0 ){
					flag = false;
				}
			}
			else{
				needle.transform.Rotate (-Vector3.forward, Time.deltaTime*100);
				if (needle.transform.eulerAngles.z > 300) {
					flag2 = 1;
				}
				if (needle.transform.eulerAngles.z<=261 && flag2==1){
					flag = true;
				}
			}



		}
	}



	public float SpinConvert (float _spin){

		float finalSpin=0;

		if (_spin>=349 || _spin<=11){

			finalSpin = 0.1f;
		}
		else if (_spin<349 && _spin>=315 ){
			finalSpin = -1f; 
		}
		else if (_spin<315 && _spin>=287 ){
			finalSpin = -1.8f;  
		}
		else if (_spin<287 && _spin>257 ){
			finalSpin = -2.3f; 
		}
		else if (_spin>11 && _spin<44){
			finalSpin = 1f;

		}
		else if (_spin>=44 && _spin<72){
			finalSpin = 1.8f;

		}
		else {
			finalSpin = 2.3f;

		}

		return finalSpin;
	}


	public void res2(){

		powerBar.SetActive (false);
		spinBar.SetActive (false);
		positionBar.SetActive (true);

		powerbar.value = 0;
		powerbarvalue = 0;

		transform.position = Position;
		transform.rotation =Quaternion.identity;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.useGravity = false;

		v.z = 0;
		needle.transform.eulerAngles = v;

		push = true;
		horFlag=true;
		pushSpin=true;
		start=false;
		flag=true;
		flag2=0;
		forceFlag=false;
	}
}
