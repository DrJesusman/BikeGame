using UnityEngine;
using System.Collections;

public class BulletDestroyScript : MonoBehaviour {
	Vector3 pos;
	float bulletSpeed;
	Vector3 bikePosition;

	public GameObject bulletHitParticle;
	GameObject spawnedBulletParticle;
	// Use this for initialization
	void Start () {
		bikePosition = GameObject.Find("biket").transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(this.transform.position.z >= 42.15f){
			bulletSpeed = 0.15f;
		}
		else{
			bulletSpeed = 0.025f;
		}
		pos = Camera.main.WorldToViewportPoint(transform.position);
		//transform.position += transform.TransformDirection(this.transform.up)*bulletSpeed;
		transform.Translate(new Vector3(1f * bulletSpeed,0f,0f), this.transform);
		if(pos.x < 0.0 ||
			1.0 < pos.x ||
			pos.y < 0.0 ||
			1.0 < pos.y ||
			this.transform.position.z > 100f){
			GameObject.Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter(Collider c){
		if(c.gameObject.CompareTag("Enemy") || c.gameObject.CompareTag("Boss Part")){
			StartCoroutine(spawnAndDestroy(c));
		}
	}

	IEnumerator spawnAndDestroy(Collider collidedWith){
		spawnedBulletParticle = (GameObject)GameObject.Instantiate
			(bulletHitParticle, 
			 collidedWith.gameObject.transform.position - (collidedWith.gameObject.transform.position - bikePosition).normalized/4, 
			 Quaternion.identity);
		spawnedBulletParticle.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
		spawnedBulletParticle.transform.parent = collidedWith.gameObject.transform;
		yield return new WaitForSeconds(0.25f);
		GameObject.Destroy(spawnedBulletParticle);
		GameObject.Destroy(this.gameObject);
	}
}
