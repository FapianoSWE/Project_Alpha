using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour {
    public ItemManagerScript ItemManager;
    public GameObject Player;
    public GameObject inventoryCanvas;
    public bool playerHasInstantiatedInventoryCanvas = false;
    public ItemManagerScript.InventoryItem[] ShopInventory = new ItemManagerScript.InventoryItem[16];
	// Use this for initialization
	void Start () {
        ItemManager = GameObject.Find("Item Manager").GetComponent<ItemManagerScript>();

        SetShopInventory(0, 0);
        SetShopInventory(1, 1);
        for (int i = 0; i < ShopInventory.Length; i++)
        {
            if(ShopInventory[i]==null)
            {
                SetShopInventory(i, 3);
          }
        }
    }
	void SetShopInventory(int itemslot, int itemid)
    {
        ShopInventory[itemslot] = ItemManager.InventoryItemList[itemid];
    }
    public void OpenInventoryCanvas()
    {
        if (playerHasInstantiatedInventoryCanvas)
        {
            inventoryCanvas.SetActive(true);
        }
        else
        {
            GameObject temp = Instantiate(inventoryCanvas);
            inventoryCanvas = temp;
            inventoryCanvas.GetComponentInChildren<CharacterInventoryCanvasScript>().owner = gameObject;
            playerHasInstantiatedInventoryCanvas = true;
        }
    }
    // Update is called once per frame
    void Update () {
        if(Player == null)
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player"))
            {
                Player = g;
                break;
                
            }
        }
    }
   public void OnPurchase(int itemid)
    {
        print("Value : " + ItemManager.GetComponent<ItemManagerScript>().InventoryItemList[itemid].value.ToString());
        if(ItemManager.GetComponent<ItemManagerScript>().InventoryItemList[itemid].value <= 
            Player.GetComponent<CharacterStatsScript>().gold)
        {
            for (int i = 0; i < Player.GetComponent<CharacterInventoryScript>().InventoryStorage.Length; i++)
            {
                if(Player.GetComponent<CharacterInventoryScript>().InventoryStorage[i].itemId == 3)
                {
                    Player.GetComponent<CharacterInventoryScript>().SetInvenoryItem(i, itemid);
                    Player.GetComponent<CharacterStatsScript>().gold -= ItemManager.GetComponent<ItemManagerScript>().InventoryItemList[itemid].value;
                    break;
                }
                else if(Player.GetComponent<CharacterInventoryScript>().InventoryStorage[i].itemId == itemid)
                {
                    Player.GetComponent<CharacterInventoryScript>().InventoryItemAmount[i] = Player.GetComponent<CharacterInventoryScript>().InventoryItemAmount[i] +1;
                    Player.GetComponent<CharacterStatsScript>().gold -= ItemManager.GetComponent<ItemManagerScript>().InventoryItemList[itemid].value;
                    break;
                }
            }
        }
    }
}
