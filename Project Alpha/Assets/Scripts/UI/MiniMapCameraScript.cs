using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraScript : MonoBehaviour {

    public GameObject[] trackedObjects;
    List<GameObject> radarObjects;
    public GameObject radarEnemyPrefab;
    List<GameObject> borderObjects;
    public float switchDistance;
    public Transform helpTransform;
    


    void Start()
    {
        CreateRadarObjects();
    }

    void Update()
    {
        for (int i = 0; i < radarObjects.Count; i++)
        {
            if (Vector3.Distance(radarObjects[i].transform.position, transform.position) > switchDistance)
            {
                helpTransform.LookAt(radarObjects[i].transform);
                borderObjects[i].transform.position = transform.position + switchDistance * helpTransform.forward;
                borderObjects[i].layer = LayerMask.NameToLayer("MiniMapLayer");
                radarObjects[i].layer = LayerMask.NameToLayer("InvisibleLayer");
            }
            else
            {
                borderObjects[i].layer = LayerMask.NameToLayer("InvisibleLayer");
                radarObjects[i].layer = LayerMask.NameToLayer("MiniMapLayer");
            }
        }
    }
    void CreateRadarObjects()
    {
        radarObjects = new List<GameObject>();
        borderObjects = new List<GameObject>();

        foreach (GameObject a in trackedObjects)
        {
            GameObject b = Instantiate(radarEnemyPrefab, a.transform.position, Quaternion.identity) as GameObject;
            radarObjects.Add(b);
            GameObject c = Instantiate(radarEnemyPrefab, a.transform.position, Quaternion.identity) as GameObject;
            borderObjects.Add(c);
        }
    }
}
