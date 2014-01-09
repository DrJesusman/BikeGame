using UnityEngine;
using System.Collections;

public class EyeballBossScript : MonoBehaviour {
	public GameObject eyeball;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		eyeball.transform.LookAt(GameObject.Find("Character").transform.position);
	}
}
