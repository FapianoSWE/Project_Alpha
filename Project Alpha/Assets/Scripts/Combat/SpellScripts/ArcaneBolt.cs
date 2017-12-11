using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneBolt : MonoBehaviour {

    float lifetime;
    public float lifetimeLength;

    public float baseDamage,
    damage;

    public GameObject deathEffect;
    
    bool canSetDamage;

    public bool enemyCaster = false;

    public GameObject caster;
    

    void Start()
    {
        canSetDamage = true;
    }

    void Update()
    {
        lifetime += Time.deltaTime;

        if (lifetime >= lifetimeLength)
        {
            Destroy(gameObject);
        }

        if (canSetDamage)
        {
            if (GameObject.Find("Player"))
            {
                baseDamage = Random.Range(baseDamage * caster.GetComponent<CharacterStatsScript>().currentLevel - 5,
                baseDamage * caster.GetComponent<CharacterStatsScript>().currentLevel + 5);
                damage = caster.GetComponent<CharacterStatsScript>().DealDamage(baseDamage, CharacterStatsScript.DamageTypes.Magic);
                canSetDamage = false;
            }
        }
    }

    public void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.name == "Player" && c.gameObject != gameObject && enemyCaster)
        {
            c.gameObject.SendMessage("TakeDamage", damage);
            Destroy(this.gameObject);
        }
        if (c.gameObject.tag == "Enemy" && c.gameObject != gameObject && !enemyCaster)
        {
            c.gameObject.SendMessage("TakeDamage", damage);
            Destroy(this.gameObject);
        }
    }
}
