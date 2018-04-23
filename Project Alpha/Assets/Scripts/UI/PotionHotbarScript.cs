using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionHotbarScript : MonoBehaviour {
    
    public int type;
    int potion;
    bool potionHasBeenSet;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        potionHasBeenSet = false;
            for (int i = 0; i < GameObject.Find("Player").GetComponent<CharacterInventoryScript>().InventoryStorage.Length; i++)
            {
                if (GameObject.Find("Player").GetComponent<CharacterInventoryScript>().InventoryStorage[i].itemId == type)
                {
                    potion = i;
                    potionHasBeenSet = true;
                    break;
                }
            }
            if(potionHasBeenSet)
            transform.Find("Text").GetComponent<Text>().text = GameObject.Find("Player").GetComponent<CharacterInventoryScript>().InventoryItemAmount[potion].ToString();
        
	}
}
