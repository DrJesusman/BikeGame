using UnityEngine;
using System.Collections;

public class DifficultyScript : MonoBehaviour {
	public static int difficulty = 1;
	public static float lastIncrease;

	void Update(){
		if(Time.time - lastIncrease >= 5f){
			difficulty += 1;
			lastIncrease = Time.time;
			Debug.Log(difficulty);
		}
	}
}
