using UnityEngine;
using System.Collections;

public class ShootingScript : MonoBehaviour {
	public static string currentBullet;
	public static float fireRate;
	private float fireTime;
	
	GameObject player;
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
				GameObject.Instantiate(normalPrefab, new Vector3(this.transform.position.x,
					this.transform.position.y+ 0.2f, this.transform.position.z), player.transform.rotation);
				fireTime = Time.time;
			}
		}
	}
}
