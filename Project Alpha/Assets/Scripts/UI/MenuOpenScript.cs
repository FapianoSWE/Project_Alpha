using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOpenScript : MonoBehaviour
{
    public bool MenuOpen;

    StatUIScript statUI;
    GameManager gameManagerScript;

    bool[] anyUIActive = new bool[2];

	void Start ()
    {
        MenuOpen = false;

        statUI = GetComponentInChildren<StatUIScript>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	void Update ()
    {
        anyUIActive[0] = statUI.statsActive;
        anyUIActive[1] = gameManagerScript.isPaused;

        MenuOpen = false;
        for (int i = 0; i < anyUIActive.Length; i++)
        {
            if (anyUIActive[i])
            {
                MenuOpen = true;
            }
        }
	}
}
