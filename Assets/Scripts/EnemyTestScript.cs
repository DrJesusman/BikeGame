using UnityEngine;
using System.Collections;

public class EnemyTestScript : MonoBehaviour {

	bool changed;
	bool first;
	bool needNewPoint = true;

	int lastUsedPosition;
	int rand;

	float smooth = 1f;

	Vector3 rallyPointStorage;
	Vector3 nextRallyPoint = Vector3.zero;
	
	Vector3[] rallyPoints = new Vector3[3];

	void Start () {
		rallyPoints[0] = GameObject.Find("Rally Point 0").gameObject.transform.position;
		rallyPoints[1] = GameObject.Find("Rally Point 1").gameObject.transform.position;
		rallyPoints[2] = GameObject.Find("Rally Point 2").gameObject.transform.position;
		first = true;
		needNewPoint = true;
		Physics.IgnoreLayerCollision(9,10);
	}
	
	// Update is called once per frame

	/*
	 * nextRallyPoint		0
	 * rallyPointStorage	a
	 * lastUsedPosition 	0
	 * rallyPoints			(0, b, c)

	*/
	void Update () {
		if(needNewPoint){
			getNextRallyPoint();
			needNewPoint = false;
		}
		//StartCoroutine(rallyDebugDisplay());
		transform.position = Vector3.Lerp(transform.position, nextRallyPoint - new Vector3(0f, 0.15f,0f), smooth * Time.deltaTime);
	}

	IEnumerator rallyDebugDisplay(){
		yield return new WaitForSeconds(1.5f);
		nextRallyPoint = Vector3.zero;
	}

	void OnTriggerEnter(Collider other){
		if(!changed && other.gameObject.name == "Rally Point " + lastUsedPosition){
			nextRallyPoint = Vector3.zero;
			changed = true;
			needNewPoint = true;
		}
	}

	void getNextRallyPoint(){
		if(lastUsedPosition == 0){
			rand = Random.Range(1,3);
		}
		else if(lastUsedPosition == 1){
			rand = Random.Range(0,2);
			if(rand == 1){
				rand = 2;
			}
		}
		else if(lastUsedPosition == 2){
			rand = Random.Range(0,2);
		}
		else{
			rand = Random.Range(0,3);
		}
		nextRallyPoint = rallyPoints[rand];
		rallyPoints[rand] = Vector3.zero;
		changed = false;
		if(!first){
			rallyPoints[lastUsedPosition] = rallyPointStorage;
		}
		else{
			first = false;
		}
		lastUsedPosition = rand;
		rallyPointStorage = nextRallyPoint;
	}
}
