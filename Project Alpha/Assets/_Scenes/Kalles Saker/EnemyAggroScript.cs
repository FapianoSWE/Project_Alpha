using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggroScript : MonoBehaviour {

    public float lifeLength;
    float time;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if(lifeLength <= time)
        {
            Destroy(gameObject);
        }
        transform.localScale *= 1.07f;
	}

    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<EnemyAIScript>())
        {
            if(!other.GetComponent<EnemyAIScript>().isAggrod)
            {
                other.GetComponent<EnemyAIScript>().isAggrod = true;
                other.GetComponent<EnemyAIScript>().target = GameObject.Find("Player").transform;
                other.GetComponent<EnemyAIScript>().aggroTime = other.GetComponent<EnemyAIScript>().aggroTimeMax;
            }
        }
    }
}
