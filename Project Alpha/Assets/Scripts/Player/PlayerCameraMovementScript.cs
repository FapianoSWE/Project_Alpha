using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraMovementScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.position += transform.forward * Input.mouseScrollDelta.y;

        if (transform.localPosition.z >= -3)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -3);
        }
        if (transform.localPosition.z <= -10)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -10);
        }

        if(transform.localPosition.y != 3.3f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 3.3f , transform.localPosition.z);
        }

        if (transform.localPosition.x != -0.18f)
        {
            transform.localPosition = new Vector3(-0.18f, transform.localPosition.y, transform.localPosition.z);
        }
    }
}
