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

    public GameObject critPrefab;

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

    public int currentHealth,
        maxHealth,
        damageTaken,
        baseHealth = 50,
        healthRegen,
        baseHealthRegen = 5;

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
    }

    public int DealDamage(int baseDamage, DamageTypes _DamageType)
    {
            int crit = Random.Range(0, 100);

            if (_DamageType == DamageTypes.Melee)
            {
                int damage;
                damage = baseDamage + ((strength * 2)) + GetComponent<CharacterInventoryScript>().EquipmentStorage[3].attackRating;
                if (crit <= currentCritChance)
                {
                    damage *= 2;
                    Instantiate(critPrefab, transform.position, Quaternion.identity, null);
                }
                return damage;
            }
            else if (_DamageType == DamageTypes.Ranged)
            {
                int damage;
                damage = baseDamage + ((agility * 2)); //+ GetComponent<CharacterInventoryScript>().EquipmentStorage[3].attackRating);

                if (crit <= currentCritChance)
                {
                    damage *= 2;
                    Instantiate(critPrefab, transform.position, Quaternion.identity, null);
                }
                return damage;
            }
            else if (_DamageType == DamageTypes.Magic)
            {
                int damage;
                damage = baseDamage + (intelligence * 2);

                if (crit <= currentCritChance)
                {
                    damage *= 2;
                    Instantiate(critPrefab, transform.position, Quaternion.identity, null);
                }
                return damage;
            }
            return 0;
    }

    public void TakeDamage(int incomingDamage,int enemyLuck)
    {
        if (IsPlayer)
        {
            currentHealth -= (incomingDamage - ((resistance * 2)+ GetComponent<CharacterInventoryScript>().EquipmentStorage[0].armorRating+GetComponent<CharacterInventoryScript>().EquipmentStorage[1].armorRating+GetComponent<CharacterInventoryScript>().EquipmentStorage[2].armorRating));
        }
        else if(!IsPlayer)
        {
            currentHealth -= (incomingDamage - (resistance * 2));
        }
        
    }

    public void Healed(int incomingHeal)
    {
            currentHealth += incomingHeal + (intelligence * 2);
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
            print(intelligence);
        }
    }

    public void AddStrength()
    {
        if (attributePoints > 0)
        {
            attributePoints -= 1;
            strength += 1;
        }
    }

    public void AddDexterity()
    {
        if (attributePoints > 0)
        {
            attributePoints -= 1;
            dexterity += 1;
        }
    }

    public void AddVitality()
    {
        if (attributePoints > 0)
        {
            attributePoints -= 1;
            vitality += 1;
        }
    }

    public void AddResistance()
    {
        if (attributePoints > 0)
        {
            attributePoints -= 1;
            resistance += 1;
        }
    }

    public void AddLuck()
    {
        if (attributePoints > 0)
        {
            attributePoints -= 1;
            luck += 1;
        }
    }

    public void AddAgility()
    {
        if (attributePoints > 0)
        {
            attributePoints -= 1;
            agility += 1;
        }
    }
    

    public void Update()
    {
        
            regenTimer -= Time.deltaTime;
            maxHealth = baseHealth * currentLevel + (vitality * 25);
            maxMana = baseMana * currentLevel + (intelligence * 15);
            currentCritChance = baseCritChance + Mathf.RoundToInt(luck * 0.25f);
            manaRegen = baseManaRegen * currentLevel + (intelligence * 2);
            healthRegen = baseHealthRegen * currentLevel + (vitality * 2);

            if (leveledUp)
            {
                currentHealth = maxHealth;
                currentMana = maxMana;

                leveledUp = false;
            }

            if (regenTimer <= 0)
            {
                currentHealth += healthRegen;
                currentMana += manaRegen;
                regenTimer = 1;
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
                if (Input.GetKeyDown(KeyCode.K))
                {
                    GainXP(35);
                }

                if (Input.GetKeyDown(KeyCode.L))
                {
                    TakeDamage(15,0);
                }
                if (Input.GetKeyDown(KeyCode.O))
                {
                    currentMana -= 75;
                }
                if (Input.GetKeyDown(KeyCode.P))
                {
                    attributePoints += 15;
                }

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
