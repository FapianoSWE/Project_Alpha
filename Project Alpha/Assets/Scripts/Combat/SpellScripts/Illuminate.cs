using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Illuminate : MonoBehaviour {

    
	void Start () {
   
        transform.SetParent(GameObject.Find("Player").transform);
       
	}
	


	public void SpellUpdate () {
        transform.localPosition = new Vector3(0, 6,1);
        transform.localRotation = Quaternion.Euler(90, 0, 0);
    }
}
