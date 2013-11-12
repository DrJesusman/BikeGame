using UnityEngine;
using System.Collections;

public class BulletDestroyScript : MonoBehaviour {
	Vector3 pos;
	float bulletSpeed;
	Vector3 bikePosition;
	// Use this for initialization
	void Start () {
		bulletSpeed = 5f;
		bikePosition = GameObject.Find("biket").transform.position/10;
	
	}
	
	// Update is called once per frame
	void Update () {
		pos = Camera.main.WorldToViewportPoint(transform.position);
		this.gameObject.transform.position += new Vector3(bikePosition.x, bulletSpeed/2 * Time.deltaTime, (4 * bulletSpeed) * Time.deltaTime);
		if(pos.x < 0.0 ||
			1.0 < pos.x ||
			pos.y < 0.0 ||
			1.0 < pos.y ||
			this.transform.position.z > 100f){
			GameObject.Destroy(this.gameObject);
		}
	}
}
