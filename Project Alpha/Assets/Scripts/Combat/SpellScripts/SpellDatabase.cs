using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDatabase : MonoBehaviour {

    public List<Spells> masterSpellBook = new List<Spells>();

    public List<Spells> localSpellBook = new List<Spells>();

    float castTime = 0.665f;
    public SpellIcons spellIcons;
    Sprite tempSprite;
    

    public void Start()
    {
        spellIcons = GetComponent<SpellIcons>();

        //ObjectName: BallSpell for spells that move, InvisibleSpell for spells on self
        //Master Spellbook Spellname, SpellId, CastTime, IsLearned, IsProjectile, LevelRequirement, BaseHealing, BaseDamage, Duration, Spelleffect, Icon, Effect Name
        masterSpellBook.Add(new Spells("Fireball", "BallSpell", 0, castTime, true, true, false,false , 1, 10,25, 0, Color.red, SpellEffect.none, SpellStatEffect.none, spellIcons.spellIcons[0], "ExplosionEffect"));
        masterSpellBook.Add(new Spells("Slow", "BallSpell", 1, castTime, true, true, false, false, 2, 15, -50, 5, Color.black, SpellEffect.slow, SpellStatEffect.none, spellIcons.spellIcons[5], "ExplosionEffect"));
        masterSpellBook.Add(new Spells("Poison", "BallSpell", 2, castTime, true, true, false, false, 3, 20,  300, 5, Color.green, SpellEffect.dot, SpellStatEffect.none, spellIcons.spellIcons[5], "ExplosionEffect"));
        masterSpellBook.Add(new Spells("Rejuvenation", "InvisibleSpell", 3, castTime, true, false, true, false, 4,  25,100, 5, Color.white, SpellEffect.hot, SpellStatEffect.none, spellIcons.spellIcons[2], "HealEffect"));
        masterSpellBook.Add(new Spells("ArcaneBolt", "SpearSpell", 4, castTime, true, true, false, false, 5, 30,  50, 0, Color.yellow, SpellEffect.none, SpellStatEffect.none, spellIcons.spellIcons[4], "ExplosionEffect"));
        masterSpellBook.Add(new Spells("ArcaneSpear", "SpearSpell", 5, castTime, true, true, false, false, 6, 15,  25, 0, Color.grey, SpellEffect.none, SpellStatEffect.none, spellIcons.spellIcons[4], "ExplosionEffect"));
        masterSpellBook.Add(new Spells("Defense Up", "InvisibleSpell", 6, castTime, true, false, true, false, 7, 30, 10, 15, Color.white, SpellEffect.stat, SpellStatEffect.resistance, spellIcons.spellIcons[2], "HealEffect"));
        masterSpellBook.Add(new Spells("Illuminate", "Illuminate",7, castTime, true, false, false, false, 8, 15, 0, 15, Color.white, SpellEffect.none, SpellStatEffect.none, spellIcons.spellIcons[3], "HealEffect"));
        masterSpellBook.Add(new Spells("Iceball", "BallSpell", 8, castTime, true, true, false, false, 9, 50, 100, 0, Color.blue, SpellEffect.none, SpellStatEffect.none, spellIcons.spellIcons[1], "ExplosionEffect"));
        masterSpellBook.Add(new Spells("Heal", "InvisibleSpell", 9, castTime, true, false, true, true, 10, 50, 150, 0, Color.white, SpellEffect.hot, SpellStatEffect.none, spellIcons.spellIcons[2], "HealEffect"));

        //Local Spellbook
        //localSpellBook.Add(new Spells("Fireball", 0, 0, false));

    }

    public void Update()
    {
        foreach (Spells spell in masterSpellBook)
        {
            if (spell.isLearned && !localSpellBook.Contains(spell))
            {
                if(spell.levelReq <= GameObject.Find("Player").GetComponent<CharacterStatsScript>().currentLevel)
                {
                    localSpellBook.Add(spell);
                }
            }
        }
        foreach (Spells spell in localSpellBook)
        {
            if(!spell.isLearned && localSpellBook.Contains(spell))
            {
                localSpellBook.Remove(spell);
            }
        }
        
    }

    public Spells ItemToSpellConverter(string Name,int Effect, float Duration, int _SpellEffect, int _SpellStatEffect)
    {
        Spells spell = new Spells();
        spell.objectName = "InvisibleSpell";
        spell.spellName = Name;
        spell.baseDamage = Effect;
        spell.duration = Duration;
        spell.spellEffect = (SpellEffect)_SpellEffect;
        spell.effect = (SpellStatEffect)_SpellStatEffect;
        spell.isItem = true;
        spell.castOnSelf = true;

        return spell;
    }
    
}
