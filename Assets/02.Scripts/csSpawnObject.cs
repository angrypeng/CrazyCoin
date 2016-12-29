using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csSpawnObject : MonoBehaviour {

	public GameObject coin;
	public GameObject spike;

	float power = 500.0f;

	public Transform ranInto;

	float perOneSec = 0.0f;

	// Use this for initialization
	void Start () {
		int seeds;
		seeds = (int)System.DateTime.Now.Ticks;
		Random.seed = seeds;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		perOneSec += Time.deltaTime;
		if (perOneSec >= 1.0f) {
			perOneSec = 0.0f;
		}
	}
}
