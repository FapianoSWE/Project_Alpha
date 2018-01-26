using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour {

    public GameObject enemyObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (enemyObject != null)
        {
            transform.Find("EnemyHealthBarFG").transform.localScale = new Vector3(enemyObject.GetComponent<EnemyAIScript>().health / enemyObject.GetComponent<EnemyAIScript>().maxHealth,1,1) ;
        }
	}
}
