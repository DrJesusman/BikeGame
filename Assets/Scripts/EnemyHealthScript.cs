using UnityEngine;
using System.Collections;

public class EnemyHealthScript : MonoBehaviour {

	public int maxHp;
	public int currentHp;

	public GameObject explosion1;
	GameObject spawnedExplosionParticle;

	// Use this for initialization
	void Start () {
		currentHp = maxHp;
	}
	
	// Update is called once per frame
	void Update () {
		if(currentHp <= 0){
			StartCoroutine(deathAnimation());
		}
	}

	public void decreaseHealth(){
		currentHp -= PlayerVariablesScript.bulletDmg;
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("Bullet")){
			decreaseHealth();
		}
		if(other.gameObject.CompareTag("Road")){
			StartCoroutine(spawnAndDestroy());
		}
	}

	IEnumerator deathAnimation(){
		Destroy(gameObject.GetComponent<EnemyTestScript>());
		spawnedExplosionParticle = (GameObject)GameObject.Instantiate
			(explosion1, 
			 gameObject.transform.position, 
			 Quaternion.identity);
		spawnedExplosionParticle.transform.parent = gameObject.transform;
		yield return new WaitForSeconds(0.25f);
		GameObject.Destroy(spawnedExplosionParticle);
		/*gameObject.rigidbody.useGravity = true;
		gameObject.collider.isTrigger = false;*/
		transform.position = Vector3.Lerp(transform.position, transform.position - new Vector3(0f, 3f, 0f), 0.002f);
	}

	IEnumerator spawnAndDestroy(){
		spawnedExplosionParticle = (GameObject)GameObject.Instantiate
			(explosion1, 
			 gameObject.transform.position, 
			 Quaternion.identity);
		spawnedExplosionParticle.transform.parent = gameObject.transform;
		yield return new WaitForSeconds(0.25f);
		GameObject.Destroy(this.gameObject);
	}
}
