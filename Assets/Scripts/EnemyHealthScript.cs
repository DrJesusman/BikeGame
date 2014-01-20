using UnityEngine;
using System.Collections;

public class EnemyHealthScript : MonoBehaviour {

	public int maxHp;
	public int currentHp;
	// Use this for initialization
	void Start () {
		currentHp = maxHp;
	}
	
	// Update is called once per frame
	void Update () {
		if(currentHp <= 0){
			GameObject.Destroy(this.gameObject);
		}
	}
}
