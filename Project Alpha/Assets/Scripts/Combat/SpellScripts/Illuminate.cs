using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Illuminate : MonoBehaviour {

    
	void Start () {
        transform.SetParent(GameObject.Find("Player").transform);
       
	}
	


	void Update () {


        transform.localPosition = new Vector3(0.5f, 0.5f, 0.5f);
    }
}
