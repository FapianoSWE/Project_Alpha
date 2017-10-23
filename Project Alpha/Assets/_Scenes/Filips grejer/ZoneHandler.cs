using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneHandler : MonoBehaviour {
    
    public GameObject enemyPrefab;

    public Vector3 center;
    public Vector3 size;

    void Start ()
    {

    }
	
	void Update ()
    {
        if(Input.GetKey(KeyCode.Q))
        {
            //SpawnEnemy();
        }
	}

    public void SpawnEnemy()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), 0, Random.Range(-size.z / 2, size.z / 2));

        Quaternion rot = new Quaternion(0,Random.Range(0,359),0,0);

        Instantiate(enemyPrefab, pos, rot);

    }

    

    
}
