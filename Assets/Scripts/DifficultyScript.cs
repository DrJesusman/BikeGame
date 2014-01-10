using UnityEngine;
using System.Collections;

public class DifficultyScript : MonoBehaviour {
	public static int difficulty = 2;
	public static float lastIncrease;

	string difficultyType = "Easy";

	void Update(){
		/*if(Time.time - lastIncrease >= 5f){
			difficulty += 1;
			lastIncrease = Time.time;
			Debug.Log(difficulty);
		}*/

		if(Input.GetKeyDown(KeyCode.Space)){
			if(difficultyType == "Easy"){
				DifficultyScript.difficulty += 2;
				difficultyType = "Medium";
			}

			else if(difficultyType == "Medium"){
				DifficultyScript.difficulty += 2;
				difficultyType = "Hard";
			}

			else if(difficultyType == "Hard"){
				DifficultyScript.difficulty = 2;
				difficultyType = "Easy";
			}

			Debug.Log ("Setting Difficulty to: " + difficultyType);
		}
	}
}
