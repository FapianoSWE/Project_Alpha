using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    public CharacterInventoryScript InventoryScript;
    int empySlots = 0;
    float aliveTime;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        empySlots = 0;
        aliveTime += 0.1f;
        for (int i = 0; i < InventoryScript.InventoryStorage.Length; i++)
        {
            if (InventoryScript.InventoryStorage[i].itemId == 3)
            {
                empySlots++;
            }
        }
        if (empySlots == 16 && aliveTime >= 2|| Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) > 100)
        {
            Destroy(GetComponent<CharacterInventoryScript>().equipmentCanvas);
            Destroy(GetComponent<CharacterInventoryScript>().inventoryCanvas);
            Destroy(gameObject);
        }
    }
}
