using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleScript : MonoBehaviour
{

    InputField consoleInputField;

    Text logText;

    bool isTyping = false;
    GameObject console;

    int commandcount = 5;


	void Start ()
    {
        console = GameObject.Find("ConsoleViewContainer");
        consoleInputField = GameObject.Find("ConsoleInputField").GetComponent<InputField>();
        logText = GameObject.Find("LogText").GetComponent<Text>();
        logText.text = string.Empty;

        console.SetActive(false);
    }
	


	void Update ()
    {
		
        if(Input.GetKeyDown(KeyCode.Return) && !isTyping)
        {
            Debug.Log("pressed enter");
            console.SetActive(true);
            consoleInputField.ActivateInputField();
            isTyping = true;
        }

        if(Input.GetKeyDown(KeyCode.Return) && isTyping)
        {
            if(logText.text == string.Empty)
            {
                logText.text = logText.text + consoleInputField.text;
                commandcount -= 1;
            }
            else if(logText.text != string.Empty)
            {
                string currentLog = logText.text;
                logText.text = consoleInputField.text + "\n" + currentLog;
                commandcount -= 1;
            }
            isTyping = false;
        }
        if(commandcount <= 0)
        {
            string[] splitLog = logText.text.Split('\n');
            logText.text = logText.text;
            commandcount = 1;
        }

        if(consoleInputField.isFocused)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().canMove = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().canJump = false;
        }

    }
}
