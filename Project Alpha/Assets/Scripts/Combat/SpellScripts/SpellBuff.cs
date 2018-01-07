using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBuff : MonoBehaviour {

    public enum BuffType
    {
        hot, //Heal Over Time
        dot, //Damage Over Time
        speed, //Character Speed Modifier
        stat, //Stat Modifier
        resistance //Elemental Resistance Modifier
    }

    public enum StatBuffType
    {
        vitality,
        intelligence,
        strength,
        luck,
        dexterity,
        resistance
    }

    public BuffType buffType;
    public StatBuffType statBuffType;

    public GameObject deathEffect;

    public float duration,
        effect,
        tickTimer;

    float time;

    CharacterStatsScript stats;

    GameObject temp;
	// Use this for initialization
	void Start () {
        stats = transform.parent.GetComponent<CharacterStatsScript>();

        if(buffType == BuffType.speed)
        {
            stats.speed += effect;
            tickTimer = 100;
        }
        else if(buffType == BuffType.stat)
        {
            if(statBuffType == StatBuffType.dexterity)
            {
                stats.dexterity += (int)effect;
            }
            if (statBuffType == StatBuffType.intelligence)
            {
                stats.intelligence += (int)effect;
            }
            if (statBuffType == StatBuffType.luck)
            {
                stats.luck += (int)effect;
            }
            if (statBuffType == StatBuffType.resistance)
            {
                stats.resistance += (int)effect;
            }
            if (statBuffType == StatBuffType.strength)
            {
                stats.strength += (int)effect;
            }
            if (statBuffType == StatBuffType.vitality)
            {
                stats.vitality += (int)effect;
            }
            tickTimer = 100;
        }
        else if (buffType == BuffType.resistance)
        {

        }
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        tickTimer -= Time.deltaTime;

        if (time >= duration)
        {
            if(duration ==0)
            {
                switch ((int)buffType)
                {
                    case 0:
                        stats.Healed(Mathf.RoundToInt(effect));
                        print("Healed For " + Mathf.RoundToInt(effect) + " HP!");
                        temp = Instantiate((GameObject)Resources.Load("Misc/TextPrefab"),transform.parent);
                        temp.transform.localPosition = new Vector3(0, 0, 0);
                        temp.GetComponent<TextPrefabScript>().Initialize(Mathf.RoundToInt(effect).ToString(), Color.green);
                        break;
                    case 1:
                        stats.TakeDamage(Mathf.RoundToInt(effect));
                        print("Dealt " + Mathf.RoundToInt(effect) + " Damage!");
                        temp = Instantiate((GameObject)Resources.Load("Misc/TextPrefab"), transform.parent);
                        temp.transform.localPosition = new Vector3(0, 0, 0);
                        temp.GetComponent<TextPrefabScript>().Initialize(Mathf.RoundToInt(effect).ToString(), Color.red);
                        break;
                }
                Instantiate(deathEffect, transform.parent.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

            if (buffType == BuffType.speed)
            {
                stats.speed -= effect;
            }
            else if (buffType == BuffType.stat)
            {
                if (statBuffType == StatBuffType.dexterity)
                {
                    stats.dexterity -= (int)effect;
                }
                if (statBuffType == StatBuffType.intelligence)
                {
                    stats.intelligence -= (int)effect;
                }
                if (statBuffType == StatBuffType.luck)
                {
                    stats.luck -= (int)effect;
                }
                if (statBuffType == StatBuffType.resistance)
                {
                    stats.resistance -= (int)effect;
                }
                if (statBuffType == StatBuffType.strength)
                {
                    stats.strength -= (int)effect;
                }
                if (statBuffType == StatBuffType.vitality)
                {
                    stats.vitality -= (int)effect;
                }
            }
            Destroy(gameObject);
        }
        if (tickTimer <= 0 && duration != 0)
        {
            switch ((int)buffType)
            {
                case 0:
                    stats.Healed(Mathf.RoundToInt(effect / duration));
                    print("Healed For " + Mathf.RoundToInt(effect / duration) + " HP!");
                    temp = Instantiate((GameObject)Resources.Load("Misc/TextPrefab"), transform.parent);
                    temp.transform.localPosition = new Vector3(0, 0, 0);
                    temp.GetComponent<TextPrefabScript>().Initialize(Mathf.RoundToInt(effect / duration).ToString(), Color.green);
                    break;
                case 1:
                    stats.TakeDamage(Mathf.RoundToInt(effect / duration));
                    print("Dealt " + Mathf.RoundToInt(effect / duration) + " Damage!");
                    temp = Instantiate((GameObject)Resources.Load("Misc/TextPrefab"), transform.parent);
                    temp.transform.localPosition = new Vector3(0, 0, 0);
                    temp.GetComponent<TextPrefabScript>().Initialize(Mathf.RoundToInt(effect / duration).ToString(), Color.red);
                    break;
            }
            Instantiate(deathEffect, transform.parent.transform.position, Quaternion.identity);
            tickTimer = 1f;
        }
        
	}
}
