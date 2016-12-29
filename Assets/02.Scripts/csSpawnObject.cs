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

		StartCoroutine ("spawnObject");
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0.0f)
			StopAllCoroutines ();
	}

	IEnumerator spawnObject(){
		while (true) {
			yield return new WaitForSeconds (1.0f);

			GameObject obj = null;

			//make coin or sprikeball
			float randomSpawnX = Random.Range(-6.0f, 6.0f);
			float randomSpawnZ = Random.Range (-7.0f, 4.0f);

			while ((randomSpawnX < 5.0f && randomSpawnX > -5.0f) ||
				(randomSpawnZ < 3.0f && randomSpawnZ > -6.0f)) {

				yield return null;

				//if ((randomSpawnX < 5.0f && randomSpawnX > -5.0f) ||
				  //(randomSpawnZ < 3.0f && randomSpawnZ > -6.0f)) {
					randomSpawnX = Random.Range (-6.0f, 6.0f);
					randomSpawnZ = Random.Range (-7.0f, 4.0f);
				//}
			}

			Debug.Log ("X : " + randomSpawnX + " , Z: " + randomSpawnZ);
		
			//Debug.Log ("X : " + randomSpawnX + " , Z: " + randomSpawnZ);

			int whichOne = Random.Range (0, 5);
			obj = whichOne < 2 ? coin : spike;

			GameObject tmp = Instantiate (obj) as GameObject;

			tmp.transform.position = new Vector3 (randomSpawnX, 2.5f, randomSpawnZ);

			Vector3 dir = tmp.transform.position - ranInto.position;
			dir.Normalize ();

			tmp.GetComponent<Rigidbody> ().AddForce (dir*power* -1);
		}
	}

	void FixedUpdate(){
		perOneSec += Time.deltaTime;
		if (perOneSec >= 1.0f) {
			perOneSec = 0.0f;
		}
	}
}
