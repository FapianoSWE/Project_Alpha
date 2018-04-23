using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class QuestManagerScript : MonoBehaviour {
    public List<Quest> QuestList = new List<Quest>();
    public GameObject ItemManager;

    [Serializable]
    public class Quest
    {
        
        public enum QuestType
        {
            kill,
            talk,
            item
        }
        public QuestType questType;
        public int enemy;
        public GameObject Target,
            questGiver;
        public int questID,
            itemID,
            Amount,
            currentAmount,
            goldReward,
            xpReward,
            itemIDReward;
        public bool isFinished,
            goalCompleted,
            ReturnToGiver;
        public string goalDesc;

        //Kill quest
        public Quest(int id,QuestType _questType,int amount, int enemyId,bool bossQuest, int XpReward, int GoldReward)
        {
            
            questID = id;
            questType = _questType;
            Amount = amount;
            isFinished = false;
            goalCompleted = false;
            enemy = enemyId;
            xpReward = XpReward;
            goldReward = GoldReward;
        }

        //Fetch quest
        public Quest(int id, QuestType _questType, int itemid,int amount, int XpReward, int GoldReward)
        {
            questID = id;
            questType = _questType;
            Amount = amount;
            itemID = itemid;
            isFinished = false;
            goalCompleted = false;
            xpReward = XpReward;
            goldReward = GoldReward;
        }

        //Talk quest
        public Quest(int id, QuestType _questType, GameObject target, bool returnToGiver, int XpReward, int GoldReward)
        {
            questID = id;
            questType = _questType;
            Target = target;
            ReturnToGiver = returnToGiver;
            isFinished = false;
            goalCompleted = false;
            xpReward = XpReward;
            goldReward = GoldReward;
        }
    }
	// Use this for initialization
    // All quests that exist
	void Start () {
        DontDestroyOnLoad(gameObject);
        ItemManager = GameObject.Find("Item Manager");
        QuestList.Add(new Quest(0,Quest.QuestType.kill,5, 0,false,200,50));
        QuestList.Add(new Quest(1,Quest.QuestType.item, 0,5,100,100));
        QuestList.Add(new Quest(2, Quest.QuestType.talk, null,false,100,50));
        QuestList.Add(new Quest(3, Quest.QuestType.talk, null, true,100,50));
        QuestList.Add(new Quest(4, Quest.QuestType.kill, 1, 2, true,500,250));
        QuestList.Add(new Quest(5, Quest.QuestType.item, 14, 12,250,200));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
