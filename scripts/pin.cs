using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pin : MonoBehaviour {
	
	private Vector3 Position;
	private Rigidbody rb;
	private AudioSource strike;

	void Start () {

		Position = transform.position;	
		rb = GetComponent<Rigidbody> ();
	}

	void OnCollisionEnter(Collision col)
	{

		if (col.gameObject.name == "Ball") {
			
			strike = GetComponent<AudioSource> ();
			strike.Play ();
		}

	}
	public bool standing(){
		if (transform.up.y > 0.6f ) {
			
			return true;
		}else{
			
			return false;
		}
	}


	public void reset(){
		
		transform.position = Position;
		transform.rotation =Quaternion.identity;
		rb.isKinematic=false;
	}

	public void standingReset(){

		if(standing()){
			transform.position = Position;
			transform.rotation =Quaternion.identity;

		}
		else{

			Vector3  cleanPos = new Vector3 (4, 2, 23);

			rb.isKinematic=true;
			transform.position = cleanPos;
		}

	}


}
