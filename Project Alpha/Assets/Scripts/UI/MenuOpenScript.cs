using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOpenScript : MonoBehaviour
{
    public bool MenuOpen;

    StatUIScript statUI;
    GameManager gameManagerScript;
    PlayerController playercontrollerScript;

    bool[] anyUIActive = new bool[4];

	void Start ()
    {
        MenuOpen = false;

        statUI = GetComponentInChildren<StatUIScript>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        playercontrollerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        DontDestroyOnLoad(gameObject);
	}
	//TODO: Make Esc button remove all open menus
	void Update ()
    {
        anyUIActive[0] = statUI.statsActive;
        anyUIActive[1] = gameManagerScript.isPaused;
        anyUIActive[2] = playercontrollerScript.itemCanvasOpen;
        anyUIActive[3] = playercontrollerScript.inDialogue;

        MenuOpen = false;
        
        for (int i = 0; i < anyUIActive.Length; i++)
        {
            if (anyUIActive[i])
            {
                Time.timeScale = 0;
                MenuOpen = true;
            }
        }
        if (!MenuOpen)
        {
            Time.timeScale = 1;
        }
	}
}
