using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpellEffect
{
    none,
    hot,
    dot,
    slow,
    stat

    //HoT/DoT = Heal/Damage over time??
}

public enum SpellStatEffect
{
    none,
    vitality,
    intelligence,
    strength,
    luck,
    dexterity,
    resistance
}

public class Spells {

    public string spellName,
        objectName;

    public int id,
        manacost,
        levelReq;

    public Sprite icon;

    public float castTime,
        baseDamage,
        duration;

    public GameObject worldObject,
        target;

    public bool isLearned,
        isProjectile,
        isHealing,
        castOnSelf,
        isItem;

    public SpellEffect spellEffect;

    public SpellStatEffect effect;

    public string particleName;

    public Color color;


    public Spells(string Name,string ObjectName, int ID, float CastTime, bool IsLearned, bool IsProjectile, bool CastOnSelf,bool IsItem, int LevelReq, int Manacost, float BaseDamage, float Duration,Color Color, SpellEffect Effect, SpellStatEffect Stat, Sprite Icon, string ParticleName)
    {
        id = ID;
        castTime = CastTime;
        isLearned = IsLearned;
        spellName = Name;
        icon = Icon;
        manacost = Manacost;
        baseDamage = BaseDamage;
        spellEffect = Effect;
        isProjectile = IsProjectile;
        levelReq = LevelReq;
        duration = Duration;
        particleName = ParticleName;
        castOnSelf = CastOnSelf;
        objectName = ObjectName;
        effect = Stat;
        color = Color;
        isItem = IsItem;
    }

    public Spells()
    {
        id = 0;
    }
    
    
}
