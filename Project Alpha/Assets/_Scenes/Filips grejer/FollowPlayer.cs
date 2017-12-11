using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    Vector3 PlayerPosition;
    void Start ()
    {

    }
	
	void Update ()
    {
        PlayerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().gameObject.transform.position;

        gameObject.transform.position = new Vector3(PlayerPosition.x, gameObject.transform.position.y, PlayerPosition.z);
        

    }
}
