using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIScript : MonoBehaviour
{
    bool removeEnemyfromList = false;
    public AreaListCheckScript areaListCheckScript;
    public BoxCollider territory;
    bool playerInTerritory = false;
    Vector3 spawnPoint;
    public Transform target;
    bool canSetTarget = true;
    public float health,
        maxHealth;
    public int level;
    public int xpWorth;
    public float MeleeAttackCooldownTime,
        SideStepCoolDownTime,
        BackStepCoolDownTime,
        MagicCoolDownTime;
    float MeleeAttackCooldown,
        SideStepCooldown,
        BackStepCooldown,
        MagicCooldown;
    List<string> attackers = new List<string>();
    public GameObject MeleeAttackZoneObject,
        chestObject;
    public int enemyID;
    public Animator animator;
    public bool CastSpellNow = false,
        hasCasted = false,
        ObstacleFound;
    public string SpellToCast;
    public Collider ownCollider,
        playerCollider;
    public CharacterController playerController;
    public float aggroTimeMax;
    float aggroTime;
    public bool isAggrod,
        returnToSpawn;
    public GameObject spawnPositionObject;
    public float distAllowedFromSpawn;

    
    [Tooltip("X = Item Id, Y = Item Amount Z = Item Drop Chance(0-100%)")]
    public List<Vector3> itemDrops = new List<Vector3>();

    public enum EnemyAIType
    {
        Melee,
        Magic
    }
    public EnemyAIType enemyAIType;
    void Start()
    {
        playerCollider = GameObject.Find("Player").GetComponent<BoxCollider>();
        playerController = GameObject.Find("Player").GetComponent<CharacterController>();
        spawnPoint = transform.position;
        level = Random.Range(1, 100);
        xpWorth = 50;
        MeleeAttackCooldown = MeleeAttackCooldownTime;
        animator.Play("EnemyIdleAnim");

        areaListCheckScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AreaListCheckScript>();
        spawnPositionObject = transform.Find("SpawnPosition").transform.gameObject;
        spawnPositionObject.transform.parent = null;
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Spell" && c.gameObject.GetComponent<Fireball>().caster != gameObject.name && !c.gameObject.GetComponent<Fireball>().enemyCaster)
        {
            if (!attackers.Contains(c.gameObject.GetComponent<Fireball>().caster))
            {
                attackers.Add(c.gameObject.GetComponent<Fireball>().caster);
            }

            target = GameObject.Find("Player").transform;

            if(playerInTerritory == false)
            {
                isAggrod = true;
                aggroTime = aggroTimeMax;
            }

            Instantiate(c.gameObject.GetComponent<Fireball>().deathEffect, gameObject.transform.position, Quaternion.identity, null);
            GetComponent<CharacterStatsScript>().TakeDamage(c.gameObject.GetComponent<Fireball>().damage, c.gameObject.GetComponent<Fireball>().casterLuck);
            if (c.gameObject.GetComponent<Fireball>())
                c.gameObject.GetComponent<Fireball>().OnHit(gameObject);

            Destroy(c.gameObject);

            GetComponent<Rigidbody>().velocity = Vector3.zero;

            returnToSpawn = false;
        }
        else
        {
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyMoveAnim") || animator.GetCurrentAnimatorStateInfo(0).IsName("EnemySideStepAnim")|| animator.GetCurrentAnimatorStateInfo(0).IsName("EnemySideStepAnim2"))
           {
                /*animator.Play("EnemyIdleAnim");
                ObstacleFound = true;*/
            }
            else
            {
                ObstacleFound = false;
            }
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInTerritory = true;

            if (canSetTarget)
            {
                target = other.transform;
                canSetTarget = false;
            }

            if (isAggrod)
                isAggrod = false;
        }


        if (other.name == "Heal(Clone)")
        {
            print("Enemy Getting Healed...");
            //GetComponent<CharacterStatsScript>().Healed(Mathf.RoundToInt((other.transform.GetComponent<HealSpellScript>().baseHeal) / (other.gameObject.transform.localScale.x)));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInTerritory = false;
            print("Player left territory");
        }
    }

    public void Death()
    {
        
        GameObject.Find("Player").GetComponent<CharacterStatsScript>().GainXP(50);
        foreach(QuestManagerScript.Quest q in GameObject.Find("Player").GetComponent<PlayerQuestScript>().TakenQuests)
        {
            if(q.questType == QuestManagerScript.Quest.QuestType.kill)
            {
                q.currentAmount++;
            }
        }
        
        bool removedEnemyfromList = true;

        for (int i = 0; i < areaListCheckScript.zoneList.Count; i++)
        {
            for (int j = 0; j < areaListCheckScript.zoneList[i].GetComponent<DetectingScript>().objects.Count; j++)
            {
                if(areaListCheckScript.zoneList[i].GetComponent<DetectingScript>().objects[j] == this.gameObject)
                {
                    areaListCheckScript.zoneList[i].GetComponent<DetectingScript>().objects.RemoveAt(j);
                    removedEnemyfromList = true;
                    break;
                }
        
            }
        }             

        GameObject temp = Instantiate(chestObject, transform.position, Quaternion.identity, null);
        temp.GetComponent<CharacterInventoryScript>().Initialize();
        int x = 0;
        bool hasItems = false;
        for (int i = 0; i < itemDrops.Count; i++)
        {            
            if(itemDrops[i].z > Random.Range(0,100))
            {
            temp.GetComponent<CharacterInventoryScript>().SetInvenoryItem(x, Mathf.RoundToInt(itemDrops[i].x), Mathf.RoundToInt(itemDrops[i].y));
            hasItems = true;
            x++;
            }
            
            
        }

        if(hasItems == false)
        {
            Destroy(temp);
        }

        Destroy(spawnPositionObject);
        if(removedEnemyfromList)
            Destroy(gameObject);

        
    }


    void Update()
    {
        Physics.IgnoreCollision(ownCollider, playerCollider);
        Physics.IgnoreCollision(ownCollider, playerController);
        MeleeAttackCooldown -= Time.deltaTime;
        SideStepCooldown -= Time.deltaTime;
        BackStepCooldown -= Time.deltaTime;
        MagicCooldown -= Time.deltaTime;
        aggroTime -= Time.deltaTime;

        if(aggroTime <= 0)
        {
            aggroTime = aggroTimeMax;
            isAggrod = false;
        }

        if (Vector3.Distance(transform.position, spawnPositionObject.transform.position) > distAllowedFromSpawn)
        {
            returnToSpawn = true;
        }
        else if (Vector3.Distance(transform.position, spawnPositionObject.transform.position) < 10)
            returnToSpawn = false;

        if (returnToSpawn)
            target = spawnPositionObject.transform;

        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, 3))
        {
            if (hit.transform.gameObject.name != "Player")
            {
                //ObstacleFound = true;

            }
            else
                ObstacleFound = false;
        }
        else
        {
            ObstacleFound = false;
        }

        if (enemyAIType == EnemyAIType.Melee)
        { 
            if (playerInTerritory||isAggrod || returnToSpawn)
            {
                animator.SetBool("InTerritory", true);
                transform.LookAt(target);
                transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
                if (BackStepCooldown <= 0 && Vector3.Distance(transform.position, target.position) >= 0 && Vector3.Distance(transform.position, target.position) <= 4 && Random.Range(0, 2) >= 0.1f)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyIdleAnim") || animator.GetCurrentAnimatorStateInfo(0).IsName("InTerritory"))
                    {
                        animator.Play("EnemyBackStepAnim");
                        BackStepCooldown = BackStepCoolDownTime;
                    }
                }
                else if (MeleeAttackCooldown <= 0 && Vector3.Distance(transform.position, target.position) <= 3 && Random.Range(0, 2) >= 0.1f)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyIdleAnim") || animator.GetCurrentAnimatorStateInfo(0).IsName("InTerritory"))
                    {

                        animator.Play("EnemyAttackAnim");
                        MeleeAttackCooldown = MeleeAttackCooldownTime;


                    }
                }
                else if (MeleeAttackCooldown <= 0 && Vector3.Distance(transform.position, target.position) >= 3 && Vector3.Distance(transform.position, target.position) <= 8 && Random.Range(0, 2) >= 0.1f && !ObstacleFound)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyIdleAnim") || animator.GetCurrentAnimatorStateInfo(0).IsName("InTerritory"))
                    {
                        animator.Play("EnemyAttackMoveForward");
                        MeleeAttackCooldown = MeleeAttackCooldownTime;
                    }


                }
                else if (SideStepCooldown <= 0 && Vector3.Distance(transform.position, target.position) >= 0 && Vector3.Distance(transform.position, target.position) <= 6 && Random.Range(0, 2) >= 0.1f &&!ObstacleFound)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyIdleAnim") || animator.GetCurrentAnimatorStateInfo(0).IsName("InTerritory"))
                    {
                        if (Random.Range(0, 2) > 0.5f)
                            animator.Play("EnemySideStepAnim");
                        else
                            animator.Play("EnemySideStepAnim2");
                        SideStepCooldown = SideStepCoolDownTime;
                    }
                }
                else if (Vector3.Distance(transform.position, target.position) >= 8 && Random.Range(0, 2) >= 0.1f && !ObstacleFound)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyIdleAnim") || animator.GetCurrentAnimatorStateInfo(0).IsName("InTerritory"))
                        animator.Play("EnemyMoveAnim");
                }

            }
            else
            {
                animator.SetBool("InTerritory", false);
                transform.LookAt(spawnPoint);
            }
    }
        else if(enemyAIType == EnemyAIType.Magic)
        {   
            if (playerInTerritory || isAggrod||returnToSpawn)
            {
                animator.SetBool("InTerritory", true);
                transform.LookAt(target);
                transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);

                if (MagicCooldown <= 0 && Vector3.Distance(transform.position, target.position) >= 5 && Random.Range(0, 2) >= 0.1f)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyIdleAnim") || animator.GetCurrentAnimatorStateInfo(0).IsName("InTerritory"))
                    {
                        SpellToCast = "Fireball";
                        animator.Play("EnemySpellCastAnim");
                        MagicCooldown = MagicCoolDownTime;
                    }
                }
                else if (MagicCooldown <= 0 && Vector3.Distance(transform.position, target.position) <= 5 && Vector3.Distance(transform.position, target.position) >= 0 && Random.Range(0, 2) >= 0.1f)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyIdleAnim") || animator.GetCurrentAnimatorStateInfo(0).IsName("InTerritory"))
                    {
                        SpellToCast = "PushSpell";
                        animator.Play("EnemySpellCastAnim");
                        MagicCooldown = MagicCoolDownTime;
                    }
                }
                else if (BackStepCooldown <= 0 && Vector3.Distance(transform.position, target.position) >= 0 && Vector3.Distance(transform.position,target.position) <= 5 && Random.Range(0, 2) >= 0.1f)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyIdleAnim") || animator.GetCurrentAnimatorStateInfo(0).IsName("InTerritory"))
                    {
                        animator.Play("EnemyBackStepAnim");
                        BackStepCooldown = BackStepCoolDownTime;
                    }
                }

            }
            else
            {
                animator.SetBool("InTerritory", false);
                transform.LookAt(spawnPoint);
            }
        }

        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
        if (GetComponent<CharacterStatsScript>().currentHealth <= 0)
        {
            Death();
        }
        health = GetComponent<CharacterStatsScript>().currentHealth;
        
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyBackStepAnim"))
        {
            GetComponent<Rigidbody>().velocity = transform.forward * -5;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyAttackMoveForward"))
        {
            GetComponent<Rigidbody>().velocity = transform.forward * 5;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyMoveAnim"))
        {
            GetComponent<Rigidbody>().velocity = transform.forward * 10;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("EnemySideStepAnim"))
        {
            GetComponent<Rigidbody>().velocity = transform.right * 5;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("EnemySideStepAnim2"))
        {
            GetComponent<Rigidbody>().velocity = transform.right * -5;
        }
        else
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        
        //GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, -10, GetComponent<Rigidbody>().velocity.z);
        ray.direction = new Vector3(0, -1, 0);
        if (Physics.Raycast(ray, out hit, 300))
        {
            if(hit.transform.gameObject.name != "Player")
            {             
                GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, -hit.distance * 0.5f, GetComponent<Rigidbody>().velocity.z);
            }
        }
    }
    public void OnMeleeHit(GameObject other)
    {
        if (target == null)
            target = other.gameObject.transform.parent.transform;

        if (target.GetComponent<EnemyAIScript>())
            return;

        other.gameObject.GetComponent<AttackZoneScript>().timer = 0;
        GetComponent<CharacterStatsScript>().TakeDamage(other.transform.parent.GetComponent<CharacterStatsScript>().DealDamage(other.transform.parent.GetComponent<CharacterStatsScript>().strength,CharacterStatsScript.DamageTypes.Melee), other.transform.parent.GetComponent<CharacterStatsScript>().luck);
        print("Melee Hit!");
        animator.Play("EnemyBackStepAnim");
        BackStepCooldown = BackStepCoolDownTime;
        returnToSpawn = false;

    }
    public void CastSpell()
    {

        
        if(SpellToCast == "Fireball")
        {
            GameObject temp;
            temp = Instantiate(Resources.Load("Spells/Fireball") as GameObject, transform.position + transform.forward * 2, Quaternion.identity);
            temp.GetComponent<Fireball>().caster = gameObject.name;
            temp.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 5000);
            temp.GetComponent<Fireball>().casterLuck = GetComponent<CharacterStatsScript>().luck;
            temp.GetComponent<Fireball>().enemyCaster = true;
        }
        else if(SpellToCast == "PushSpell")
        {
            GameObject temp;
            temp = Instantiate(Resources.Load("Spells/PushSpell") as GameObject, transform.position + new Vector3(0,1,0), Quaternion.identity);
            temp.GetComponent<PushSpellScript>().CastedFromEnemy = true;
        }

        MagicCooldown = MagicCoolDownTime;
    }
}
