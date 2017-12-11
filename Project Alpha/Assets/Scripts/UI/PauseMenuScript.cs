using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    public GameManager gameManagerScript;
    public GameObject pauseMenu;

	void Start ()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

        pauseMenu.SetActive(false);
	}
	
	void Update ()
    {
        if (gameManagerScript.isPaused)
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(false);
        }
	}

    public void ResumeGame()
    {
        gameManagerScript.isPaused = false;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
