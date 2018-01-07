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
        maxHealth,
        Speed;
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
    public float aggroTimeMax,
     aggroTime;
    public bool isAggrod,
        returnToSpawn;
    public GameObject spawnPositionObject,
        enemyToSpawn;
    public float distAllowedFromSpawn;

    public GameObject AggroZone;
    
    [Tooltip("X = Item Id, Y = Item Amount Z = Item Drop Chance(0-100%)")]
    public List<Vector3> itemDrops = new List<Vector3>();

    [Tooltip("How much damage within 1 second needed to stagger")]
    public float poise;

    [Range(0, 1)]
    float poiseTimer;

    float poisedamage;

    SpellDatabase spellDatabase;

    GameObject currentSpellObject;

    int id;
    float duration;

    public enum EnemyAIType
    {
        Melee,
        Magic,
        Boss
    }
    public EnemyAIType enemyAIType;
    void Start()
    {
        spellDatabase = GameObject.Find("SpellObjects").GetComponent<SpellDatabase>();
        playerCollider = GameObject.Find("Player").GetComponent<BoxCollider>();
        playerController = GameObject.Find("Player").GetComponent<CharacterController>();
        spawnPoint = transform.position;
        level = Random.Range(1, 100);
        xpWorth = 50;
        MeleeAttackCooldown = MeleeAttackCooldownTime;
        animator.Play("EnemyIdleAnim");

        areaListCheckScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AreaListCheckScript>();
        spawnPositionObject = transform.Find("SpawnPosition").transform.gameObject;
        spawnPositionObject.transform.parent = GameObject.Find("Enemies").GetComponent<Transform>();
    }

    void OnCollisionEnter(Collision c)
    {

        if (c.gameObject.tag == "Spell" && c.gameObject.GetComponent<SpellBase>().caster != gameObject)
        {
            if (!c.gameObject.GetComponent<SpellBase>().enemyCaster)
            {
                GameObject temp = Instantiate(AggroZone, gameObject.transform);
                temp.transform.localPosition = new Vector3(0, 0, 0);

                target = GameObject.Find("Player").transform;

                if (playerInTerritory == false)
                {
                    isAggrod = true;
                    aggroTime = aggroTimeMax;
                }
                

                if (poiseTimer == 0)
                    poiseTimer = 1;

                poisedamage += c.gameObject.GetComponent<SpellBase>().damage - (GetComponent<CharacterStatsScript>().resistance * 2);

                if (poisedamage > poise)
                {
                    animator.Play("EnemyBackStepAnim");
                    poiseTimer = 0;
                    poisedamage = 0;
                    BackStepCooldown = BackStepCoolDownTime;
                }

                if (c.gameObject.GetComponent<Fireball>())
                    Destroy(c.gameObject);

                GetComponent<Rigidbody>().velocity = Vector3.zero;

                returnToSpawn = false;
            }
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
        GameObject.Find("Player").GetComponent<CharacterStatsScript>().GainXP((int)(xpWorth * (0.75f + Random.Range(0,0.5f))));
        foreach(QuestManagerScript.Quest q in GameObject.Find("Player").GetComponent<PlayerQuestScript>().TakenQuests)
        {
            if(q.questType == QuestManagerScript.Quest.QuestType.kill)
            {
                if (q.enemy == enemyID)
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
        poiseTimer -= Time.deltaTime;

        if (poiseTimer < 0)
        {
            poisedamage = 0;
            poiseTimer = 0;
        }
            


        if(aggroTime <= 0)
        {
            aggroTime = aggroTimeMax;
            isAggrod = false;
        }

        if (Vector3.Distance(transform.position, spawnPositionObject.transform.position) > distAllowedFromSpawn)
        {
            returnToSpawn = true;
        }
        else if (Vector3.Distance(transform.position, spawnPositionObject.transform.position) < distAllowedFromSpawn/5)
            returnToSpawn = false;

        if (returnToSpawn)
            target = spawnPositionObject.transform;
        else
            target = GameObject.Find("Player").transform;

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

                if (MagicCooldown <= 0 && Vector3.Distance(transform.position, target.position) >= 7 && Random.Range(0, 2) >= 0.1f)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyIdleAnim") || animator.GetCurrentAnimatorStateInfo(0).IsName("InTerritory"))
                    {
                        SpellToCast = "Fireball";
                        animator.Play("EnemySpellCastAnim");
                        MagicCooldown = MagicCoolDownTime;
                    }
                }
                else if (MagicCooldown <= 0 && Vector3.Distance(transform.position, target.position) <= 7 && Vector3.Distance(transform.position, target.position) >= 0 && Random.Range(0, 2) >= 0.1f)
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
        else if(enemyAIType == EnemyAIType.Boss)
        {   
            if (playerInTerritory || isAggrod || returnToSpawn)
            {
                animator.SetBool("InTerritory", true);
                transform.LookAt(target);
                transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
                if (BackStepCooldown <= 0 && Vector3.Distance(transform.position, target.position) >= 0 && Vector3.Distance(transform.position, target.position) <= 10 && Random.Range(0, 2) >= 0.1f)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyIdleAnim") || animator.GetCurrentAnimatorStateInfo(0).IsName("InTerritory"))
                    {
                        animator.Play("EnemyBackStepAnim");
                        BackStepCooldown = BackStepCoolDownTime;
                    }
                }
                else if (MeleeAttackCooldown <= 0 && Vector3.Distance(transform.position, target.position) <= 20 && Random.Range(0, 2) >= 0.1f)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyIdleAnim") || animator.GetCurrentAnimatorStateInfo(0).IsName("InTerritory"))
                    {

                        animator.Play("EnemyAttackAnim");
                        MeleeAttackCooldown = MeleeAttackCooldownTime;


                    }
                }
                else if (MeleeAttackCooldown <= 0 && Vector3.Distance(transform.position, target.position) >= 20 && Vector3.Distance(transform.position, target.position) <= 30 && Random.Range(0, 2) >= 0.1f && !ObstacleFound)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyIdleAnim") || animator.GetCurrentAnimatorStateInfo(0).IsName("InTerritory"))
                    {
                        animator.Play("EnemyAttackMoveForward");
                        MeleeAttackCooldown = MeleeAttackCooldownTime;
                    }


                }
                else if (SideStepCooldown <= 0 && Vector3.Distance(transform.position, target.position) >= 20 && Vector3.Distance(transform.position, target.position) <= 35 && Random.Range(0, 2) >= 0.1f && !ObstacleFound)
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
                else if (MagicCooldown <= 0 && Vector3.Distance(transform.position, target.position) >= 30 && Random.Range(0, 2) >= 0.1f)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyIdleAnim") || animator.GetCurrentAnimatorStateInfo(0).IsName("InTerritory"))
                    {
                        SpellToCast = "SpawnEnemy";
                        animator.Play("EnemySpellCastAnim");
                        MagicCooldown = MagicCoolDownTime;
                    }
                }
                else if (Vector3.Distance(transform.position, target.position) >= 30 && Random.Range(0, 2) >= 0.1f && !ObstacleFound)
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

        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
        if (GetComponent<CharacterStatsScript>().currentHealth <= 0)
        {
            Death();
        }
        health = GetComponent<CharacterStatsScript>().currentHealth;
        
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyBackStepAnim"))
        {
            GetComponent<Rigidbody>().velocity = transform.forward * (-5 * (Speed * (GetComponent<CharacterStatsScript>().speed / 100)));
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyAttackMoveForward"))
        {
            GetComponent<Rigidbody>().velocity = transform.forward * (5 * (Speed * (GetComponent<CharacterStatsScript>().speed / 100)));
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyMoveAnim"))
        {
            GetComponent<Rigidbody>().velocity = transform.forward * (10 * (Speed * (GetComponent<CharacterStatsScript>().speed / 100)));
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("EnemySideStepAnim"))
        {
            GetComponent<Rigidbody>().velocity = transform.right * (5* (Speed * (GetComponent<CharacterStatsScript>().speed / 100)));
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
                //GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, -hit.distance * 0.5f, GetComponent<Rigidbody>().velocity.z);
                transform.position = new Vector3(transform.position.x, (GetComponent<BoxCollider>().size.y/2)*transform.localScale.y  + hit.point.y,transform.position.z);
            }
        }
    }
    public void OnMeleeHit(GameObject other)
    {
        if (target == null)
            target = other.gameObject.transform.parent.transform;

        if (other.GetComponent<EnemyAIScript>())
            return;

        other.gameObject.GetComponent<AttackZoneScript>().timer = 0;
        GetComponent<CharacterStatsScript>().TakeDamage(other.transform.parent.GetComponent<CharacterStatsScript>().DealDamage(other.transform.parent.GetComponent<CharacterStatsScript>().strength,CharacterStatsScript.DamageTypes.Melee));

        if (poiseTimer == 0)
            poiseTimer = 1;

        if (poisedamage > poise)
        {
            animator.Play("EnemyBackStepAnim");
            poiseTimer = 0;
            poisedamage = 0;
            BackStepCooldown = BackStepCoolDownTime;
        }

       
        returnToSpawn = false;

    }
    public void CastSpell()
    {      
     
        if(SpellToCast == "PushSpell")
        {
            GameObject temp;
            temp = Instantiate(Resources.Load("Spells/EnemySpells/PushSpell") as GameObject, transform.position + new Vector3(0,1,0), Quaternion.identity);
            temp.GetComponent<PushSpellScript>().CastedFromEnemy = true;
        }
        else if (SpellToCast == "SpawnEnemy")
        {
            GameObject temp = Instantiate(enemyToSpawn);

            Vector3 playerPos = GameObject.Find("Player").transform.position;

            temp.transform.position = new Vector3(playerPos.x + Random.Range(-10, 10), playerPos.y + 1, playerPos.z + Random.Range(-10, 10));
        }
        else if (SpellToCast != null)
        {
            GameObject temp;
            SpellDatabase s = GameObject.Find("SpellObjects").GetComponent<SpellDatabase>();
            for (int i = 0; i < s.masterSpellBook.Count; i++)
            {
                if (s.masterSpellBook[i].spellName == SpellToCast)
                {
                    id = i;
                }
            }
            temp = Instantiate(Resources.Load("Spells/" + s.masterSpellBook[id].objectName) as GameObject, transform.position + transform.forward * 5, gameObject.transform.rotation);
            currentSpellObject = temp;
            duration = findSpell(id).duration;
            currentSpellObject.GetComponent<SpellBase>().SpellId = id;
            currentSpellObject.GetComponent<SpellBase>().caster = gameObject;
            temp.GetComponent<SpellBase>().caster = gameObject;
            temp.GetComponent<SpellBase>().enemyCaster = true;
        }
        FindObjectOfType<AudioPlayerScript>().PlayAudio("SpellCast", transform.position);
        MagicCooldown = MagicCoolDownTime;
    }

    public Spells findSpell(int id)
    {
        Spells temp = GameObject.Find("SpellObjects").GetComponent<SpellDatabase>().masterSpellBook[id];
        return temp;
    }
}
