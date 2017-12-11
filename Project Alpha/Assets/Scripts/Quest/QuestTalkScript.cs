using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTalkScript : MonoBehaviour
{
    public GameObject QuestManager,
       DialogueCanvas,
       QuestMarker;
    public QuestManagerScript.Quest NPCQuest;

    public int QuestId;

    public string dialogue,
        questDialogue,
        completedDialogue,
        preDialogue;
    // Use this for initialization
    GameObject player;
    // Use this for initialization
    void Start()
    {
        QuestManager = GameObject.Find("Quest Manager");
        NPCQuest = QuestManager.GetComponent<QuestManagerScript>().QuestList[QuestId];
        dialogue = preDialogue;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player"))
            if (GameObject.Find("Player").GetComponent<PlayerQuestScript>() && QuestMarker.activeInHierarchy)
            {
                foreach (QuestManagerScript.Quest q in GameObject.Find("Player").GetComponent<PlayerQuestScript>().TakenQuests)
                {
                    if (q.questID == NPCQuest.questID)
                    {
                        QuestMarker.SetActive(true);
                        break;
                    }
                }

                foreach (QuestManagerScript.Quest q in GameObject.Find("Player").GetComponent<PlayerQuestScript>().CompletedQuests)
                {
                    if (q.questID == NPCQuest.questID)
                    {
                        QuestMarker.SetActive(false);
                        break;
                    }
                }
            }
    }

    public void OnPress(GameObject g)
    {
        DialogueCanvas.SetActive(true);
        player = g;
        if (player.GetComponent<PlayerQuestScript>())
        {
            QuestManagerScript.Quest quest = null;
            bool foundQuest = false,
                questCompleted = false;
            foreach (QuestManagerScript.Quest q in player.GetComponent<PlayerQuestScript>().TakenQuests)
            {
                if (q.questID == NPCQuest.questID)
                {
                    foundQuest = true;
                    quest = q;
                }
            }

            if (!foundQuest)
            {
                foreach (QuestManagerScript.Quest q in player.GetComponent<PlayerQuestScript>().CompletedQuests)
                {
                    if (q.questID == NPCQuest.questID)
                    {
                        questCompleted = true;
                        foundQuest = true;
                        quest = q;
                    }
                }
            }

            if (foundQuest)
            {

                if (quest.questID == NPCQuest.questID)
                {
                    if (quest.goalCompleted && !quest.isFinished && !questCompleted)
                    {

                    }
                    else if (quest.goalCompleted && quest.isFinished)
                    {
                        print("You have already completed this Quest!");
                        dialogue = completedDialogue;
                    }
                    else if (quest.ReturnToGiver == false)
                    {
                        print("Quest Complete!");
                        player.GetComponent<CharacterStatsScript>().GainXP(quest.xpReward);
                        player.GetComponent<CharacterStatsScript>().gold += quest.goldReward;
                        quest.goalCompleted = true;
                        quest.isFinished = true;
                        player.GetComponent<PlayerQuestScript>().QuestTexts[player.GetComponent<PlayerQuestScript>().TakenQuests.IndexOf(quest)].GetComponent<Text>().text = "";
                        player.GetComponent<PlayerQuestScript>().TakenQuests.Remove(quest);
                        player.GetComponent<PlayerQuestScript>().CompletedQuests.Add(quest);

                        dialogue = questDialogue;
                    }
                    else
                    {
                        quest.goalCompleted = true;
                        dialogue = questDialogue;
                    }
                }



            }
        }
    }

    public void OnExitPress()
    {
        DialogueCanvas.SetActive(false);
    }
}

