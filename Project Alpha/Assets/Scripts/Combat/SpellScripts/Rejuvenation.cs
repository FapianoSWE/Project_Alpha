using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rejuvenation : MonoBehaviour {


    bool canSetHealing = true;
    float duration;
    float tickTimer = 1f;
    float tickHeal;
    CharacterStatsScript characterStats;
    PlayerController playerController;
    float damage;

    void Start () {
        characterStats = GameObject.Find("Player").GetComponent<CharacterStatsScript>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        duration = transform.parent.GetComponent<SpellBase>().thisSpell.duration;
        damage = transform.parent.GetComponent<SpellBase>().damage / duration;
	}


    public void SpellUpdate()
    {

        tickTimer -= Time.deltaTime;
        duration -= Time.deltaTime;

        if (canSetHealing)
        {
            tickHeal = damage;
            canSetHealing = false;
        }


        if (tickTimer <= 0)
        {
            characterStats.currentHealth += (int)tickHeal;
            print("Healed Player For " + tickHeal + " HP!");
            Instantiate(transform.parent.GetComponent<SpellBase>().deathEffect, GameObject.Find("Player").transform.position, Quaternion.identity);
            tickTimer = 1f;
        }
        if (duration <= 0)
        {
            Destroy(this);
        }
    }
}
