using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;


public class SaveAndLoadScript : MonoBehaviour
{
    public string currentScene;
    public savedata data;
    public Vector3 xyz;
    public Quaternion xyzw;
    public int playerLevel;
    public int playerXP;

    public int playerInt,
               playerVit,
               playerStr,
               playerAgi,
               playerLuck,
               playerDex,
               playerRes,
               playerAttributePoints,
               playerHealth,
               playerGold,
               playerMana;

    /*public int[] playerInventoryStorage = new int[16];
    public int[] playerInventoryItemAmount = new int[16];*/
    // Use this for initialization
    void Start()
    {  
        /*for (int i = 0; i < playerInventoryStorage.Length; i++)
        {
            playerInventoryStorage[i] = 3;
            data.inventoryStorage[i] = 3;
        }*/
    }

    [Serializable]
    public class savedata
    {
        public string currentscene;
        public SerVector3 position;
        public SerQuaternion rotation;
        public int level,
             xp,
           _int,
            vit,
            str,
            agi,
           luck,
           dex,
            res,
           ap,
            gold,
            health,
            mana;
        /*public int[] inventoryStorage = new int[16];
        public int[] inventoryItemAmount = new int[16];*/
    }
    [Serializable]
    public class SerVector3
    {
        public float x,
            y,
            z;
    }
    [Serializable]
    public class SerQuaternion
    {
        public float x,
            y,
            z,
            w;
    }
    // Update is called once per frame
    void Update()
    {

    }

    public string GetPlayerScene()
    {
        if (File.Exists(Application.dataPath + "/SaveData" + "/SaveData_" + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/SaveData" + "/SaveData_" + ".dat", FileMode.Open);
            data = (savedata)bf.Deserialize(file);
            copyLoadSceneData();
            file.Close();
            return currentScene;
        }
        else
            return "Main";
    }

    public void Save()
    {
        if(!Directory.Exists(Application.dataPath + "/SaveData"))
        {
            Directory.CreateDirectory(Application.dataPath + "/SaveData");
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/SaveData" + "/SaveData_" +".dat");
        xyz = gameObject.transform.position;
        xyzw = gameObject.transform.rotation;
        playerLevel = GetComponent<CharacterStatsScript>().currentLevel;
        playerXP = GetComponent<CharacterStatsScript>().currentXp;
        playerInt = GetComponent<CharacterStatsScript>().intelligence;
        playerVit = GetComponent<CharacterStatsScript>().vitality;
        playerStr = GetComponent<CharacterStatsScript>().strength;
        playerAgi = GetComponent<CharacterStatsScript>().agility;
        playerLuck = GetComponent<CharacterStatsScript>().luck;
        playerDex = GetComponent<CharacterStatsScript>().dexterity;
        playerRes = GetComponent<CharacterStatsScript>().resistance;
        playerAttributePoints = GetComponent<CharacterStatsScript>().attributePoints;
        playerHealth = (int)GetComponent<CharacterStatsScript>().currentHealth;
        //playerMana = GetComponent<CharacterStatsScript>().currentMana;
        playerGold = GetComponent<CharacterStatsScript>().gold;
        currentScene = SceneManager.GetActiveScene().name;
        /*for (int i = 0; i < GetComponent<CharacterInventoryScript>().InventoryStorage.Length; i++)
        {
            playerInventoryStorage[i] = GetComponent<CharacterInventoryScript>().InventoryStorage[i].itemId;
        }
        for (int i = 0; i < GetComponent<CharacterInventoryScript>().InventoryItemAmount.Length; i++)
        {
            playerInventoryItemAmount[i] = GetComponent<CharacterInventoryScript>().InventoryItemAmount[i];
        }*/
        
        CopySaveData();
        bf.Serialize(file, data);
        file.Close();
        print("Saved!");
       
    }

    public void CopySaveData()
    {
        data.position = Vector3ToSerVector3(xyz);
        data.rotation = QuaternionToSerQuaternion(xyzw);
        data.level = playerLevel;
        data.xp = playerXP;
        data.vit = playerVit;
        data._int = playerInt;
        data.str = playerStr;
        data.agi = playerAgi;
        data.luck = playerLuck;
        data.dex = playerDex;
        data.res = playerRes;
        data.ap = playerAttributePoints;
        data.health = playerHealth;
        data.mana = playerMana;
        data.gold = playerGold;
        data.currentscene = currentScene;
        /*for (int i = 0; i < GetComponent<CharacterInventoryScript>().InventoryStorage.Length; i++)
        {
            data.inventoryStorage[i] = GetComponent<CharacterInventoryScript>().InventoryStorage[i].itemId;
        }
        for (int i = 0; i < GetComponent<CharacterInventoryScript>().InventoryItemAmount.Length; i++)
        {
            data.inventoryItemAmount[i] = GetComponent<CharacterInventoryScript>().InventoryItemAmount[i];
        }*/
        
    }

    public void load()
    {
        if (File.Exists(Application.dataPath + "/SaveData" + "/SaveData_" + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/SaveData" + "/SaveData_" + ".dat", FileMode.Open);
            data = (savedata)bf.Deserialize(file);
            copyLoadData();
            file.Close();
            if(transform.position.x == GameObject.FindGameObjectWithTag("Respawn").transform.position.x && transform.position.z == GameObject.FindGameObjectWithTag("Respawn").transform.position.z)
            {
                gameObject.transform.position = xyz;
                gameObject.transform.rotation = xyzw;
            }

            GetComponent<CharacterStatsScript>().currentLevel = playerLevel;
            GetComponent<CharacterStatsScript>().currentXp = playerXP;
            GetComponent<CharacterStatsScript>().intelligence = playerInt;
            GetComponent<CharacterStatsScript>().vitality = playerVit;
            GetComponent<CharacterStatsScript>().strength = playerStr;
            GetComponent<CharacterStatsScript>().agility = playerAgi;
            GetComponent<CharacterStatsScript>().luck = playerLuck;
            GetComponent<CharacterStatsScript>().dexterity = playerDex;
            GetComponent<CharacterStatsScript>().resistance = playerRes;
            GetComponent<CharacterStatsScript>().attributePoints = playerAttributePoints;
            GetComponent<CharacterStatsScript>().currentHealth = playerHealth;
            GetComponent<CharacterStatsScript>().currentMana = playerMana;
            GetComponent<CharacterStatsScript>().gold = playerGold;
            /*for (int i = 0; i < GetComponent<CharacterInventoryScript>().InventoryStorage.Length; i++)
            {
                GetComponent<CharacterInventoryScript>().SetInvenoryItem(i,playerInventoryStorage[i]);
            }
            for (int i = 0; i < GetComponent<CharacterInventoryScript>().InventoryItemAmount.Length; i++)
            {
                GetComponent<CharacterInventoryScript>().InventoryItemAmount[i] = playerInventoryItemAmount[i];
            }*/
            
            print("Loaded!");                        
        }
    }
    public void copyLoadSceneData()
    {
        currentScene = data.currentscene;
    }
    public void copyLoadData()
    {
        xyz = SerVector3ToVector(data.position);
        xyzw = SerQuaternionToQuaternion(data.rotation);
        playerLevel = data.level;
        playerXP = data.xp;
        playerVit = data.vit;
        playerInt = data._int;
        playerStr = data.str;
        playerAgi = data.agi;
        playerLuck = data.luck;
        playerDex = data.dex;
        playerRes = data.res;
        playerAttributePoints = data.ap;
        playerHealth = data.health;
        playerMana = data.mana;
        playerGold = data.gold;
        currentScene = data.currentscene;
        /*for (int i = 0; i < GetComponent<CharacterInventoryScript>().InventoryStorage.Length; i++)
        {
            playerInventoryStorage[i] =data.inventoryStorage[i];                  
        }
        for (int i = 0; i < GetComponent<CharacterInventoryScript>().InventoryItemAmount.Length; i++)
        {
            playerInventoryItemAmount[i] = data.inventoryItemAmount[i];
        }*/
       
    }

    public SerVector3 Vector3ToSerVector3(Vector3 V3)
    {
        SerVector3 SV3 = new SerVector3();
        SV3.x = V3.x;
        SV3.y = V3.y;
        SV3.z = V3.z;
        return SV3;
    }

    public Vector3 SerVector3ToVector(SerVector3 SV3)
    {
        Vector3 V3 = new Vector3();
        V3.x = SV3.x;
        V3.y = SV3.y;
        V3.z = SV3.z;
        return V3;
    }
    
    public SerQuaternion QuaternionToSerQuaternion(Quaternion Q)
    {
        SerQuaternion SQ = new SerQuaternion();
        SQ.x = Q.x;
        SQ.y = Q.y;
        SQ.z = Q.z;
        SQ.w = Q.w;
        return SQ;
    }
    public Quaternion SerQuaternionToQuaternion(SerQuaternion SQ)
    {
        Quaternion Q = new Quaternion();
        Q.x = SQ.x;
        Q.y = SQ.y;
        Q.z = SQ.z;
        Q.w = SQ.w;
        return Q;
    }

}
