using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class QuestManagerScript : MonoBehaviour {
    public List<Quest> QuestList = new List<Quest>();
    public GameObject ItemManager;
    public GameObject enemyPrefab;

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
        public GameObject enemy;
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
        public Quest(int id,QuestType _questType,int amount, GameObject enemyType)
        {
            
            questID = id;
            questType = _questType;
            Amount = amount;
            isFinished = false;
            goalCompleted = false;
            enemy = enemyType;
            
        }

        //Fetch quest
        public Quest(int id, QuestType _questType, int itemid,int amount)
        {
            questID = id;
            questType = _questType;
            Amount = amount;
            itemID = itemid;
            isFinished = false;
            goalCompleted = false;
        }

        //Talk quest
        public Quest(int id, QuestType _questType, GameObject target, bool returnToGiver)
        {
            questID = id;
            questType = _questType;
            Target = target;
            ReturnToGiver = returnToGiver;
            isFinished = false;
            goalCompleted = false;
        }
    }
	// Use this for initialization
    // All quests that exist
	void Start () {
        DontDestroyOnLoad(gameObject);
        QuestList.Add(new Quest(0,Quest.QuestType.kill,10, enemyPrefab));
        QuestList.Add(new Quest(1,Quest.QuestType.item, 0,5));
        QuestList.Add(new Quest(2, Quest.QuestType.talk, null,false));
        QuestList.Add(new Quest(3, Quest.QuestType.talk, null, true));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
