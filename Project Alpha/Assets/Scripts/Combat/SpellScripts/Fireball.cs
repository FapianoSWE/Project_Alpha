using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball :  MonoBehaviour {

    float lifetime;
    public float lifetimeLength;

    public float baseDamage,
    damage;

    public GameObject deathEffect;

    bool canSetDamage;

    public bool enemyCaster = false;

    public float speed;

    public GameObject caster;

    Vector3 forward;

	void Start ()
    {
        canSetDamage = true;

        forward = new Vector3(transform.forward.x * 3, 0, transform.forward.z * 3);

    }

	void Update ()
    {
        lifetime += Time.deltaTime;

        if(lifetime >= lifetimeLength)
        {
            Destroy(gameObject);
        }

        if(canSetDamage && caster != null)
        {            
            
            baseDamage = Random.Range(baseDamage * caster.GetComponent<CharacterStatsScript>().currentLevel - 5,
            baseDamage * caster.GetComponent<CharacterStatsScript>().currentLevel + 5);
            damage = caster.GetComponent<CharacterStatsScript>().DealDamage(baseDamage, CharacterStatsScript.DamageTypes.Magic);
            canSetDamage = false;
                    
        }

        transform.position += forward;


        Ray ray = new Ray(transform.position,transform.up*-1);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,10))
        {
            if(hit.distance != 1)
            {
                transform.position = new Vector3(transform.position.x, hit.point.y + 1,transform.position.z);
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
