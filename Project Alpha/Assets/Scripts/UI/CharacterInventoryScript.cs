using System.Collections;
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
                InventoryItemAmount[i] = 0;
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
                else if(ItemManager.InventoryItemList[itemid].armorType == ItemManagerScript.InventoryItem.ArmorType.head)
                {
                    int temp = EquipmentStorage[0].itemId;
                    SetEquipmentItem(0, itemid);
                    EquipmentStorage[0].itemId = itemid;
                    for (int i = 0; i < InventoryStorage.Length; i++)
                    {
                        if (InventoryStorage[i].itemId == 3)
                        {
                            SetInvenoryItem(i, temp);
                            break;
                        }
                    }
                }
                else if (ItemManager.InventoryItemList[itemid].armorType == ItemManagerScript.InventoryItem.ArmorType.boots)
                {
                    int temp = EquipmentStorage[2].itemId;
                    SetEquipmentItem(2, itemid);
                    EquipmentStorage[2].itemId = itemid;
                    for (int i = 0; i < InventoryStorage.Length; i++)
                    {
                        if (InventoryStorage[i].itemId == 3)
                        {
                            SetInvenoryItem(i, temp);
                            break;
                        }
                    }
                }
                else if (ItemManager.InventoryItemList[itemid].armorType == ItemManagerScript.InventoryItem.ArmorType.ring)
                {
                    if(EquipmentStorage[4].itemId == 3)
                    {
                        int temp = EquipmentStorage[4].itemId;
                        SetEquipmentItem(4, itemid);
                        EquipmentStorage[4].itemId = itemid;
                        for (int i = 0; i < InventoryStorage.Length; i++)
                        {
                            if (InventoryStorage[i].itemId == 3)
                            {
                                SetInvenoryItem(i, temp);
                                break;
                            }
                        }
                    }
                    else if (EquipmentStorage[5].itemId == 3)
                    {
                        int temp = EquipmentStorage[5].itemId;
                        SetEquipmentItem(5, itemid);
                        EquipmentStorage[5].itemId = itemid;
                        for (int i = 0; i < InventoryStorage.Length; i++)
                        {
                            if (InventoryStorage[i].itemId == 3)
                            {
                                SetInvenoryItem(i, temp);
                                break;
                            }
                        }
                    }
                    else
                    {
                        int temp = EquipmentStorage[4].itemId;
                        SetEquipmentItem(4, itemid);
                        EquipmentStorage[4].itemId = itemid;
                        for (int i = 0; i < InventoryStorage.Length; i++)
                        {
                            if (InventoryStorage[i].itemId == 3)
                            {
                                SetInvenoryItem(i, temp);
                                break;
                            }
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
                FindObjectOfType<AudioPlayerScript>().PlayAudio("Drink", GameObject.Find("Player").transform.position);
                break;

            case ItemManagerScript.InventoryItem.ItemType.weapon:              
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

            case ItemManagerScript.InventoryItem.ItemType.buff:
                if(ItemManager.InventoryItemList[itemid].attributeType == ItemManagerScript.InventoryItem.AttributeType.intelligence)
                {
                    ItemManagerScript.InventoryItem i = ItemManager.InventoryItemList[itemid];
                    GameObject g = Instantiate((GameObject)Resources.Load("Spells/InvisibleSpell"));
                    g.GetComponent<SpellBase>().thisSpell = FindObjectOfType<SpellDatabase>().ItemToSpellConverter(i.ItemName, i.attributeBuff, i.duration, 4, 2);
                    g.GetComponent<SpellBase>().caster = GameObject.Find("Player");

                }                
                else if (ItemManager.InventoryItemList[itemid].attributeType == ItemManagerScript.InventoryItem.AttributeType.strength)
                {
                    ItemManagerScript.InventoryItem i = ItemManager.InventoryItemList[itemid];
                    GameObject g = Instantiate((GameObject)Resources.Load("Spells/InvisibleSpell"));
                    g.GetComponent<SpellBase>().thisSpell = FindObjectOfType<SpellDatabase>().ItemToSpellConverter(i.ItemName, i.attributeBuff, i.duration, 4, 3);
                    g.GetComponent<SpellBase>().caster = GameObject.Find("Player");
                }
                else if (ItemManager.InventoryItemList[itemid].attributeType == ItemManagerScript.InventoryItem.AttributeType.Vitality)
                {
                    ItemManagerScript.InventoryItem i = ItemManager.InventoryItemList[itemid];
                    GameObject g = Instantiate((GameObject)Resources.Load("Spells/InvisibleSpell"));
                    g.GetComponent<SpellBase>().thisSpell = FindObjectOfType<SpellDatabase>().ItemToSpellConverter(i.ItemName, i.attributeBuff, i.duration, 4, 1);
                    g.GetComponent<SpellBase>().caster = GameObject.Find("Player");
                }
                else if (ItemManager.InventoryItemList[itemid].attributeType == ItemManagerScript.InventoryItem.AttributeType.Dexterity)
                {
                    ItemManagerScript.InventoryItem i = ItemManager.InventoryItemList[itemid];
                    GameObject g = Instantiate((GameObject)Resources.Load("Spells/InvisibleSpell"));
                    g.GetComponent<SpellBase>().thisSpell = FindObjectOfType<SpellDatabase>().ItemToSpellConverter(i.ItemName, i.attributeBuff, i.duration, 4, 5);
                    g.GetComponent<SpellBase>().caster = GameObject.Find("Player");
                }
                else if (ItemManager.InventoryItemList[itemid].attributeType == ItemManagerScript.InventoryItem.AttributeType.Resistance)
                {
                    ItemManagerScript.InventoryItem i = ItemManager.InventoryItemList[itemid];
                    GameObject g = Instantiate((GameObject)Resources.Load("Spells/InvisibleSpell"));
                    g.GetComponent<SpellBase>().thisSpell = FindObjectOfType<SpellDatabase>().ItemToSpellConverter(i.ItemName, i.attributeBuff, i.duration, 4,6);
                    g.GetComponent<SpellBase>().caster = GameObject.Find("Player");
                }
                else if (ItemManager.InventoryItemList[itemid].attributeType == ItemManagerScript.InventoryItem.AttributeType.Luck)
                {
                    ItemManagerScript.InventoryItem i = ItemManager.InventoryItemList[itemid];
                    GameObject g = Instantiate((GameObject)Resources.Load("Spells/InvisibleSpell"));
                    g.GetComponent<SpellBase>().thisSpell = FindObjectOfType<SpellDatabase>().ItemToSpellConverter(i.ItemName, i.attributeBuff, i.duration, 4, 4);
                    g.GetComponent<SpellBase>().caster = GameObject.Find("Player");
                }
                FindObjectOfType<AudioPlayerScript>().PlayAudio("Drink", GameObject.Find("Player").transform.position);
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
    public void OpenEquipmentCanvas()
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
            if (inventoryCanvas.activeSelf == false)
                OpenInventoryCanvas();
            else
                inventoryCanvas.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.O) && gameObject.GetComponent<PlayerController>())
        {
            if (equipmentCanvas.activeSelf == false)
                OpenEquipmentCanvas();
            else
                equipmentCanvas.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.Escape) && gameObject.GetComponent<PlayerController>())
        {
            if (inventoryCanvas.activeSelf)
                inventoryCanvas.SetActive(false);
            if (equipmentCanvas.activeSelf)
                equipmentCanvas.SetActive(false);
        }

        for (int i = 0; i < InventoryStorage.Length; i++)
        {
            if (InventoryStorage[i].itemId == 3)
            {
                InventoryItemAmount[i] = 0;
            }
        }

    }
}
