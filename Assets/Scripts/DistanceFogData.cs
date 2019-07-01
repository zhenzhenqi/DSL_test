using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DistanceFogData : MonoBehaviour {

    public Color distanceFogColor;
    public Shader targetShader;
    [Range(0.0f, 20.0f)]
    public float fogRange;
    [Range(-30.0f, 100.0f)]
    public float fogStart;
 
    public List<Renderer> releventRenderers;

	// Use this for initialization
	void Start () {
        releventRenderers = new List<Renderer>();
		GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach(GameObject go in allObjects){
            if (go.GetComponent<Renderer>() != null)
            {
                if (go.GetComponent<Renderer>().material.shader == targetShader)
                {
                    releventRenderers.Add(go.GetComponent<Renderer>());
                }
            }

        }
	}
	
	// Update is called once per frame
	void Update () {
        foreach (Renderer r in releventRenderers){
            r.material.SetVector("_CameraPos", Camera.main.transform.position);
            r.material.SetVector("_DistanceFogColor", distanceFogColor);
            r.material.SetFloat("_FogStart", fogStart);
            r.material.SetFloat("_FogRange", fogRange);
        }
	}
}
