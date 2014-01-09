using UnityEngine;
using System.Collections;

public class FloorScroll : MonoBehaviour {
	
	float scrollSpeed;
	
	// Use this for initialization
	void Start () {
		scrollSpeed = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		float offset = Time.time * scrollSpeed;
		renderer.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
	}
}
