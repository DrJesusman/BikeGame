using UnityEngine;
using System.Collections;

public class ObstacleScript : MonoBehaviour {

	float difficulty;
	float spawnSpeed;
	int numOfFlashes = 3;
	public float spawnPositionY = 0.09f;
	bool sendObstacle = false;

	float lastSpawnTime;

	public GameObject childObject;
	GameObject body;
	GameObject car;
	GameObject transparentChar;
	GameObject solidChar;


	// Use this for initialization
	void Start () {
		difficulty = DifficultyScript.difficulty;
		transparentChar = (GameObject)this.transform.FindChild("Transparent Ground Enemy Container").gameObject;
		solidChar = (GameObject)this.transform.FindChild("Ground Enemy Container").gameObject;
		body = (GameObject)transparentChar.transform.FindChild("Body").gameObject;
		car = (GameObject)transparentChar.transform.FindChild("Car").gameObject;

		solidChar.SetActive(false);
		makeTransparent(body);
		makeTransparent(car);
		StartCoroutine(flash());

	}
	
	// Update is called once per frame
	void Update () {
		if(sendObstacle){
			this.transform.position += new Vector3(0, 0, -0.05f * difficulty);
		}
	}

	IEnumerator flash(){
		for(int i = numOfFlashes; i >= 0; i--){
			makeTransparent(body);
			car.SetActive(true);
			yield return new WaitForSeconds(1.0f / difficulty);
			makeInvisible(body);
			car.SetActive(false);
			yield return new WaitForSeconds(1.0f / difficulty);
		}
		makeVisible(body);
		car.SetActive(true);
		transparentChar.SetActive(false);
		solidChar.SetActive(true);
		this.transform.position += new Vector3(0f, 0f, 10f);
		sendObstacle = true;
	}

	void makeTransparent(GameObject g){
		g.renderer.material.color = new Color(g.renderer.material.color.r,
		                                        g.renderer.material.color.g,
		                                        g.renderer.material.color.b,
		                                        0.5f);
	}

	void makeInvisible(GameObject g){
		g.renderer.material.color = new Color(g.renderer.material.color.r,
		                                      g.renderer.material.color.g,
		                                      g.renderer.material.color.b,
		                                      0f);
	}

	void makeVisible(GameObject g){
		g.renderer.material.color = new Color(g.renderer.material.color.r,
		                                      g.renderer.material.color.g,
		                                      g.renderer.material.color.b,
		                                      1f);
	}
}
