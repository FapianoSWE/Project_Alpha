using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRemovalScript : MonoBehaviour {
    float time;
	// Use this for initialization
	void Start () {
        time = 10;
	}
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;

        if (time <= 0)
            Destroy(gameObject);
	}
}
