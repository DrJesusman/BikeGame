using UnityEngine;
using System.Collections;

public class ShootingScript : MonoBehaviour {
	public static string currentBullet;
	public static float fireRate;
	private float fireTime;

	GameObject player;
	GameObject spawnedBullet;
	Quaternion lookDirection;
	//bullet prefabs
	public GameObject normalPrefab;
	// Use this for initialization
	void Start () {
		currentBullet = "Normal Bullet";
		fireRate = 0.1f;
		player = GameObject.Find("Seat Package");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void Fire(){
		if((Time.time - fireTime) > fireRate){
			if(currentBullet == "Normal Bullet"){
				spawnedBullet = (GameObject)GameObject.Instantiate
					(normalPrefab, GameObject.Find("Bullet Spawn").transform.position, 
					 GameObject.Find("defaultgun").transform.rotation);

				fireTime = Time.time;
			}
		}
	}

	public void FireForegroundRight(){
		if((Time.time - fireTime) > fireRate){
			if(currentBullet == "Normal Bullet"){
				spawnedBullet = (GameObject)GameObject.Instantiate
					(normalPrefab, GameObject.Find("Bullet Spawn").transform.position, 
					 GameObject.Find("defaultgun").transform.rotation);
				fireTime = Time.time;
			}
		}
	}

	public void FireForegroundLeft(){
		if((Time.time - fireTime) > fireRate){
			if(currentBullet == "Normal Bullet"){
				spawnedBullet = (GameObject)GameObject.Instantiate
					(normalPrefab, GameObject.Find("Bullet Spawn").transform.position, 
					 GameObject.Find("defaultgun").transform.rotation);
				fireTime = Time.time;
			}
		}
	}

}
