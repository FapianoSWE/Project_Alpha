using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPrefabScript : MonoBehaviour {

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

    public void Initialize(string Text, Color Color)
    {
        GetComponent<TextMesh>().text = Text;
        GetComponent<TextMesh>().color = Color;
    }
}
