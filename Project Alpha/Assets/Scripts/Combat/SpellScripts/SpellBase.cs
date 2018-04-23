using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpellBase : MonoBehaviour {
    public Spells thisSpell;
    float lifetime;
    public float lifetimeLength;

    public float baseDamage,
    damage;

    public GameObject deathEffect,
        spellEffect;

    bool canSetDamage;

    public bool enemyCaster = false;

    public float speed;

    public GameObject caster;

    Vector3 forward;

    public int SpellId;

    bool hasEffect;

    public bool isItem;
    //For items

    void Start()
    {

        if(thisSpell != null)
        {
            if(thisSpell.isItem)
                isItem = true;
        }
            

        if (spellEffect == null)
        {
            //Debug
            spellEffect = (GameObject)Resources.Load("Spells/SpellEffect");
        }

        lifetimeLength = 5;

        if(!isItem)
        {
            thisSpell = GameObject.Find("SpellObjects").GetComponent<SpellDatabase>().masterSpellBook[SpellId];
        }


        canSetDamage = true;


        if (thisSpell.isProjectile)
            forward = new Vector3(transform.forward.x * 50, 0, transform.forward.z * 50);

        deathEffect = (GameObject)Resources.Load("Effects/" + thisSpell.particleName);       

        if (thisSpell.effect == SpellStatEffect.none && thisSpell.spellEffect != SpellEffect.slow && !isItem)
        {           
            damage = caster.GetComponent<CharacterStatsScript>().DealDamage(thisSpell.baseDamage, CharacterStatsScript.DamageTypes.Magic);
        }
        else
        damage = thisSpell.baseDamage;

        if (transform.Find("SpellEffect"))
        {
            hasEffect = true;
        }

        if (thisSpell.castOnSelf)
        {
            GameObject temp = Instantiate(spellEffect, caster.transform);
            SpellBuff s = temp.GetComponent<SpellBuff>();
            s.duration = thisSpell.duration;
            s.effect = damage;
            s.buffType = (SpellBuff.BuffType)thisSpell.spellEffect - 1;
            s.statBuffType = (SpellBuff.StatBuffType)thisSpell.effect - 1;
            s.deathEffect = deathEffect;
            Destroy(gameObject);
        }

        if(GetComponent<MeshRenderer>())
        GetComponent<MeshRenderer>().material.SetColor("_Color",thisSpell.color);

    }

    void Update()
    {
        lifetime += Time.deltaTime;

        if (thisSpell.spellEffect != SpellEffect.none &&lifetime > lifetimeLength)
        {
            Destroy(gameObject);
        }

        if(thisSpell.isProjectile)
        {
            transform.position += forward * Time.deltaTime;

            Ray ray = new Ray(transform.position, transform.up * -1);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10))
            {
                if (hit.distance != 1)
                {
                    transform.position = new Vector3(transform.position.x, hit.point.y + 1, transform.position.z);
                }
            }
        }

        if (hasEffect)
        {
            gameObject.SendMessage("SpellUpdate");
        }

    }

    public void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.name == "Player" && c.gameObject != gameObject && enemyCaster || c.gameObject.tag == "Enemy" && c.gameObject != gameObject && !enemyCaster)
        {
            if(thisSpell.spellEffect == 0)
            {
                c.gameObject.SendMessage("TakeDamage", damage);
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(this.gameObject);              
            }
            else
            {
                GameObject temp = Instantiate(spellEffect, c.gameObject.transform);
                SpellBuff s = temp.GetComponent<SpellBuff>();
                s.duration = thisSpell.duration;
                s.effect = damage;
                s.buffType = (SpellBuff.BuffType)thisSpell.spellEffect - 1;
                s.deathEffect = deathEffect;
                s.statBuffType = (SpellBuff.StatBuffType)thisSpell.effect - 1;
                Destroy(gameObject);
            }
            FindObjectOfType<AudioPlayerScript>().PlayAudio("Explosion", GameObject.Find("Player").transform.position, true);

        }
        /*if (c.gameObject.tag == "Enemy" && c.gameObject != gameObject && !enemyCaster)
        {
    
            if (thisSpell.spellEffect == 0)
            {
                c.gameObject.SendMessage("TakeDamage", damage);
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
            else
            {
                
                GameObject temp = Instantiate(spellEffect, c.gameObject.transform);
                SpellBuff s = temp.GetComponent<SpellBuff>();
                s.duration = thisSpell.duration;
                s.effect = damage;
                s.buffType = (SpellBuff.BuffType)thisSpell.spellEffect - 1;
                s.deathEffect = deathEffect;
                s.statBuffType = (SpellBuff.StatBuffType)thisSpell.effect - 1;
                Destroy(gameObject);
            }
        }*/
    }


}
