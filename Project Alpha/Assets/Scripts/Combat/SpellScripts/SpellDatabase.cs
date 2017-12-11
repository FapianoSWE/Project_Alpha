using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDatabase : MonoBehaviour {

    public List<Spells> masterSpellBook = new List<Spells>();

    public List<Spells> localSpellBook = new List<Spells>();
    float castTime = 0.665f;
    public SpellIcons spellIcons;
    Sprite tempSprite;
    public void Awake()
    {

        foreach (Spells spell in masterSpellBook)
        {
            if (spell.isLearned && !localSpellBook.Contains(spell))
            {
                if (spell.levelReq <= GameObject.Find("Player").GetComponent<CharacterStatsScript>().currentLevel)
                {
                    localSpellBook.Add(spell);
                }
            }
        }
    }

    public void Start()
    {
        spellIcons = GetComponent<SpellIcons>();
        //Master Spellbook Spellname, SpellId, CastTime, IsLearned, IsProjectile, LevelRequirement, BaseHealing, BaseDamage, Duration, Spelleffect, Icon
        masterSpellBook.Add(new Spells("Fireball", 0, castTime, true, true, 1, 0, 0, 50, 0, SpellEffect.none, spellIcons.spellIcons[0]));
        masterSpellBook.Add(new Spells("Darkball", 1, castTime, true, true, 2, 0, 0, 50, 0, SpellEffect.none, spellIcons.spellIcons[1]));
        masterSpellBook.Add(new Spells("FrostBall", 2, castTime, true, true, 3, 0, 0, 50, 0, SpellEffect.none, spellIcons.spellIcons[9]));
        masterSpellBook.Add(new Spells("Rejuvenation", 3, castTime, true, false, 4, 0, 5, 0, 15, SpellEffect.hot, spellIcons.spellIcons[9]));
        masterSpellBook.Add(new Spells("ArcaneBolt", 4, castTime, true, true, 5, 0, 0, 50, 0, SpellEffect.none, spellIcons.spellIcons[9]));
        masterSpellBook.Add(new Spells("ArcaneSpear", 5, castTime, true, true, 6, 0, 0, 25, 0, SpellEffect.none, spellIcons.spellIcons[9]));
        masterSpellBook.Add(new Spells("DefenseUp", 6, castTime, true, false, 7, 0, 0, 0, 15, SpellEffect.none, spellIcons.spellIcons[9]));



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
    
}
