using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterEquipmentCanvasScript : MonoBehaviour
{
    public GameObject owner;
    Vector3 deltaMousePos;
    Vector3 previousMousePos;
    public List<GameObject> ItemSlotList = new List<GameObject>();
    public GameObject infoPanel;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(owner == null)
        {
            transform.parent.gameObject.AddComponent<DestroyScript>();
        }

        deltaMousePos = Input.mousePosition - previousMousePos;
        previousMousePos = Input.mousePosition;

        for (int i = 0; i < ItemSlotList.Count; i++)
        {
            ItemSlotList[i].GetComponent<Image>().overrideSprite = owner.GetComponent<CharacterInventoryScript>().EquipmentStorage[i].itemTexture;
        }

        if (Vector3.Distance(Camera.main.transform.position, owner.transform.position) > 50)
        {
            transform.parent.gameObject.SetActive(false);
        }
        if (infoPanel.activeInHierarchy)
            infoPanel.transform.position = Input.mousePosition - new Vector3(0, Screen.height * 0.3f);
    }
    public void OnDrag()
    {
        transform.position += deltaMousePos;
    }
    public void OnCursorEnter(int pos)
    {
        if (owner.GetComponent<CharacterInventoryScript>().EquipmentStorage[pos].itemId != 3)
        {
            infoPanel.gameObject.SetActive(true);
            infoPanel.transform.position = Input.mousePosition - new Vector3(0, 150);
            infoPanel.GetComponentInChildren<Text>().text = (owner.GetComponent<CharacterInventoryScript>().EquipmentStorage[pos].ItemName.ToString());

            if(owner.GetComponent<CharacterInventoryScript>().EquipmentStorage[pos].armorRating != 0)
            {
                infoPanel.GetComponentInChildren<Text>().text = infoPanel.GetComponentInChildren<Text>().text + "\n" + "Armor Rating: " +
                    owner.GetComponent<CharacterInventoryScript>().EquipmentStorage[pos].armorRating;
            }
            if (owner.GetComponent<CharacterInventoryScript>().EquipmentStorage[pos].attackRating != 0)
            {
                infoPanel.GetComponentInChildren<Text>().text = infoPanel.GetComponentInChildren<Text>().text + "\n" + "Attack Rating: " +
                    owner.GetComponent<CharacterInventoryScript>().EquipmentStorage[pos].attackRating;
            }
            
        }
    }
    public void OnCursorExit()
    {
        infoPanel.gameObject.SetActive(false);
    }
    public void OnItemPress(int itemslot)
    {

        if (owner.GetComponent<CharacterInventoryScript>().InventoryStorage[itemslot].itemId != 3 && owner.GetComponent<CharacterInventoryScript>().InventoryStorage[itemslot].can_use)
        {
            owner.GetComponent<CharacterInventoryScript>().OnUseItem(owner.GetComponent<CharacterInventoryScript>().InventoryStorage[itemslot].itemId);
            owner.GetComponent<CharacterInventoryScript>().InventoryItemAmount[itemslot]--;
            if (owner.GetComponent<CharacterInventoryScript>().InventoryItemAmount[itemslot] <= 0)
            {
                owner.GetComponent<CharacterInventoryScript>().SetInvenoryItem(itemslot, 3);
            }
        }
        GameObject temp = GameObject.Find("me");
        bool isFinished = false;
        if (isFinished == false)
        {
            for (int i = 0; i < owner.GetComponent<CharacterInventoryScript>().InventoryStorage.Length; i++)
            {
                if (temp.GetComponent<CharacterInventoryScript>().InventoryStorage[i].itemId == owner.GetComponent<CharacterInventoryScript>().InventoryStorage[itemslot].itemId
                    && temp.GetComponent<CharacterInventoryScript>().InventoryItemAmount[i] < temp.GetComponent<CharacterInventoryScript>().InventoryStorage[i].itemMaxAmount)
                {
                    temp.GetComponent<CharacterInventoryScript>().InventoryItemAmount[i]++;
                    owner.GetComponent<CharacterInventoryScript>().InventoryItemAmount[itemslot]--;
                    if (owner.GetComponent<CharacterInventoryScript>().InventoryItemAmount[itemslot] <= 0)
                    {
                        owner.GetComponent<CharacterInventoryScript>().SetInvenoryItem(itemslot, 3);
                    }
                    isFinished = true;
                    break;
                }   
            }


            if (isFinished == false)
            {
                for (int i = 0; i < owner.GetComponent<CharacterInventoryScript>().InventoryStorage.Length; i++)
                {
                    if (temp.GetComponent<CharacterInventoryScript>().InventoryStorage[i].itemId == 3)
                    {
                        temp.GetComponent<CharacterInventoryScript>().SetInvenoryItem(i, owner.GetComponent<CharacterInventoryScript>().InventoryStorage[itemslot].itemId);
                        temp.GetComponent<CharacterInventoryScript>().InventoryItemAmount[itemslot] = owner.GetComponent<CharacterInventoryScript>().InventoryItemAmount[itemslot];
                        owner.GetComponent<CharacterInventoryScript>().InventoryItemAmount[itemslot]--;
                        if (owner.GetComponent<CharacterInventoryScript>().InventoryItemAmount[itemslot] <= 0)
                        {
                            owner.GetComponent<CharacterInventoryScript>().SetInvenoryItem(itemslot, 3);
                        }
                        isFinished = true;
                        break;
                    }
                }
            }

        }

    }

 }
