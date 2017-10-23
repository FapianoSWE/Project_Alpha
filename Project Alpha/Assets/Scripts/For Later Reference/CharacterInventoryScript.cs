﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventoryScript : MonoBehaviour
{
    public ItemManagerScript ItemManager;
    public int[] InventoryItemAmount = new int[16];
    public ItemManagerScript.InventoryItem[] InventoryStorage = new ItemManagerScript.InventoryItem[16];
    public ItemManagerScript.InventoryItem[] EquipmentStorage = new ItemManagerScript.InventoryItem[8];
    public GameObject inventoryCanvas,
        equipmentCanvas;
    bool initialized = false,
        playerHasInstantiatedInventoryCanvas = false,
        playerHasInstantiatedEquipmentCanvas = false;

    public void SetInvenoryItem(int inventorySlot, int itemid)
    {
        if (!ItemManager)
            ItemManager = GameObject.Find("Item Manager").GetComponent<ItemManagerScript>();
        InventoryStorage[inventorySlot] = ItemManager.InventoryItemList[itemid];
        InventoryItemAmount[inventorySlot] = 1;
    }
    public void SetInvenoryItem(int inventorySlot, int itemid, int itemamount)
    {
        if (!ItemManager)
            ItemManager = GameObject.Find("Item Manager").GetComponent<ItemManagerScript>();
        InventoryStorage[inventorySlot] = ItemManager.InventoryItemList[itemid];
        InventoryItemAmount[inventorySlot] = 1;
        InventoryItemAmount[inventorySlot] = itemamount;
    }
    public void SetEquipmentItem(int equipmentSlot, int itemid)
    {
        EquipmentStorage[equipmentSlot] = ItemManager.InventoryItemList[itemid];
    }

    public void Initialize()
    {
        if (!ItemManager)
            ItemManager = GameObject.Find("Item Manager").GetComponent<ItemManagerScript>();
        for (int i = 0; i < InventoryStorage.Length; i++)
        {
            if (InventoryStorage[i] == null)
            {
                SetInvenoryItem(i, 3);
            }
        }
        for (int i = 0; i < EquipmentStorage.Length; i++)
        {
            if (EquipmentStorage[i] == null && i !=3)
            {
                SetEquipmentItem(i, 3);
            }
            if(i == 3)
            {
                SetEquipmentItem(3, 7);
            }
        }
        OpenInventoryCanvas();
        OpenEquipmentCanvas();
        inventoryCanvas.SetActive(false);
        equipmentCanvas.SetActive(false);
        initialized = true;
        print("initialized!");
    }
  //  Use this for initialization

   void Start()
    {
        if (!initialized)
        {
            Initialize();
        }
    }

    public void OnUseItem(int itemid)
    {
        GameObject target;

        target = gameObject;


        switch (ItemManager.InventoryItemList[itemid].itemType)
        {
            case ItemManagerScript.InventoryItem.ItemType.armor:
                if (ItemManager.InventoryItemList[itemid].armorType == ItemManagerScript.InventoryItem.ArmorType.body)
                {
                    int temp = EquipmentStorage[1].itemId;
                    SetEquipmentItem(1, itemid);
                    EquipmentStorage[1].itemId = itemid;
                    for (int i = 0; i < InventoryStorage.Length; i++)
                    {
                        if (InventoryStorage[i].itemId == 3)
                        {
                            SetInvenoryItem(i, temp);
                            break;
                        }
                    }
                }
                break;

            case ItemManagerScript.InventoryItem.ItemType.healing:
                if (ItemManager.InventoryItemList[itemid].healType == ItemManagerScript.InventoryItem.HealType.health)
                {
                    target.GetComponent<CharacterStatsScript>().currentHealth += ItemManager.InventoryItemList[itemid].healRating;
                }
                else
                {
                    target.GetComponent<CharacterStatsScript>().currentMana += ItemManager.InventoryItemList[itemid].healRating;
                }
                break;

            case ItemManagerScript.InventoryItem.ItemType.weapon:
                if (ItemManager.InventoryItemList[itemid].weaponType == ItemManagerScript.InventoryItem.WeaponType.sword)
                {
                    int temp = EquipmentStorage[3].itemId;
                    SetEquipmentItem(3, itemid);
                    EquipmentStorage[3].itemId = itemid;
                    for (int i = 0; i < InventoryStorage.Length; i++)
                    {
                        if (InventoryStorage[i].itemId == 3)
                        {
                            SetInvenoryItem(i, temp);
                            break;
                        }
                    }
                }
                break;

        }
    }
    public void RemoveItem(int itemid, int count)
    {
        for (int i = 0; i < InventoryStorage.Length; i++)
        {
            if (InventoryStorage[i].itemId == itemid)
            {
                if (InventoryItemAmount[i] > count)
                {
                    InventoryItemAmount[i] -= count;
                    return;
                }
                else if (InventoryItemAmount[i] == count)
                {
                    InventoryItemAmount[i] = 0;
                    InventoryStorage[i] = ItemManager.GetComponent<ItemManagerScript>().InventoryItemList[3];
                    return;
                }
                else
                {
                    print("Error: Not enough items to complete quest");
                    return;
                }
            }
        }

    }
    void OpenInventoryCanvas()
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
    void OpenEquipmentCanvas()
    {
        if (playerHasInstantiatedEquipmentCanvas)
        {
            equipmentCanvas.SetActive(true);
        }
        else
        {
            GameObject temp = Instantiate(equipmentCanvas);
            equipmentCanvas = temp;
            equipmentCanvas.GetComponentInChildren<CharacterEquipmentCanvasScript>().owner = gameObject;
            playerHasInstantiatedEquipmentCanvas = true;
        }

    }
   // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)&&gameObject.GetComponent<PlayerController>())
        {
            OpenInventoryCanvas();
        }
        if (Input.GetKeyDown(KeyCode.O) && gameObject.GetComponent<PlayerController>())
        {
            OpenEquipmentCanvas();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 50.0f))
            {
                if (hit.transform != null)
                {
                    if(hit.transform.gameObject.GetComponent<CharacterInventoryScript>())
                    {
                        hit.transform.gameObject.GetComponent<CharacterInventoryScript>().OpenInventoryCanvas();
                    }
                    if(hit.transform.gameObject.GetComponent<ShopScript>())
                    {
                        hit.transform.gameObject.GetComponent<ShopScript>().OpenInventoryCanvas();
                    }
                    if(hit.transform.gameObject.GetComponent<QuestGiverScript>())
                    {
                        hit.transform.gameObject.GetComponent<QuestGiverScript>().OnPress(gameObject);
                    }
                    if (hit.transform.gameObject.GetComponent<QuestTalkScript>())
                    {
                        hit.transform.gameObject.GetComponent<QuestTalkScript>().OnPress(gameObject);
                    }

                }
            }

        }
    }
}
