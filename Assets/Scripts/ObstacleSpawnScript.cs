using UnityEngine;
using System.Collections;

public class ObstacleSpawnScript : MonoBehaviour {
	
	public int difficulty;
	float spawnSpeed;
	public float spawnPositionY;
	float spawnPositionX;
	
	float lastSpawnTime;
	
	public GameObject groundEnemy;
	public GameObject floor;
	
	// Use this for initialization
	void Start () {
		difficulty = DifficultyScript.difficulty;
		spawnSpeed = 10f;
		lastSpawnTime = -0f;
		floor = GameObject.Find("BRIDGE");
		spawnPositionY = this.transform.position.y;// + (groundEnemy.transform.localScale.y);
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - lastSpawnTime >= spawnSpeed / (0.5 * difficulty)){
			Spawn();
			lastSpawnTime = Time.time;
		}
	}
	
	void Spawn(){
		spawnPositionX = Random.Range(-0.5f, 0.51f);
		GameObject.Instantiate(groundEnemy, new Vector3(spawnPositionX, spawnPositionY, 42f), Quaternion.identity);
	}
}