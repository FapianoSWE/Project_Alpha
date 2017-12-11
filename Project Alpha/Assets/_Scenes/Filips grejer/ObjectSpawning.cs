using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawning : MonoBehaviour {

    ZoneHandler zoneHandler;
    DetectingScript detectingScript;
    //public List<GameObject> objects;
    bool firstEnter;
    string tempName;
    //Vector3 EnterPosition;

    public GameObject enemyPrefab;

    Vector3 center;
    Vector3 size;
    Vector3 scale;


    void Start ()
    {
        //zoneHandler = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ZoneHandler>();
        detectingScript = transform.parent.GetComponent<DetectingScript>();
        //objects = new List<GameObject>();
        
        center = gameObject.transform.position;

        size.x = gameObject.transform.parent.localScale.x * gameObject.transform.localScale.x; //(gameObject.transform.position.x * 2) - (gameObject.transform.position.x / 2);
        size.z = gameObject.transform.parent.localScale.z * gameObject.transform.localScale.z; //(gameObject.transform.position.z * 2) - (gameObject.transform.position.z / 2);
        scale.z = 1;
        scale.x = 1;




    }
	
	void Update ()
    {
        
      
    }
    
        

        /*for (int i = 0; i < totalObjects.Count; i++)
        {
            if ("Zone " + gameObject.name + " " + other.gameObject.name == totalObjects[i])
            {
                
            }
        }*/


    public void SpawnEnemy()
    {
        //float allowedValueX;
        //float allowedValueZ;
        float allowedValueY = 0;
        float distance;
        int count = 0;

        for (int i = 0; i < 4; i++)
        {
            Vector3 pos = center + new Vector3(Random.Range((-size.x * scale.x) / 2, (size.x * scale.x) / 2), allowedValueY, Random.Range((-size.z * scale.z) / 2, (size.z * scale.z) / 2));
            

            RaycastHit hit;
            Ray ray = new Ray(pos + (new Vector3(0, 450, 0)), transform.up * -1);
            if (Physics.Raycast(ray, out hit, 600))
            {
                print(hit.transform.name);
                distance = Vector3.Distance(detectingScript.EnterPosition, pos);

                Quaternion rot = new Quaternion(0, Random.Range(0, 359), 0, 0);

                print(hit);
                print(hit.transform);
                print(hit.transform.tag);
                if (distance > 5 && hit.transform.tag == "Terrain" && hit.transform.tag != "Enemy")
                {

                    Instantiate(enemyPrefab, new Vector3(pos.x, hit.point.y + 1, pos.z), rot, GameObject.Find("Enemies").GetComponent<Transform>());

                }
            }            
            else
            {
                count++;
                i--;

                if(count > 1000)
                {
                    break;
                }
            }
        }

        //allowedValueX = RandomExcept(((int)-size.x * (int)scale.x) / 2, ((int)-size.x * (int)scale.x) / 2, (int)detectingScript.EnterPosition.x);

        //allowedValueZ = RandomExcept(((int)-size.z * (int)scale.z) / 2, ((int)-size.z * (int)scale.z) / 2, (int)detectingScript.EnterPosition.z);

        
        
        

    }
    public int RandomExcept(int fromNr, int exclusiveToNr, int exceptNr)
    {
        int randomNr = exceptNr;

         

        while (randomNr == exceptNr)
        {
            randomNr = Random.Range(fromNr, exclusiveToNr);
        }

        return randomNr;
    }
}

