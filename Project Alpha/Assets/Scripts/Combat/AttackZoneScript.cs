using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZoneScript : MonoBehaviour {

    public float timer = 1;

    public string attacker;

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = 1;
            gameObject.SetActive(false);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyAIScript>() && !other.isTrigger)
        {
            other.gameObject.GetComponent<EnemyAIScript>().OnMeleeHit(gameObject);
        }
    }
}
