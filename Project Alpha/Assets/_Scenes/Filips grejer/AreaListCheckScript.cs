using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaListCheckScript : MonoBehaviour {

    public List<GameObject> zoneList = new List<GameObject>();




	void Start ()
    {
		
	}
	
	void Update ()
    {
        
	}
    public void CheckIfEnemyHasMovedBetweenZones(GameObject o)
    {
        for (int i = 0; i < zoneList.Count; i++)
        {
            for (int j = 0; j < zoneList[i].GetComponent<DetectingScript>().objects.Count; j++)
            {
                if (zoneList[i].GetComponent<DetectingScript>().objects[j] == o)
                {
                    zoneList[i].GetComponent<DetectingScript>().objects.RemoveAt(j);
                    break;
                }

            }
        }
    }
}
