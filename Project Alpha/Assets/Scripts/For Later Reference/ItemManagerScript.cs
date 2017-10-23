using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManagerScript : MonoBehaviour {
    public List<InventoryItem> InventoryItemList = new List<InventoryItem>();
    public class InventoryItem
    {
        public int itemId,
            value,
            armorRating,
            attackRating,
            healRating,
            itemMaxAmount;
        public string ItemName,
            ItemNames;
        public bool can_use;
        public Sprite itemTexture;
        public ItemType itemType = new ItemType();
        public HealType healType = new HealType();
        public WeaponType weaponType = new WeaponType();
        public ArmorType armorType = new ArmorType();
        public enum ItemType
        {
            healing,
            loot,
            gold,
            armor,
            weapon,
        }
        public enum ArmorType
        {
            head,
            body,
            legs,
            ring,
        }
        public enum WeaponType
        {
            sword,
            bow,
            staff,
        }
        public enum HealType
        {
            health,
            mana,
        }

        public InventoryItem(int itemid, int maxamount, int Value, ItemType itemtype,string itemName,string itemNames, int healrating, HealType healtype, string texturename, bool canUse)
        {
            itemId = itemid;
            itemType = itemtype;
            healRating = healrating;
            can_use = canUse;
            itemMaxAmount = maxamount;
            value = Value;
            ItemName = itemName;
            ItemNames = itemNames;
            itemTexture = Resources.Load<Sprite>("InventoryIcons/" + texturename);
        }

        public InventoryItem(int itemid, int maxamount, int Value, ItemType itemtype, string itemName, string itemNames, ArmorType armortype, int armorrating, string texturename, bool canUse)
        {
            itemId = itemid;
            itemType = itemtype;
            armorType = armortype;
            armorRating = armorrating;
            can_use = canUse;
            itemMaxAmount = maxamount;
            value = Value;
            ItemName = itemName;
            ItemNames = itemNames;
            itemTexture = Resources.Load<Sprite>("InventoryIcons/" + texturename);
        }
        public InventoryItem(int itemid, int maxamount, int Value, ItemType itemtype, string itemName, string itemNames, WeaponType weapontype, int attackrating, string texturename, bool canUse)
        {
            itemId = itemid;
            itemType = itemtype;
            weaponType = weapontype;
            attackRating = attackrating;
            can_use = canUse;
            itemMaxAmount = maxamount;
            value = Value;
            ItemName = itemName;
            ItemNames = itemNames;
            itemTexture = Resources.Load<Sprite>("InventoryIcons/" + texturename);
        }
        public InventoryItem(int itemid, string texturename, bool canUse)
        {
            itemId = itemid;
            can_use = canUse;
            itemTexture = Resources.Load<Sprite>("InventoryIcons/" + texturename);
        }
        public InventoryItem(int itemid, int maxamount, int Value, bool canUse, string texturename)
        {
            itemId = itemid;
            can_use = canUse;
            itemMaxAmount = maxamount;
            value = Value;
            itemTexture = Resources.Load<Sprite>("InventoryIcons/" + texturename);
        }

    }
    // Use this for initialization
    void Start () {
        InventoryItemList.Add(new InventoryItem(0, 64, 50, InventoryItem.ItemType.healing,"Health Potion(50)","Health Potions(50)", 50, InventoryItem.HealType.health, "healthpotion", true));
        InventoryItemList.Add(new InventoryItem(1, 64, 50, InventoryItem.ItemType.healing, "Mana Potion(50)", "Mana Potions(50)", 50, InventoryItem.HealType.mana, "manapotion", true));
        InventoryItemList.Add(new InventoryItem(2, 1, 250, InventoryItem.ItemType.armor,"Diamond Armor","Diamond Armors", InventoryItem.ArmorType.body, 50, "diamondarmor", true));
        InventoryItemList.Add(new InventoryItem(3, "transparent", false));
        InventoryItemList.Add(new InventoryItem(4, 10000, 0, false, "money"));
        InventoryItemList.Add(new InventoryItem(5, 1, 100, InventoryItem.ItemType.armor, "Iron Armor", "Iron Armors", InventoryItem.ArmorType.body, 25, "ironarmor", true));
        InventoryItemList.Add(new InventoryItem(6, 1, 250, InventoryItem.ItemType.weapon, "Diamond Sword", "Diamond Sword", InventoryItem.WeaponType.sword, 50, "diamondsword", true));
        InventoryItemList.Add(new InventoryItem(7, 1, 100, InventoryItem.ItemType.weapon, "Iron Sword", "Iron Swords", InventoryItem.WeaponType.sword, 25, "ironsword", true));
    }

    // Update is called once per frame
    void Update () {
		
	}
}
