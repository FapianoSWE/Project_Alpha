using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritPrefabScript : MonoBehaviour {

    float timer = 1;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Destroy(gameObject);
        }
        GetComponent<Rigidbody>().AddForce(transform.up);
	}
}
