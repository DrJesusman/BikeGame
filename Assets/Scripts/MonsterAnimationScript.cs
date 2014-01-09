using UnityEngine;
using System.Collections;

public class MonsterAnimationScript : MonoBehaviour {
	
	float lastPlayedTime;
	int choice;
	string[] animationNameArray;
	public static float difficulty;
	
	float lastDifficultyIncrease;
	
	GameObject leftEye;
	GameObject rightEye;
	// Use this for initialization
	void Start () {
		difficulty = 10;
		choice = 0;
		animationNameArray = new string[3] {"Left Eye Move", "Right Eye Move", "Eye Circle"};
		
		leftEye = GameObject.Find("Left Eye");
		rightEye = GameObject.Find("Right Eye");
		//animation.Play("Right Eye Move");
	}
	
	// Update is called once per frame
	void Update () {
		if( (Time.time - lastPlayedTime) > (5 * (0.1 * difficulty)) ){
			bossMove(animationNameArray[Random.Range(0,animationNameArray.Length)]);
		}
		if(leftEye != null && leftEye.transform.parent == null){
			if(animation.IsPlaying("Left Eye Move")){
				animation.Stop("Left Eye Move");
			}
			if(animation.IsPlaying("Eye Circle")){
				animation.Stop("Eye Circle");
			}
			animationNameArray = new string[1] {"Right Eye Move"};
		}
		if(rightEye != null && rightEye.transform.parent == null){
			if(animation.IsPlaying("Right Eye Move")){
				animation.Stop("Right Eye Move");
			}
			if(animation.IsPlaying("Eye Circle")){
				animation.Stop("Eye Circle");
			}
			animationNameArray = new string[1] {"Left Eye Move"};
		}
		
		if(Time.time - lastDifficultyIncrease > 3){
			difficulty *= 0.9f;
			lastDifficultyIncrease = Time.time;
		}
		
	}
	
	void bossMove(string an){
		animation.PlayQueued(an,QueueMode.CompleteOthers ,PlayMode.StopAll);
		lastPlayedTime = Time.time;
		choice = 0;
	}
}
