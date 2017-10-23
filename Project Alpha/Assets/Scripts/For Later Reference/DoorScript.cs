using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour {
    public string sceneToConnect,
        spawnLocation;
    GameObject temp;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "me")
        {
            other.GetComponent<SaveAndLoadScript>().Save();
            GameObject.Find("VarStorage").GetComponent<VarStorageScript>().spawnpointname = spawnLocation;
            Destroy(other.gameObject);
            SceneManager.LoadScene(sceneToConnect);
        }
    }

}
