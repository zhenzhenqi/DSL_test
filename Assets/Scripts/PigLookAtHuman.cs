using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PigLookAtHuman : MonoBehaviour {


    public float triggerDistance = 1.66f;
    public string tvmanMaskName = "tvman";
    [Range(0f, 0.2f)]
    public float distorationIntensity = 0.08f;


    Transform influencer;
    Animator animator;
    SphereCollider col;
    LayerMask tvmanMask;
    SkinnedMeshRenderer mesh;

	void Start () {
        animator = GetComponent<Animator>();

        col = GetComponent<SphereCollider>();
        col.isTrigger = true;
        col.radius = triggerDistance;

        tvmanMask = LayerMask.GetMask(tvmanMaskName);
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if(influencer!=null){
            float distance = Vector3.Distance(transform.position, influencer.position);
            mesh.material.SetFloat("_DistorationIntensity", Mathf.Lerp(distorationIntensity, 0f, distance/triggerDistance));
        }else{
            mesh.material.SetFloat("_DistorationIntensity", 0f);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(1 << other.gameObject.layer == tvmanMask){
            influencer = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (1 << other.gameObject.layer == tvmanMask)
        {
            if(other.transform == influencer){
                influencer = null;
            }
        }
    }

    void CancelDistortion(){
        mesh.material.SetFloat("_DistorationIntensity", 0f);
    }

}
