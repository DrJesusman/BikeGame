using UnityEngine;
using System.Collections;

public class PlayerCollisionScript : MonoBehaviour {

	public GameObject characterHitFlames;
	GameObject spawnedCharacterHitFlames;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("Obstacle")){
			StartCoroutine(characterHit());
		}

	}

	IEnumerator characterHit(){
		Debug.Log("Character hit by obstacle");
		spawnedCharacterHitFlames = (GameObject)GameObject.Instantiate
			(characterHitFlames, this.transform.position, Quaternion.identity);
		yield return new WaitForSeconds(1f);
		GameObject.Destroy(spawnedCharacterHitFlames);
	}

}
