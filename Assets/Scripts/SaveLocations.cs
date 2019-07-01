using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System.IO;

public class SaveLocations : MonoBehaviour
{


    public List<Trans> incomingList;

    public string filename = "transform_data.txt";
    public string path;


    private void Start()
    {
        path = "Assets/" + filename;
    }

    [System.Serializable]
    public struct TransWrapper
    {
        public List<Trans> list;
    }

    [System.Serializable]
    public struct Trans
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
    }


    public void GetTransforms()
    {
        TransWrapper wrapper;

        wrapper.list = new List<Trans>();

        foreach (Transform child in transform)
        {
            Trans tran = new Trans();
            tran.position = child.position;
            tran.rotation = child.rotation;
            tran.scale = child.localScale;
            wrapper.list.Add(tran);

        }



        var file = File.CreateText(path);
        string towrite = JsonUtility.ToJson(wrapper);
        Debug.Log(towrite);
        file.Write(towrite);
        file.Close();

    }


    public void ApplyTransforms()
    {
        StreamReader reader = new StreamReader(path);

        string got = reader.ReadToEnd();
        reader.Close();

        TransWrapper wrapper = JsonUtility.FromJson<TransWrapper>(got);

        incomingList = wrapper.list;


        int index = 0;
        foreach (Transform child in transform)
        {

            child.position = incomingList[index].position;
            child.rotation = incomingList[index].rotation;
            child.localScale = incomingList[index].scale;
            index++;
        }



    }





}

#if UNITY_EDITOR
[CustomEditor(typeof(SaveLocations))]
public class SaveLocationsEditor : Editor
{

    public override void OnInspectorGUI()
    {
        SaveLocations myTarget = (SaveLocations)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Get Transforms"))
        {
            myTarget.GetTransforms();
        }

        if (GUILayout.Button("Apply Transforms"))
        {
            myTarget.ApplyTransforms();
        }



    }

}
#endif