using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour {

	float time = 0; 
	float timeMax = 20f; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Timer ();
	}

	void Timer() {
		if (time < timeMax) {
			transform.position = transform.position + new Vector3(-1.0f, 0.0f, -1.0f);
		} 
		time = time + 1;
	}
}
