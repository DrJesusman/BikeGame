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
	GameObject[] carChildren = new GameObject[40];
	GameObject transparentChar;
	GameObject solidChar;

	float visibility = 20f;


	// Use this for initialization
	void Start () {
		difficulty = DifficultyScript.difficulty;
		transparentChar = (GameObject)this.transform.FindChild("Transparent Ground Enemy Container").gameObject;
		solidChar = (GameObject)this.transform.FindChild("Ground Enemy Container").gameObject;
		body = (GameObject)transparentChar.transform.FindChild("Body").gameObject;
		car = (GameObject)transparentChar.transform.FindChild("Car").gameObject;

		solidChar.SetActive(false);
		transparentChar.SetActive(true);
		makeTransparent(body, visibility);
		changeChildren(car, visibility);
		StartCoroutine(flash());

		for(int i = 0; i >= 39; i++){
			Debug.Log("changing: " + car.transform.GetChild(0));
		}


	}
	
	// Update is called once per frame
	void Update () {
		if(sendObstacle){
			this.transform.position += new Vector3(0, 0, -0.05f * difficulty);
		}
	}

	IEnumerator flash(){
		for(float i = visibility; i >= 0; i--){
			makeTransparent(body, i/100f);
			changeChildren(car, i/100f);
			i -= DifficultyScript.difficulty/2;
			yield return new WaitForSeconds(0.05f);
			//Debug.Log(car.renderer.sharedMaterial.color.a);
			//car.SetActive(true);
			/*makeInvisible(body);
			makeInvisible(car);*/
			//yield return new WaitForSeconds(1.0f / difficulty);
		}
		makeVisible(body);
		car.SetActive(true);
		transparentChar.SetActive(false);
		solidChar.SetActive(true);
		this.transform.position += new Vector3(0f, 0f, 10f);
		sendObstacle = true;
	}

	void makeTransparent(GameObject g, float percentage){
		g.renderer.material.color = new Color(g.renderer.material.color.r,
		                                        g.renderer.material.color.g,
		                                        g.renderer.material.color.b,
		                                       percentage);
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

	void changeChildren(GameObject g, float alpha){
		foreach(Transform child in car.transform){
			makeTransparent(child.gameObject, alpha);
		}
	}
}
