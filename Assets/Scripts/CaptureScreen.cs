using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureScreen : MonoBehaviour {

    [Range(1,10)]
    public int SuperSize = 3;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.C) && Input.GetKeyDown(KeyCode.D)){
            
            ScreenCapture.CaptureScreenshot("screenshot", SuperSize);
            Debug.Log("Screen Captured");
        }
	}
}
