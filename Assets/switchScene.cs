using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchScene : MonoBehaviour {
//	public PlayerUseScript pigScript; 
	public GameObject[] childScenes = new GameObject[8];
	public int DSL_test_counter;
	public int sceneIndex;

	// Use this for initialization
	void Start () {
		DSL_test_counter = 0;
		sceneIndex = 0;
		Switch ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Player" ) {
			Debug.Log ("Triggered by "+col.gameObject.name+" "+col.gameObject.tag);
			DSL_test_counter += 1;
			Switch();
		} else {
		}
	}

	void Switch(){
		Debug.Log ("switch now");
		sceneIndex = (DSL_test_counter%8);
		foreach (var child in childScenes) {
			child.SetActive (false);
		}
		childScenes [sceneIndex].SetActive (true);
	}
		
}
