using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatsScript : MonoBehaviour
{

    Text level,
         xp,
         health,
         mana;

    public GameObject textPrefab;

    public int baseCritChance = 10,
               currentCritChance;

    GameObject tempUI;

    Text vitalitypoints,
         intelligencepoints,
         strengthpoints,
         agilitypoints,
         luckpoints,
         dexteritypoints,
         resistancepoints,
         _AttributePonts,
         goldamount;

    Button intButton,
           strButton;

    Text[] visualText;

    public StatUIScript statUI;

    public enum DamageTypes
    {
        Melee,
        Ranged,
        Magic
    }


    public bool IsPlayer;

    bool leveledUp = false;

    public int 
        attributePoints = 50,
        vitality,
        intelligence,
        strength,
        agility,
        luck,
        dexterity,
        resistance,
        gold;

    float regenTimer = 1;

    public int 
        currentLevel = 1,
        levelCap,
        currentXp,
        xpNeeded,
        baseXpNeeded = 100;

    public float 
        currentMana,
        maxMana,
        baseMana = 150,
        baseManaRegen = 5,
        manaRegen;

    public float currentHealth,
        maxHealth,
        damageTaken,
        baseHealth = 50,
        healthRegen,
        baseHealthRegen = 5;

    public float speed = 100;

    public bool regenEnabled;

    public void Start()
    {

        statUI = GameObject.FindGameObjectWithTag("StatContainer").GetComponent<StatUIScript>();

        if(IsPlayer)
        {
            //visualText = GameObject.Find("Character Stats").GetComponentsInChildren<Text>();
    /*
            xpNeeded = baseXpNeeded;
            level = visualText[0];
            xp = visualText[1];
            health = visualText[2];
            mana = visualText[3];
            */
            GameObject.Find("IntPlus").GetComponent<Button>().onClick.AddListener(delegate { AddIntelligence(); });
            GameObject.Find("VitPlus").GetComponent<Button>().onClick.AddListener(delegate { AddVitality(); }); ;
            GameObject.Find("StrPlus").GetComponent<Button>().onClick.AddListener(delegate { AddStrength(); }); ;
            //GameObject.Find("AgiPlus").GetComponent<Button>().onClick.AddListener(delegate { AddAgility(); }); ;
            GameObject.Find("LuckPlus").GetComponent<Button>().onClick.AddListener(delegate { AddLuck(); }); ;
            GameObject.Find("DexPlus").GetComponent<Button>().onClick.AddListener(delegate { AddDexterity(); }); ;
            GameObject.Find("ResPlus").GetComponent<Button>().onClick.AddListener(delegate { AddResistance(); }); ;

            /*
            vitalitypoints = GameObject.Find("Vitality").GetComponent<Text>();
            intelligencepoints = GameObject.Find("Intelligence").GetComponent<Text>();
            strengthpoints = GameObject.Find("Strength").GetComponent<Text>();
            agilitypoints = GameObject.Find("Agility").GetComponent<Text>();
            luckpoints = GameObject.Find("Luck").GetComponent<Text>();
            dexteritypoints = GameObject.Find("Dexterity").GetComponent<Text>();
            resistancepoints = GameObject.Find("Resistance").GetComponent<Text>();
            _AttributePonts = GameObject.Find("Attribute Points").GetComponent<Text>();
            goldamount = GameObject.Find("Gold Amount").GetComponent<Text>();
            tempUI.gameObject.SetActive(false);
            */
        }

        currentHealth = baseHealth;
        currentMana = baseMana;


    }

    public void LevelUp(int overXP)
    {
        currentLevel += 1;
        currentXp = 0 + overXP;
        xpNeeded = currentLevel * baseXpNeeded;
        attributePoints += 3;
        leveledUp = true;
        FindObjectOfType<AudioPlayerScript>().PlayAudio("LevelUp", transform.position);
    }

    public float DealDamage(float baseDamage, DamageTypes _DamageType)
    {
            int crit = Random.Range(0, 100);

            if (_DamageType == DamageTypes.Melee)
            {
                float damage;
            if (GetComponent<CharacterInventoryScript>().EquipmentStorage[3].weaponType == ItemManagerScript.InventoryItem.WeaponType.sword)
                damage = baseDamage * (0.9f + ((float)strength / 10)) + GetComponent<CharacterInventoryScript>().EquipmentStorage[3].attackRating;
            else
                damage = baseDamage * (0.9f + ((float)strength / 10));

                if (crit <= currentCritChance)
                {
                    damage *= 2;
                }
                return damage;
            }
            else if (_DamageType == DamageTypes.Ranged)
            {
                float damage;
                damage = baseDamage * (0.9f + ((float)agility / 10)); //+ GetComponent<CharacterInventoryScript>().EquipmentStorage[3].attackRating);

            if (crit <= currentCritChance)
                {
                    damage *= 2;
                }
                return damage;
            }
            else if (_DamageType == DamageTypes.Magic)
            {
                float damage;
            if (!GetComponent<EnemyAIScript>() && GetComponent<CharacterInventoryScript>().EquipmentStorage[3].weaponType == ItemManagerScript.InventoryItem.WeaponType.staff)
                damage = baseDamage * (0.9f + ((float)intelligence / 10) + ((float)GetComponent<CharacterInventoryScript>().EquipmentStorage[3].attackRating / 10));
            else
                damage = baseDamage * (0.9f + ((float)intelligence / 10));

                if (crit <= currentCritChance)
                {
                    damage *= 2;
                }
                return damage;
            }
            return 0;
    }

    public void TakeDamage(float incomingDamage)
    {
        if (IsPlayer)
        {
            int armor = 0;
            foreach (ItemManagerScript.InventoryItem i in GetComponent<CharacterInventoryScript>().EquipmentStorage)
            {
                armor += i.armorRating;
            }
            currentHealth -= (int)(incomingDamage - ((resistance * 2)+ armor));
            regenTimer = 3;
        }
        else if(!IsPlayer)
        {
            FindObjectOfType<EnemyHealthBar>().enemyObject = gameObject;
            currentHealth -= Mathf.RoundToInt(incomingDamage - (resistance * 2));
            GameObject t = Instantiate(textPrefab,transform.position,GameObject.Find("Player").transform.rotation);
            t.GetComponent<TextPrefabScript>().Initialize(Mathf.RoundToInt(incomingDamage - (resistance * 2)).ToString(), Color.red);
           
        }
        
    }

    public void Healed(float incomingHeal)
    {
        currentHealth += incomingHeal;
        if(!IsPlayer)
        {
            GameObject t = Instantiate(textPrefab, transform.position, GameObject.Find("Player").transform.rotation);
            t.GetComponent<TextPrefabScript>().Initialize(incomingHeal.ToString(), Color.green);
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void GainXP(int xpGained)
    {
            currentXp += xpGained;
    }

    public void AddIntelligence()
    {
        if (attributePoints > 0)
        {
            attributePoints -= 1;
            intelligence += 1;
            FindObjectOfType<AudioPlayerScript>().PlayAudio("Button", transform.position);
        }
    }

    public void AddStrength()
    {
        if (attributePoints > 0)
        {
            attributePoints -= 1;
            strength += 1;
            FindObjectOfType<AudioPlayerScript>().PlayAudio("Button", transform.position);
        }
    }

    public void AddDexterity()
    {
        if (attributePoints > 0)
        {
            attributePoints -= 1;
            dexterity += 1;
            FindObjectOfType<AudioPlayerScript>().PlayAudio("Button", transform.position);
        }
    }

    public void AddVitality()
    {
        if (attributePoints > 0)
        {
            attributePoints -= 1;
            vitality += 1;
            FindObjectOfType<AudioPlayerScript>().PlayAudio("Button", transform.position);
        }
    }

    public void AddResistance()
    {
        if (attributePoints > 0)
        {
            attributePoints -= 1;
            resistance += 1;
            FindObjectOfType<AudioPlayerScript>().PlayAudio("Button", transform.position);
        }
    }

    public void AddLuck()
    {
        if (attributePoints > 0)
        {
            attributePoints -= 1;
            luck += 1;
            FindObjectOfType<AudioPlayerScript>().PlayAudio("Button", transform.position);
        }
    }

    public void AddAgility()
    {
        if (attributePoints > 0)
        {
            attributePoints -= 1;
            agility += 1;
            FindObjectOfType<AudioPlayerScript>().PlayAudio("Button", transform.position);
        }
    }
    

    public void Update()
    {

        if(Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            agility = 1000;
            strength = 1000;
            vitality = 1000;
            intelligence = 1000;
            luck = 1000;
            dexterity = 1000;
            currentLevel = 1000;
        }

        if (Input.GetKey(KeyCode.Keypad0))
        {
            GainXP(50);
        }
        if (Input.GetKey(KeyCode.Keypad1))
        {
            gold++;
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            currentHealth--;
        }
        if (Input.GetKey(KeyCode.Keypad3))
        {
            regenEnabled = !regenEnabled;
        }
        regenTimer -= Time.deltaTime;
            maxHealth = (baseHealth * currentLevel + (vitality * 10)) - 10;
            maxMana = (baseMana * currentLevel + (intelligence * 5)) - 5;
            currentCritChance = baseCritChance + Mathf.RoundToInt(luck * 0.25f);
            manaRegen = (baseManaRegen * currentLevel + (intelligence * 2))-2;
            healthRegen = (baseHealthRegen * currentLevel + (vitality * 2))-2;

            if (leveledUp)
            {
                currentHealth = maxHealth;
                currentMana = maxMana;

                leveledUp = false;
            }

            if (regenTimer <= 0 && IsPlayer && regenEnabled)
            {
                currentHealth += healthRegen;
                currentMana += manaRegen;
                regenTimer = 3;
            }

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }

            if (currentMana > maxMana)
            {
                currentMana = maxMana;
            }
            
            if(IsPlayer)
            {

            if (Input.GetKeyDown(KeyCode.C))
            {
                if (!statUI.statsActive)
                {
                    statUI.statsActive = true;
                }
                else if (statUI.statsActive)
                {
                    statUI.statsActive = false;
                }

            }
                if (currentXp >= xpNeeded)
                {
                    int overXP = currentXp - xpNeeded;
                    LevelUp(overXP);
                }
            }
           
           
        
            
    }

    


}
