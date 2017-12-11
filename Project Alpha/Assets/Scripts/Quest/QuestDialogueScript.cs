using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestDialogueScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.transform.parent.GetComponent<QuestGiverScript>())
            GetComponentInChildren<Text>().text = transform.parent.transform.parent.GetComponent<QuestGiverScript>().dialogue;
        if (transform.parent.transform.parent.GetComponent<QuestTalkScript>())
            GetComponentInChildren<Text>().text = transform.parent.transform.parent.GetComponent<QuestTalkScript>().dialogue;

        GameObject.Find("Player").GetComponent<PlayerController>().inDialogue = true;
    }

    public void Exit()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().inDialogue = false;
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
