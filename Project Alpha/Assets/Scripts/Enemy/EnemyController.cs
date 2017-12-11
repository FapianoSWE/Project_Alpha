//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EnemyController : MonoBehaviour {


//    public BoxCollider territory;
//    bool playerInTerritory = false;
//    Vector3 spawnPoint;
//    Transform target;
//    bool canSetTarget = true;
//    public float health,
//        maxHealth;
//    public int level;
//    public int xpWorth;
//    public float MeleeAttackCooldownTime;
//    float MeleeAttackCooldown;
//    List<string> attackers = new List<string>();
//    public GameObject MeleeAttackZoneObject,
//        chestObject;
//    public int enemyID;

//	void Start ()
//    {
//        spawnPoint = transform.position;
//        level = Random.Range(1, 100);      
//        xpWorth = 50 * level;
//        MeleeAttackCooldown = MeleeAttackCooldownTime;
//    }

//    void OnCollisionEnter(Collision c)
//    {
//        if(c.gameObject.tag == "Spell")
//        {
//            if(!attackers.Contains(c.gameObject.GetComponent<Fireball>().caster))
//            {
//                attackers.Add(c.gameObject.GetComponent<Fireball>().caster);
//            }
//            Instantiate(c.gameObject.GetComponent<Fireball>().deathEffect, gameObject.transform.position, Quaternion.identity, null);
//            GetComponent<CharacterStatsScript>().TakeDamage(c.gameObject.GetComponent<Fireball>().damage, c.gameObject.GetComponent<Fireball>().casterLuck);
//            if (c.gameObject.GetComponent<Fireball>())

//            Destroy(c.gameObject);
//        }
//        else 
//        {
//        }
//    }

//    void OnTriggerEnter(Collider other)
//    {
//        if(other.tag == "Player")
//        {
//            playerInTerritory = true;

//            if(canSetTarget)
//            {
//                target = other.transform;
//                canSetTarget = false;
//            }
//        }

//        print(other.name);
//        if (other.name == "Heal(Clone)")
//        {
//            print("Enemy Getting Healed...");
//            //GetComponent<CharacterStatsScript>().Healed(Mathf.RoundToInt((other.transform.GetComponent<HealSpellScript>().baseHeal) / (other.gameObject.transform.localScale.x)));
//        }

//    }

//    void OnTriggerExit(Collider other)
//    {
//        if(other.tag == "Player")
//        {
//            playerInTerritory = false;
//        }
//    }

//   public void Death()
//    {
        
                
//                    GameObject.Find("Player").GetComponent<CharacterStatsScript>().GainXP(xpWorth);
//                    //c.GetComponent<PlayerQuestScript>().EnemyKilled(enemyID);
               
//        /*GameObject temp = Instantiate(chestObject, transform.position, Quaternion.identity, null);
//        temp.GetComponent<CharacterInventoryScript>().Initialize();
//        temp.GetComponent<CharacterInventoryScript>().SetInvenoryItem(0, 4, 25);
//        temp.GetComponent<CharacterInventoryScript>().SetInvenoryItem(1, 2);
//        temp.GetComponent<CharacterInventoryScript>().SetInvenoryItem(2, 6);
//        int crit = Random.Range(0, 100);
//        if (crit < GameObject.Find("me").GetComponent<CharacterStatsScript>().currentCritChance)
//        {
//            temp.GetComponent<CharacterInventoryScript>().SetInvenoryItem(3, 0);
//        }*/

//        Destroy(gameObject);
//    }


//	void Update ()
//    {
//        MeleeAttackCooldown -= Time.deltaTime;
//        if (playerInTerritory)
//        {
//            transform.LookAt(target);
//            transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
//            Vector3.MoveTowards(transform.position, target.position, 5);
//            if(MeleeAttackCooldown <= 0 && Vector3.Distance(transform.position,target.position) <= 10)
//            {
//                MeleeAttackZoneObject.SetActive(true);
//                MeleeAttackCooldown = MeleeAttackCooldownTime;
//            }
//            Vector3 temp;
//            temp = Vector3.Lerp(transform.position, target.transform.position, 0.01f);
//            transform.position = new Vector3(temp.x,transform.position.y,temp.z);
//        }
//        else
//        {
//            transform.LookAt(spawnPoint);
//            Vector3.MoveTowards(transform.position, spawnPoint, 5);
//        }

//        if(GetComponent<CharacterStatsScript>().currentHealth <= 0)
//        {
//            Death();
//        }
//        health = GetComponent<CharacterStatsScript>().currentHealth;


//	}
//    public void OnMeleeHit(GameObject other)
//    {

//        other.gameObject.GetComponent<AttackZoneScript>().timer = 0;
//        GetComponent<CharacterStatsScript>().TakeDamage(other.transform.parent.GetComponent<CharacterStatsScript>().strength * 5, other.transform.parent.GetComponent<CharacterStatsScript>().luck);
//        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player"))
//        {
//                print("Melee Hit!");
//                break;
            
//        }
//    }
//}
