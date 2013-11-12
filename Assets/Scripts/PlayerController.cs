using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	bool debug;
	bool grounded;
	float moveSpeed;
	float jumpLength;
	float gravity;
	float jumpStartTime;
	Vector3 startingPosition;
	Quaternion startingRotation;
	
	bool tiltingRight;
	bool tiltingLeft;
	
	ShootingScript shootingScript;
	GameObject seatPackage;
	Touch t;
	// Use this for initialization
	void Start () {
		debug = true;
		grounded = true;
		tiltingLeft = false;
		tiltingRight = false;
		
		moveSpeed = 1f;
		gravity = 1f;
		jumpLength = 0.25f;
		startingPosition = this.transform.position;
		startingRotation = this.transform.rotation;
		shootingScript = (ShootingScript)this.gameObject.GetComponent("ShootingScript");
		seatPackage = GameObject.Find("Seat Package");
	}
	
	// Update is called once per frame
	void Update () {
		if(!debug){
			if(Input.touchCount > 0){
				for(int i = 0; i < Input.touchCount; i++){
						t = Input.GetTouch(i);
						//Move Left
						if(t.position.x < (Screen.width * 0.33) && t.position.y < Screen.height / 2){
							tiltLeft();
							if(this.transform.position.x > -0.5){
								this.gameObject.transform.position -= Vector3.right * moveSpeed * Time.deltaTime;
							}
						}
						//Fire move right
						if(t.position.x > (Screen.width * 0.66) && t.position.y > Screen.height / 2){
							tiltRight();
							if(this.transform.position.x < 0.5){
								this.gameObject.transform.position += Vector3.right * moveSpeed/2 * Time.deltaTime;
							}
							shootingScript.Fire();
						}
						//Fire move Left
						if(t.position.x < (Screen.width * 0.33) && t.position.y > Screen.height / 2){
							tiltLeft();
							if(this.transform.position.x > -0.5){
								this.gameObject.transform.position -= Vector3.right * moveSpeed/2 * Time.deltaTime;
							}
							shootingScript.Fire();
						}
						
						//Jump
						if(t.position.x > (Screen.width * 0.33) && 
							t.position.x < (Screen.width * 0.66) && 
							t.position.y > (Screen.height / 2) &&
							grounded){
							grounded = false;
							jumpStartTime = Time.time;
						}
						
						//Fire
						if(t.position.x > (Screen.width * 0.33) && 
							t.position.x < (Screen.width * 0.66) && 
							t.position.y < (Screen.height / 2)){
							shootingScript.Fire();
						}
						Debug.Log (this.transform.rotation.eulerAngles);
						//this.transform.rotation = Quaternion.identity;
						if(grounded ){
							this.transform.rotation = 
								Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0f, 180f, 0f), 20 * Time.deltaTime);
						}
						if(seatPackage.transform.rotation.z < -0.007){
							seatPackage.transform.RotateAround(this.transform.position, new Vector3(0,0,1f), 2.5f);
						}
						else if(seatPackage.transform.rotation.z > 0.007){
							seatPackage.transform.RotateAround(this.transform.position, new Vector3(0,0,1f), -2.5f);
						}
				}
			}
		}
		//MOUSE CONTROL
		else{
			if(Input.GetKey(KeyCode.Mouse0)){
				//Move right
				if(Input.mousePosition.x > (Screen.width * 0.66) && Input.mousePosition.y < Screen.height / 2){
					tiltRight();
					if(this.transform.position.x < 0.5){
						this.gameObject.transform.position += Vector3.right * moveSpeed * Time.deltaTime;
					}
				}
				//Move Left
				if(Input.mousePosition.x < (Screen.width * 0.33) && Input.mousePosition.y < Screen.height / 2){
					tiltLeft();
					if(this.transform.position.x > -0.5){
						this.gameObject.transform.position -= Vector3.right * moveSpeed * Time.deltaTime;
					}
				}
				//Fire move right
				if(Input.mousePosition.x > (Screen.width * 0.66) && Input.mousePosition.y > Screen.height / 2){
					tiltRight();
					if(this.transform.position.x < 0.5){
						this.gameObject.transform.position += Vector3.right * moveSpeed/2 * Time.deltaTime;
					}
					shootingScript.Fire();
				}
				//Fire move Left
				if(Input.mousePosition.x < (Screen.width * 0.33) && Input.mousePosition.y > Screen.height / 2){
					tiltLeft();
					if(this.transform.position.x > -0.5){
						this.gameObject.transform.position -= Vector3.right * moveSpeed/2 * Time.deltaTime;
					}
					shootingScript.Fire();
				}
							
				//Jump
				if(Input.mousePosition.x > (Screen.width * 0.33) && 
					Input.mousePosition.x < (Screen.width * 0.66) && 
					Input.mousePosition.y > (Screen.height / 2) &&
					grounded){
					grounded = false;
					jumpStartTime = Time.time;
				}
							
				//Fire
				if(Input.mousePosition.x > (Screen.width * 0.33) && 
					Input.mousePosition.x < (Screen.width * 0.66) && 
					Input.mousePosition.y < (Screen.height / 2)){
					shootingScript.Fire();
				}
			}
		else{
				//this.transform.rotation = Quaternion.identity;
				if(grounded ){
					this.transform.rotation = 
						Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0f, 180f, 0f), 20 * Time.deltaTime);
				}
				if(seatPackage.transform.rotation.z < -0.007){
					seatPackage.transform.RotateAround(this.transform.position, new Vector3(0,0,1f), 2.5f);
				}
				else if(seatPackage.transform.rotation.z > 0.007){
					seatPackage.transform.RotateAround(this.transform.position, new Vector3(0,0,1f), -2.5f);
				}
			}
		}

		if(!grounded){
			if((Time.time - jumpStartTime) < jumpLength){
				this.transform.rotation = Quaternion.Slerp
					(this.transform.rotation, Quaternion.Euler(-45f,180f,0f) , 20 * Time.deltaTime);
				this.gameObject.transform.position += Vector3.up * moveSpeed * Time.deltaTime;
			}
			else{
				//apply gravity
				this.gameObject.transform.position -= Vector3.up * gravity * Time.deltaTime;
				if(this.transform.position.y <= startingPosition.y){
					grounded = true;
				}
			}
		}
					
	}
	
	void tiltRight(){
		if(seatPackage.transform.rotation.z >= -0.1f){
			seatPackage.transform.RotateAround(this.transform.position, new Vector3(0,0,1f), -2.5f);
		}
	}
	
	void tiltLeft(){
		if(seatPackage.transform.rotation.z <= 0.1f){
			seatPackage.transform.RotateAround(this.transform.position, new Vector3(0,0,1f), 2.5f);
		}
	}
}
