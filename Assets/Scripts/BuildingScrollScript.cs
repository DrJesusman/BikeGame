using UnityEngine;
using System.Collections;

public class BuildingScrollScript : MonoBehaviour {
	float buildingScrollSpeed;
	public GameObject buildingSpawn;
	public GameObject road;
	// Use this for initialization
	void Start () {
		buildingScrollSpeed = 8;
		Physics.IgnoreLayerCollision(8,9);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += -road.transform.up * buildingScrollSpeed * Time.deltaTime;
	}
	
	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("Building Reset")){
			this.gameObject.transform.position = new Vector3(this.transform.position.x,
				this.transform.position.y, buildingSpawn.transform.position.z);
		}
	}
}
