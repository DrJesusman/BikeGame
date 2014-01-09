using UnityEngine;
using System.Collections;

public class BackGroundScrollScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += new Vector3(0f, 0f, 0.01f);
	}
}
