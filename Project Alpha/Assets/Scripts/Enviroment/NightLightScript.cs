using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightLightScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		/*if(System.DateTime.Now.Hour >= 17)
        {
            GetComponent<Light>().intensity = 0.2f;
        }
        else if(System.DateTime.Now.Hour <= 6)
        {
            GetComponent<Light>().intensity = 0.2f;
        }
        else
        {
            GetComponent<Light>().intensity = 0;
        }*/
	}
}
