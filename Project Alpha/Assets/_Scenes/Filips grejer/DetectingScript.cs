using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectingScript : MonoBehaviour {

    public Vector3 EnterPosition;
    AreaListCheckScript areaListCheckScript;
    public List<ObjectSpawning> objectSpawning = new List<ObjectSpawning>();
    
    float spawnTimer;
    bool ithappendBoi;
    


    public List<GameObject> objects;

    void Start ()
    {
        //objectSpawning = GameObject.FindGameObjectWithTag("AreaSpawn").GetComponent<ObjectSpawning>();
        areaListCheckScript = GameObject.Find("MainCamera").GetComponent<AreaListCheckScript>();
        objects = new List<GameObject>();
    }
	
	void Update ()
    {


        
        if(ithappendBoi == true)
        {

            spawnTimer += Time.deltaTime;
            
        }

        if(spawnTimer > 20 && objects.Count < 1)
        {
            spawnTimer = 0;
            ithappendBoi = false;
        }
        
    }


    void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "Player" && spawnTimer == 0)
        {
            ithappendBoi = true;

            EnterPosition = other.transform.position;
            
            

            for (int i = 0; i < objectSpawning.Count; i++)
            {
                objectSpawning[i].SpawnEnemy();
            }
        }
        else
        {
            
        }

        if (other.gameObject.tag == "Enemy" && other.isTrigger == false)
        {
           

            areaListCheckScript.CheckIfEnemyHasMovedBetweenZones(other.gameObject);

            objects.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (other.gameObject == objects[i])
            {
                objects.Remove(objects[i]);
            }
        }
    }
}
