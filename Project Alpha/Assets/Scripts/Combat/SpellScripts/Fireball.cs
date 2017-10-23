using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

    float lifetime;
    public float lifetimeLength;

    public int baseDamage,
    damage,
    casterLuck;

    public GameObject deathEffect;

    public string caster;
    bool canSetDamage;

    public bool enemyCaster = false;

	void Start ()
    {
        canSetDamage = true;
	}

	void Update ()
    {
        lifetime += Time.deltaTime;

        if(lifetime >= lifetimeLength)
        {
            Destroy(gameObject);
        }

        if(canSetDamage)
        {            
            if(GameObject.Find("Player"))
            { 
                    baseDamage = 25 * GameObject.Find("Player").GetComponent<CharacterStatsScript>().currentLevel;
                    damage = GameObject.Find("Player").GetComponent<CharacterStatsScript>().DealDamage(baseDamage, CharacterStatsScript.DamageTypes.Magic);
                    canSetDamage = false;
             }            
        }
    }

    public void OnHit(GameObject g)
    {

    }
}
