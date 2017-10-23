using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDatabase : MonoBehaviour {

    public List<Spells> masterSpellBook = new List<Spells>();

    public List<Spells> localSpellBook = new List<Spells>();
    public Sprite tempSprite;
    float castTime = 1.33f;

    public void Start()
    {

        tempSprite = Resources.Load("Spells/Icons/FidgetSpinner") as Sprite;
        //Master Spellbook
        masterSpellBook.Add(new Spells("Fireball", 0, castTime, true, true, 1, 50, 0, 50, 0, SpellEffect.none, Resources.Load("Spells/Icons/Fireball") as Sprite));
        masterSpellBook.Add(new Spells("Darkball", 1, castTime, true, true, 2, 50, 0, 50, 0, SpellEffect.none, Resources.Load("Spell/Icon/Darkball") as Sprite));
        masterSpellBook.Add(new Spells("Illuminate", 2, castTime, true, false, 3, 50, 0, 50, 5, SpellEffect.none,  tempSprite));
        masterSpellBook.Add(new Spells("FrostBall", 3, castTime, true, true, 4, 50, 0, 50, 0, SpellEffect.none, tempSprite));
        masterSpellBook.Add(new Spells("Rejuvenation", 4, castTime, true, false, 1, 50, 5, 0, 15, SpellEffect.hot, tempSprite));



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
