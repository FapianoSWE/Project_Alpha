using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darkball : MonoBehaviour {

    float lifetime;
    public float lifetimeLength;
    Rigidbody rb;

    void Start () {
        rb = GetComponent<Rigidbody>();
	}
	


	void Update () {

        lifetime += Time.deltaTime;

        if (lifetime >= lifetimeLength)
        {
            Destroy(gameObject);
        }

        rb.AddForce(Vector3.up * 50);

    }
}
