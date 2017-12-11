using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInventoryCanvasScript : MonoBehaviour
{
    Vector3 deltaMousePos;
    Vector3 previousMousePos;
    public List<GameObject> ItemSlotList = new List<GameObject>();
    public GameObject owner,
        infoPanel;
    // Use this for initialization
    void Start()
    {

    }
    private void OnDestroy()
    {
        owner = null;
    }
    // Update is called once per frame
    void Update()
    {
        deltaMousePos = Input.mousePosition - previousMousePos;
        previousMousePos = Input.mousePosition;
        if (owner == null)
        {
            transform.parent.gameObject.AddComponent<DestroyScript>();
        }


        for (int i = 0; i < ItemSlotList.Count; i++)
        {
            if (owner.GetComponent<CharacterInventoryScript>())
            {
                ItemSlotList[i].GetComponent<Image>().overrideSprite = owner.GetComponent<CharacterInventoryScript>().InventoryStorage[i].itemTexture;
            }
            else if (owner.GetComponent<ShopScript>())
            {
                ItemSlotList[i].GetComponent<Image>().overrideSprite = owner.GetComponent<ShopScript>().ShopInventory[i].itemTexture;
            }
        } 
        for (int i = 0; i < ItemSlotList.Count; i++)
        {

            if (owner.GetComponent<ShopScript>())
            {
                if (owner.GetComponent<ShopScript>().ShopInventory[i] != null)
                {
                    ItemSlotList[i].GetComponentInChildren<Text>().text = owner.GetComponent<ShopScript>().ShopInventory[i].value.ToString();
                }
            }
            else if (owner.GetComponent<CharacterInventoryScript>().InventoryStorage[i].itemMaxAmount > 1)
            {
                ItemSlotList[i].GetComponentInChildren<Text>().text = owner.GetComponent<CharacterInventoryScript>().InventoryItemAmount[i].ToString();
            }
            else
            {
                ItemSlotList[i].GetComponentInChildren<Text>().text = " ";
            }

        }
        if (Vector3.Distance(Camera.main.transform.position, owner.transform.position) > 50)
        {
            transform.parent.gameObject.SetActive(false);
        }
        if (transform.position.y + 138 > Screen.height)
        {
            transform.position = new Vector2(transform.position.x, Screen.height - 138);
        }

        if (infoPanel.activeInHierarchy)
            infoPanel.transform.position = Input.mousePosition - new Vector3(0, Screen.height * 0.3f);
    }

    public void OnCursorEnter(int pos)
    {

        if (owner.GetComponent<CharacterInventoryScript>().InventoryStorage[pos].itemId != 3)
        {
            infoPanel.gameObject.SetActive(true);
            infoPanel.transform.position = Input.mousePosition - new Vector3(0, 150);
            infoPanel.GetComponentInChildren<Text>().text = (owner.GetComponent<CharacterInventoryScript>().InventoryStorage[pos].ItemName.ToString());
            if (owner.GetComponent<CharacterInventoryScript>().InventoryStorage[pos].armorRating != 0)
            {
                infoPanel.GetComponentInChildren<Text>().text = infoPanel.GetComponentInChildren<Text>().text + "\n" + "Armor Rating: " +
                    owner.GetComponent<CharacterInventoryScript>().InventoryStorage[pos].armorRating;
            }
            if (owner.GetComponent<CharacterInventoryScript>().InventoryStorage[pos].attackRating != 0)
            {
                infoPanel.GetComponentInChildren<Text>().text = infoPanel.GetComponentInChildren<Text>().text + "\n" + "Attack Rating: " +
                    owner.GetComponent<CharacterInventoryScript>().InventoryStorage[pos].attackRating;
            }
            if (owner.GetComponent<CharacterInventoryScript>().InventoryStorage[pos].healRating != 0)
            {
                if(owner.GetComponent<CharacterInventoryScript>().InventoryStorage[pos].healType == 0)
                {
                    infoPanel.GetComponentInChildren<Text>().text = infoPanel.GetComponentInChildren<Text>().text + "\n" + "Heals " +
                        owner.GetComponent<CharacterInventoryScript>().InventoryStorage[pos].healRating + " Health Points";
                }
                else
                {
                    infoPanel.GetComponentInChildren<Text>().text = infoPanel.GetComponentInChildren<Text>().text + "\n" + "Heals " +
                        owner.GetComponent<CharacterInventoryScript>().InventoryStorage[pos].healRating + " Mana Points";
                }
            }
        }
    }
    public void OnCursorExit()
    {
        infoPanel.gameObject.SetActive(false);
    }

    

    public void OnDrag()
    {
        transform.position += deltaMousePos;
    }
    public void OnItemPress(int itemslot)
    {
        if (owner.GetComponent<ShopScript>())
        {
            owner.GetComponent<ShopScript>().OnPurchase(owner.GetComponent<ShopScript>().ShopInventory[itemslot].itemId);
        }   
        else if (owner.GetComponent<CharacterInventoryScript>() && !owner.GetComponent<ChestScript>())
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
        }
        else
        {
            GameObject temp = GameObject.Find("Player");
            if (owner.GetComponent<CharacterInventoryScript>().InventoryStorage[itemslot].itemId == 4)
            {
                temp.GetComponent<CharacterStatsScript>().gold += owner.GetComponent<CharacterInventoryScript>().InventoryItemAmount[itemslot];
                owner.GetComponent<CharacterInventoryScript>().InventoryItemAmount[itemslot] = 0;
                if (owner.GetComponent<CharacterInventoryScript>().InventoryItemAmount[itemslot] <= 0)
                {
                    owner.GetComponent<CharacterInventoryScript>().SetInvenoryItem(itemslot, 3);
                }
            }
            else
            {
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
                }

                if (isFinished == false)
                {
                    for (int i = 0; i < owner.GetComponent<CharacterInventoryScript>().InventoryStorage.Length; i++)
                    {
                        if (temp.GetComponent<CharacterInventoryScript>().InventoryStorage[i].itemId == 3)
                        {
                            temp.GetComponent<CharacterInventoryScript>().SetInvenoryItem(i, owner.GetComponent<CharacterInventoryScript>().InventoryStorage[itemslot].itemId);
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

}
