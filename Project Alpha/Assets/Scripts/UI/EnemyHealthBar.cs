using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour {

    public GameObject enemyObject;

    public float lastUpdated;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (enemyObject != null)
        {
            transform.Find("EnemyHealthBarFG").gameObject.SetActive(true);
            transform.Find("EnemyHealthBarBG").gameObject.SetActive(true);
            transform.Find("EnemyHealthBarName").gameObject.SetActive(true);
            transform.Find("EnemyHealthBarFG").transform.localScale = new Vector3(enemyObject.GetComponent<EnemyAIScript>().health / enemyObject.GetComponent<EnemyAIScript>().maxHealth,1,1);
            transform.Find("EnemyHealthBarName").GetComponent<Text>().text = enemyObject.GetComponent<EnemyAIScript>().Name;
            lastUpdated += Time.deltaTime;
            if(lastUpdated >= 5)
            {
                enemyObject = null;
            }
        }
        else
        {
            transform.Find("EnemyHealthBarFG").gameObject.SetActive(false);
            transform.Find("EnemyHealthBarBG").gameObject.SetActive(false);
            transform.Find("EnemyHealthBarName").gameObject.SetActive(false);
        }
	}
}
