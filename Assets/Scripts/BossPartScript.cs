using UnityEngine;
using System.Collections;

public class BossPartScript : MonoBehaviour {
	int health;
	float maxHealth;
	int armor;
	float healthPercentage;
	bool isDead;
	bool exploded;
	float timeKilled;
	
	int explosionNumber;
	
	GameObject tempExplosion;
	public GameObject explosionPrefab1;
	public GameObject explosionPrefab2;
	public GameObject explosionPrefab3;
	
	GameObject bridge;
	// Use this for initialization
	void Start () {
		isDead = false;
		this.health = 20;
		maxHealth = 20f;
		bridge = GameObject.Find("BRIDGE");

		Debug.Log(gameObject.renderer.material.color.r + " " + gameObject.renderer.material.color.g +
		          " " + gameObject.renderer.material.color.r);
	}
	
	// Update is called once per frame
	void Update () {
		if(isDead){
			this.transform.parent = null;
			this.rigidbody.isKinematic = false;
			this.rigidbody.useGravity = true;
			this.rigidbody.AddForce(-bridge.transform.up,ForceMode.Force);
			this.transform.RotateAround(this.transform.position, this.transform.forward, 1f);
			if((Time.time - timeKilled) > 5){
				Destroy (this.gameObject);
			}
		}
		if(this.transform.position.y <= -5){
			Destroy(this.gameObject);
		}
	}
	
	void FixedUpdate(){
		if(health <= 0 && !exploded){
			explosionNumber = Random.Range(1,4);
			if(explosionNumber == 1){
				tempExplosion = (GameObject)GameObject.Instantiate(explosionPrefab1, this.gameObject.transform.position, Quaternion.identity);
			}
			else if(explosionNumber == 2){
				tempExplosion = (GameObject)GameObject.Instantiate(explosionPrefab2, this.gameObject.transform.position, Quaternion.identity);
			}
			else if(explosionNumber == 3){
				tempExplosion = (GameObject)GameObject.Instantiate(explosionPrefab3, this.gameObject.transform.position, Quaternion.identity);
			}
			tempExplosion.transform.parent = this.transform;
			isDead = true;
			exploded = true;
			timeKilled = Time.time;
		}
	}
	
	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("Bullet")){
			health -=1;
			healthPercentage = health / maxHealth;
			/*Debug.Log("r " + gameObject.renderer.material.color.r * healthPercentage +
			          " g " + gameObject.renderer.material.color.g * (1f - healthPercentage) +
			          " b " + gameObject.renderer.material.color.b * (1f - healthPercentage));*/
			gameObject.renderer.material.color = 
				new Color(gameObject.renderer.material.color.r - 0.1f * healthPercentage, 
				          gameObject.renderer.material.color.g * (1 - healthPercentage), 
				          gameObject.renderer.material.color.b * (1 - healthPercentage), 1f);
			Destroy(other.gameObject);
		}
		
	}
}
