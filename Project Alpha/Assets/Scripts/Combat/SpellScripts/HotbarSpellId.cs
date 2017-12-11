using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarSpellId : MonoBehaviour {

    public int spellId;
    public Spells currentSpell;
    public SpellDatabase spellDatabase;
    public PlayerController playercontroller;
    public bool changed = true;
    public Image spellImage;

    public void Start()
    {
        spellDatabase = GameObject.Find("SpellObjects").GetComponent<SpellDatabase>();
        playercontroller = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void Update()
    {
        
        currentSpell = playercontroller.findSpell(spellId);
        if(currentSpell != null)
        {
            spellImage.sprite = currentSpell.icon;
        }
        else if(currentSpell == null)
        {
            spellImage.sprite = GameObject.Find("SpellObjects").GetComponent<SpellIcons>().spellIcons[10];
        }
        
        
    }
}
