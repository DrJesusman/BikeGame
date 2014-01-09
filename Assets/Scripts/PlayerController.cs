using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	bool debug;
	bool grounded;
	float moveSpeed;
	float jumpStrength;
	float jumpLength;
	float gravity;
	float jumpStartTime;
	Vector3 startingPosition;
	Quaternion startingRotation;

	GameObject gun;
	GameObject aimer;
	GameObject aimerLeft;
	GameObject aimerRight;
	int xAimOffset = 63;
	int yAimOffset = 5;
	float xAimOffsetRight = 9f;
	float yAimOffsetRight = -4f;
	float xAimOffsetLeft = 9f;
	float yAimOffsetLeft = -4f;

	/* 1280x720
	 * float xAimOffsetRight = 9f;
	float yAimOffsetRight = -4f;
	float xAimOffsetLeft = 9f;
	float yAimOffsetLeft = -4f;
	*/

	GameObject arrowTextureLeft;
	GameObject arrowTextureRight;
	GameObject shootTextureBottom;
	GameObject shootTextureSmallLeft;
	GameObject shootTextureSmallRight;
	GameObject jumpTexture;

	bool tiltingRight;
	bool tiltingLeft;
	
	ShootingScript shootingScript;
	GameObject seatPackage;
	GameObject seat;
	Touch t;

	public GameObject upperBodyLeft;
	public GameObject upperBodyRight;

	// Use this for initialization
	void Start () {
		debug = true;
		grounded = true;
		tiltingLeft = false;
		tiltingRight = false;

		upperBodyRight.SetActive(false);
		
		moveSpeed = 1f;
		gravity = 1.5f;
		jumpStrength = 18f;
		jumpLength = 0.35f;

		shootingScript = (ShootingScript)this.gameObject.GetComponent("ShootingScript");
		seatPackage = GameObject.Find("Seat Package");
		seat = GameObject.Find("Seat");
		startingPosition = this.transform.position;
		startingRotation = seatPackage.transform.rotation;

		gun = GameObject.Find("defaultgun");
		aimer = GameObject.Find("Aimer");
		aimerLeft = GameObject.Find("Aimer Left");
		aimerRight = GameObject.Find("Aimer Right");

		arrowTextureLeft = GameObject.Find("Arrow Texture Left");
		arrowTextureRight = GameObject.Find("Arrow Texture Right");
		shootTextureBottom = GameObject.Find("Shoot Texture Bottom");
		shootTextureSmallLeft = GameObject.Find("Shoot Texture Small Left");
		shootTextureSmallRight = GameObject.Find("Shoot Texture Small Right");
		jumpTexture = GameObject.Find("Jump Texture");

		arrowTextureLeft.SetActive(false);
		arrowTextureRight.SetActive(false);
		shootTextureBottom.SetActive(false);
		shootTextureSmallLeft.SetActive(false);
		shootTextureSmallRight.SetActive(false);
		jumpTexture.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		arrowTextureLeft.SetActive(false);
		arrowTextureRight.SetActive(false);
		shootTextureBottom.SetActive(false);
		shootTextureSmallLeft.SetActive(false);
		shootTextureSmallRight.SetActive(false);
		jumpTexture.SetActive(false);
		if(!debug){
			if(Input.touchCount > 0){
				for(int i = 0; i < Input.touchCount; i++){
					t = Input.GetTouch(i);
					//Move Left
					if(t.position.x < (Screen.width * 0.25) && t.position.y < Screen.height / 2){
						tiltLeft();
						arrowTextureLeft.SetActive(true);
						if(this.transform.position.x > -1){
							this.gameObject.transform.position -= Vector3.right * moveSpeed * Time.deltaTime;
						}
					}
					//Move right
					if(t.position.x > (Screen.width * 0.75) && t.position.y < Screen.height / 2){
						tiltRight();
						arrowTextureRight.SetActive(true);
						if(this.transform.position.x < 1){
							this.gameObject.transform.position += Vector3.right * moveSpeed * Time.deltaTime;
						}
					}
					//Fire foreground right
					if(t.position.x > (Screen.width * 0.75) && t.position.y > Screen.height / 2){
						shootTextureSmallRight.SetActive(true);
						tiltRight();
						aimerRight.transform.position = new Vector3(
							((t.position.x / 500f) * (t.position.x - (Screen.width * (7/8)))/200f) - xAimOffsetRight, 
							((t.position.y / 500f) * (t.position.y - (Screen.height * (0.75f)))/75f) - yAimOffsetRight, 
							aimerRight.transform.position.z);
						
						gun.transform.LookAt(aimerRight.transform.position, gun.transform.forward);
						gun.transform.right = (aimerRight.transform.position - gun.transform.position).normalized;
						shootingScript.FireForegroundRight();
					}
					//Fire foreground Left
					if(t.position.x < (Screen.width * 0.25) && t.position.y > Screen.height / 2){
						tiltLeft();
						shootTextureSmallLeft.SetActive(true);
						aimerLeft.transform.position = new Vector3(
							((t.position.x / 500f) * (t.position.x - (Screen.width * (1/8)))/25f) - xAimOffsetLeft, 
							((t.position.y / 500f) * (t.position.y - (Screen.height * (0.75f)))/75f) - yAimOffsetLeft, 
							aimerRight.transform.position.z);
						
						gun.transform.LookAt(aimerLeft.transform.position, gun.transform.forward);
						gun.transform.right = (aimerLeft.transform.position - gun.transform.position).normalized;
						shootingScript.FireForegroundLeft();
					}
					
					//Jump
					if(t.position.x > (Screen.width * 0.25) && 
						t.position.x < (Screen.width * 0.75) && 
						t.position.y > (Screen.height / 2) &&
						grounded){
						jumpTexture.SetActive(true);
						grounded = false;
						jumpStartTime = Time.time;
					}
					
					//Fire
					if(t.position.x > (Screen.width * 0.25) && 
						t.position.x < (Screen.width * 0.75) && 
						t.position.y < (Screen.height / 2)){
						shootTextureBottom.SetActive(false);
						aimer.transform.position = new Vector3((t.position.x / 10f) - xAimOffset, 
						                                       (t.position.y / 10f) - yAimOffset, 
						                                       aimer.transform.position.z);
						
						gun.transform.LookAt(aimer.transform.position, gun.transform.forward);
						gun.transform.right = (aimer.transform.position - gun.transform.position).normalized;
						shootingScript.Fire();
					}
					//this.transform.rotation = Quaternion.identity;
					if(grounded ){
						seatPackage.transform.rotation = 
							Quaternion.Slerp(seatPackage.transform.rotation, Quaternion.Euler(0f, 180f, 0f), 20 * Time.deltaTime);
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
				//Move Left
				if(Input.mousePosition.x < (Screen.width * 0.25) && Input.mousePosition.y < Screen.height / 2){
					tiltLeft();
					arrowTextureLeft.SetActive(true);
					if(this.transform.position.x > -1){
						this.gameObject.transform.position -= Vector3.right * moveSpeed * Time.deltaTime;
					}
				}
				//Move right
				if(Input.mousePosition.x > (Screen.width * 0.75) && Input.mousePosition.y < Screen.height / 2){
					tiltRight();
					arrowTextureRight.SetActive(true);
					if(this.transform.position.x < 1){
						this.gameObject.transform.position += Vector3.right * moveSpeed * Time.deltaTime;
					}
				}
				//Fire foreground right
				if(Input.mousePosition.x > (Screen.width * 0.75) && Input.mousePosition.y > Screen.height / 2){
					tiltRight();
					shootTextureSmallRight.SetActive(true);
					aimerRight.transform.position = new Vector3(
						((Input.mousePosition.x / 500f) * (Input.mousePosition.x - (Screen.width * (7/8)))/200f) - xAimOffsetRight, 
						((Input.mousePosition.y / 500f) * (Input.mousePosition.y - (Screen.height * (0.75f)))/75f) - yAimOffsetRight, 
					                                           aimerRight.transform.position.z);
					
					gun.transform.LookAt(aimerRight.transform.position, gun.transform.forward);
					gun.transform.right = (aimerRight.transform.position - gun.transform.position).normalized;
					shootingScript.FireForegroundRight();
				}
				//Fire foreground Left
				if(Input.mousePosition.x < (Screen.width * 0.25) && Input.mousePosition.y > Screen.height / 2){
					tiltLeft();
					shootTextureSmallLeft.SetActive(true);
					aimerLeft.transform.position = new Vector3(
						((Input.mousePosition.x / 500f) * (Input.mousePosition.x - (Screen.width * (1/8)))/25f) - xAimOffsetLeft, 
					    ((Input.mousePosition.y / 500f) * (Input.mousePosition.y - (Screen.height * (0.75f)))/75f) - yAimOffsetLeft, 
					                                           aimerRight.transform.position.z);
					
					gun.transform.LookAt(aimerLeft.transform.position, gun.transform.forward);
					gun.transform.right = (aimerLeft.transform.position - gun.transform.position).normalized;
					shootingScript.FireForegroundLeft();
				}
							
				//Jump
				if(Input.mousePosition.x > (Screen.width * 0.25) && 
					Input.mousePosition.x < (Screen.width * 0.75) && 
					Input.mousePosition.y > (Screen.height / 2) &&
					grounded){
					jumpTexture.SetActive(true);
					grounded = false;
					jumpStartTime = Time.time;
				}
							
				//Fire
				if(Input.mousePosition.x > (Screen.width * 0.25) && 
					Input.mousePosition.x < (Screen.width * 0.74) && 
					Input.mousePosition.y < (Screen.height / 2)){
					shootTextureBottom.SetActive(true);
					aimer.transform.position = new Vector3((Input.mousePosition.x / 10f) - xAimOffset, 
					                                       (Input.mousePosition.y / 10f) - yAimOffset, 
					                                       aimer.transform.position.z);
					
					gun.transform.LookAt(aimer.transform.position, gun.transform.forward);
					gun.transform.right = (aimer.transform.position - gun.transform.position).normalized;
					shootingScript.Fire();
				}
			}
		/*else{
				//this.transform.rotation = Quaternion.identity;
				if(grounded ){
					this.transform.rotation = 
						Quaternion.Slerp(this.transform.rotation, startingRotation, 20 * Time.deltaTime);
					seatPackage.transform.position = this.transform.position;
				}
				if(seatPackage.transform.rotation.z < -0.007){
					seatPackage.transform.RotateAround(seat.renderer.bounds.center, new Vector3(0,0,1f), 2.5f);
				}
				else if(seatPackage.transform.rotation.z > 0.007){
					seatPackage.transform.RotateAround(seat.renderer.bounds.center, new Vector3(0,0,1f), -2.5f);
				}
			}*/
		}

		if(!grounded){
			if((Time.time - jumpStartTime) < jumpLength){
				//animation.Play("jump");
				this.gameObject.transform.position += Vector3.up * jumpStrength * Time.deltaTime 
					* (jumpLength - (Time.time - jumpStartTime));
			}
			//apply gravity
			this.gameObject.transform.position -= Vector3.up * gravity * Time.deltaTime;
			if(this.transform.position.y <= startingPosition.y){
				grounded = true;
			}
		}
					
	}
	
	void tiltRight(){
		/*if(seatPackage.transform.rotation.z >= -0.1f){
			seatPackage.transform.RotateAround(seat.renderer.bounds.center, new Vector3(0,0,1f), -2.5f);
		}*/
		if(upperBodyRight.activeSelf == true && upperBodyLeft.activeSelf == false){
			upperBodyRight.SetActive(false);
			upperBodyLeft.SetActive(true);
			gun = GameObject.Find("defaultgun");
		}

	}
	
	void tiltLeft(){
		/*if(seatPackage.transform.rotation.z <= 0.1f){
			seatPackage.transform.RotateAround(seat.renderer.bounds.center, new Vector3(0,0,1f), 2.5f);
		}*/
		if(upperBodyRight.activeSelf == false && upperBodyLeft.activeSelf == true){
			upperBodyRight.SetActive(true);
			upperBodyLeft.SetActive(false);
			gun = GameObject.Find("defaultgun");
		}
	}
}
