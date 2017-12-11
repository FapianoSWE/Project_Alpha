using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerQuestScript : MonoBehaviour {
    public GameObject QuestCanvas,
        QuestText,
        ItemManager;
    public List<QuestManagerScript.Quest> TakenQuests = new List<QuestManagerScript.Quest>();
    public List<QuestManagerScript.Quest> CompletedQuests = new List<QuestManagerScript.Quest>();
    public List<GameObject> QuestTexts = new List<GameObject>();
	// Use this for initialization
	void Start () {
        ItemManager = GameObject.Find("Item Manager");
        QuestCanvas = GameObject.Find("Quest Log");
        QuestText = QuestCanvas.transform.GetChild(0).transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
            
             
            foreach (QuestManagerScript.Quest q in TakenQuests)
            {
                if (q.goalCompleted == false)
                {
                    if (q.questType == QuestManagerScript.Quest.QuestType.item)
                    {

                        for (int i = 0; i < GetComponent<CharacterInventoryScript>().InventoryStorage.Length; i++)
                        {
                        if (GetComponent<CharacterInventoryScript>().InventoryStorage[i].itemId == q.itemID)
                            {
                                q.currentAmount = GetComponent<CharacterInventoryScript>().InventoryItemAmount[i];
                                if(q.currentAmount >= q.Amount)
                                {
                                q.goalCompleted = true;
                                }
                                break;
                            }
                        }


                    }
                    else if (q.questType == QuestManagerScript.Quest.QuestType.kill)
                    {
                    if (q.currentAmount >= q.Amount)
                    {
                        q.goalCompleted = true;
                    }
                }

                }
            
        }
        
        for (int i = 0; i < TakenQuests.Count; i++)
        {
            if (i >= 6)
                break;
            
                if (TakenQuests[i].questType == QuestManagerScript.Quest.QuestType.item)
                {
                    if (TakenQuests[i].goalCompleted)
                        {
                        if (QuestTexts.Count >= TakenQuests.Count)
                        {                           
                            QuestTexts[i].GetComponent<Text>().text = (TakenQuests[i].Amount + "/" + TakenQuests[i].Amount + " "
                                + ItemManager.GetComponent<ItemManagerScript>().InventoryItemList[TakenQuests[i].itemID].ItemNames + " aquired.");
                            QuestTexts[i].GetComponent<Text>().color = Color.green;
                            QuestTexts[i].transform.localPosition = new Vector3(0, (-35* (i)) - 10);
                    }
                        else
                        {
                        print("Adds new Text");
                        print(i);
                        GameObject temp = Instantiate(QuestText, transform.Find("Quest Log").transform.GetChild(0));
                        temp.transform.localPosition = new Vector3(0, (-35* (i)) - 10);
                        QuestTexts.Add(temp);
                        QuestTexts[i].GetComponent<Text>().text = (TakenQuests[i].Amount + "/" + TakenQuests[i].Amount + " "
                                + ItemManager.GetComponent<ItemManagerScript>().InventoryItemList[TakenQuests[i].itemID].ItemNames + " aquired.");
                            QuestTexts[i].GetComponent<Text>().color = Color.green;
                        }
                    }
                    else if(!TakenQuests[i].isFinished)
                    {
                        if(QuestTexts.Count >= TakenQuests.Count)
                        {
                            QuestTexts[i].GetComponent<Text>().text = (TakenQuests[i].currentAmount + "/" + TakenQuests[i].Amount + " "
                                + ItemManager.GetComponent<ItemManagerScript>().InventoryItemList[TakenQuests[i].itemID].ItemNames + " aquired.");
                            QuestTexts[i].GetComponent<Text>().color = Color.black;
                        QuestTexts[i].transform.localPosition = new Vector3(0, (-35* (i)) - 10);
                    }
                        else
                        {
                            print("Adds new Text");
                        print(i);
                        GameObject temp = Instantiate(QuestText, GameObject.Find("Quest Log").transform);
                            temp.transform.localPosition = new Vector3(0, (-35* (i)) - 10);
                            QuestTexts.Add(temp);
                            QuestTexts[i].GetComponent<Text>().text = (TakenQuests[i].currentAmount + "/" + TakenQuests[i].Amount + " "
                                + ItemManager.GetComponent<ItemManagerScript>().InventoryItemList[TakenQuests[i].itemID].ItemNames + " aquired.");
                            QuestTexts[i].GetComponent<Text>().color = Color.black;
                        }
                        
                    }

                }
                else if(TakenQuests[i].questType == QuestManagerScript.Quest.QuestType.kill)
                {
                    if (TakenQuests[i].goalCompleted)
                    {
                        if (QuestTexts.Count >= TakenQuests.Count)
                        {
                            QuestTexts[i].GetComponent<Text>().text = (TakenQuests[i].Amount + "/" + TakenQuests[i].Amount + " enemies killed.");
                            QuestTexts[i].GetComponent<Text>().color = Color.green;
                        QuestTexts[i].transform.localPosition = new Vector3(0, (-35* (i)) - 10);
                    }
                        else
                        {
                            print("Adds new Text");
                        print(i);
                        GameObject temp = Instantiate(QuestText, QuestCanvas.transform.GetChild(0));
                        temp.transform.localPosition = new Vector3(0, (-35* (i)) - 10);
                        QuestTexts.Add(temp);
                        QuestTexts[i].GetComponent<Text>().text = (TakenQuests[i].Amount + "/" + TakenQuests[i].Amount + " enemies killed.");
                            QuestTexts[i].GetComponent<Text>().color = Color.green;
                        }
                    }
                    else
                    {
                    
                        if (QuestTexts.Count >= TakenQuests.Count)
                        {
                            QuestTexts[i].GetComponent<Text>().text = (TakenQuests[i].currentAmount + "/" + TakenQuests[i].Amount + " enemies killed.");
                            QuestTexts[i].GetComponent<Text>().color = Color.black;
                        QuestTexts[i].transform.localPosition = new Vector3(0, (-35* (i)) - 10);
                    }
                        else
                        {
                            print("Adds new Text");
                            print(i);
                            GameObject temp = Instantiate(QuestText, QuestCanvas.transform.GetChild(0));
                            temp.transform.localPosition = new Vector3(0, (-35* (i)) - 10);
                        QuestTexts.Add(temp);
                            QuestTexts[i].GetComponent<Text>().text = (TakenQuests[i].currentAmount + "/" + TakenQuests[i].Amount + " enemies killed.");
                            QuestTexts[i].GetComponent<Text>().color = Color.black;
                        }                  

                    }
                }
                else if(TakenQuests[i].questType == QuestManagerScript.Quest.QuestType.talk)
                {
                if (QuestTexts.Count >= TakenQuests.Count)
                {
                    if(TakenQuests[i].ReturnToGiver == false)
                    {
                        QuestTexts[i].GetComponent<Text>().text = ("Talk to "+TakenQuests[i].Target.gameObject.name);
                        QuestTexts[i].GetComponent<Text>().color = Color.black;
                        QuestTexts[i].transform.localPosition = new Vector3(0, (-35* (i)) - 10);
                    }
                    else
                    {
                        QuestTexts[i].GetComponent<Text>().text = ("Talk to " + TakenQuests[i].Target.gameObject.name + ", and then return to "+ TakenQuests[i].questGiver.gameObject.name);
                        QuestTexts[i].GetComponent<Text>().color = Color.black;
                        QuestTexts[i].transform.localPosition = new Vector3(0, (-35* (i)) - 10);
                    }

                }
                else
                {
                    if (TakenQuests[i].ReturnToGiver == false)
                    {
                        print("Adds new Text");
                        print(i);
                        GameObject temp = Instantiate(QuestText, GameObject.Find("Quest Log").transform.GetChild(0));
                        temp.transform.localPosition = new Vector3(0, (-35* (i)) - 10);
                        QuestTexts.Add(temp);
                        QuestTexts[i].GetComponent<Text>().text = ("Talk to " + TakenQuests[i].Target.gameObject.name);
                        QuestTexts[i].GetComponent<Text>().color = Color.black;
                    }
                    else
                    {
                        print("Adds new Text");
                        print(i);
                        GameObject temp = Instantiate(QuestText, GameObject.Find("Quest Log").transform.GetChild(0));
                        temp.transform.localPosition = new Vector3(0, (-35* (i)) - 10);
                        QuestTexts.Add(temp);
                        QuestTexts[i].GetComponent<Text>().text = ("Talk to " + TakenQuests[i].Target.gameObject.name + ", and then return to " + TakenQuests[i].questGiver.gameObject.name);
                        QuestTexts[i].GetComponent<Text>().color = Color.black;
                    }   
                }

                for (int j = 0; j < CompletedQuests.Count; j++)
                {
                    if (TakenQuests[i].questID == CompletedQuests[j].questID && j != i)
                    {
                        print("ASDIOasnfhoia");

                        TakenQuests.RemoveAt(i);

                        i--;
                        break;
                    }
                }
            }            
            else
            {
 
            }
        }

        if(QuestTexts.Count > TakenQuests.Count && TakenQuests.Count >= 1)
        {
            for (int i = TakenQuests.Count; i < QuestTexts.Count; i++)
            {
                Destroy(QuestTexts[i]);
                QuestTexts.RemoveAt(i);
                i--;
            }
        }

	}


}
