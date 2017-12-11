using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isPaused = false;

	void Start ()
    {
		
	}

	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
	}
}
