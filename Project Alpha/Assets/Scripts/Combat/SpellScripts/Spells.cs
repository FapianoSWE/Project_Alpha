using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpellEffect
{
    none,
    slow,
    dot,
    hot
}

public class Spells {

    public string spellName;

    public int id;
    public Sprite icon;
    public float castTime;
    public int manacost;
    public GameObject worldObject;
    public float baseDamage;
    public float baseHealing;
    public bool isLearned;
    public SpellEffect spellEffect;
    public bool isProjectile;
    public int levelReq;
    public float duration;
    public Spells(string Name, int ID, float CastTime, bool IsLearned, bool IsProjectile, int LevelReq, int Manacost, float BaseHealing, float BaseDamage, float Duration, SpellEffect Effect, Sprite Icon)
    {
        id = ID;
        castTime = CastTime;
        isLearned = IsLearned;
        spellName = Name;
        icon = Icon;
        manacost = Manacost;
        baseDamage = BaseDamage;
        baseHealing = BaseHealing;
        spellEffect = Effect;
        isProjectile = IsProjectile;
        levelReq = LevelReq;
        duration = Duration;
    }

    public Spells()
    {
        id = 0;
    }
    
    
}
