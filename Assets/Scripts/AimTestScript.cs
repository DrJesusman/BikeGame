using UnityEngine;
using System.Collections;

public class AimTestScript : MonoBehaviour {
	GameObject gun;
	GameObject aimer;

	// Use this for initialization
	void Start () {
		gun = GameObject.Find("defaultgun");
		aimer = GameObject.Find("Aimer");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Mouse0)){
			/*if(Input.mousePosition.x > (Screen.width * 0.33) && 
			   Input.mousePosition.x < (Screen.width * 0.66) && 
			   Input.mousePosition.y < (Screen.height / 2)){*/
			/*MousePosition = Input.mousePosition; 
			
			MousePosition.x = (Screen.height/2) - Input.mousePosition.y;
			
			MousePosition.y = -(Screen.width/2) + Input.mousePosition.x;*/
			aimer.transform.position = new Vector3((-Input.mousePosition.x / 25f) + 16, 
			                                       (-Input.mousePosition.y / 25f) + 8, 
			                                       aimer.transform.position.z);
			
			gun.transform.LookAt(aimer.transform.position, this.transform.forward);
			transform.right = (aimer.transform.position - transform.position).normalized;
			//}
		}
	}
}
