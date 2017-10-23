//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EnemySpawner : MonoBehaviour
//{
    
//    public float spawnClock;

//    public int range;
    
//	void Start ()
//    {

//	}
	

//	void Update ()
//    {
//        spawnClock -= Time.deltaTime;

//        if(spawnClock <= 0 && GameObject.Find("Enemies").transform.childCount < 50)
//        {
//            GameObject temp = Instantiate("Enemies/Amazing_Enemy", new Vector3(transform.position.x + Random.Range(range*-1, range),
//                transform.position.y, transform.position.z + Random.Range(range * -1, range)), Quaternion.identity, 0);
//            temp.transform.SetParent(GameObject.Find("Enemies").GetComponent<Transform>());
//            spawnClock = 4;
//        }
//	}
//}
