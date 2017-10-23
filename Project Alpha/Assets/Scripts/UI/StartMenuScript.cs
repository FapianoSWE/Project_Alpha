using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    public Button 
        startButton,
        exitButton;

	void Start ()
    {
        startButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(ExitGame);
	}
	
	void Update ()
    {
		
	}

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
