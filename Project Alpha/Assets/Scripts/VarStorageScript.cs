using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VarStorageScript : MonoBehaviour {
    public string spawnpointname;
	// Use this for initialization
	void Start () {
        spawnpointname = "Spawn";
        DontDestroyOnLoad(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
