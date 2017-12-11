    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public enum CastStates
{
    notCasting,
    casting,
    justCasted
}

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    Quaternion originalRotation;
    Text[] coordinates;
    GameObject commandDisplay;
    

    public KeyCode[] hotBar = new KeyCode[10];
    public GameObject[] hotbarGameobject = new GameObject[10];

    public float jumpSpeed = 30.0f;
    public float gravity = 55.0f;
    public float runSpeed = 70.0f;
    private float rotateSpeed = 150.0f;

    public bool grounded;
    public Vector3 moveDirection = Vector3.zero;
    public bool isWalking;
    private string moveStatus = "idle";

    public GameObject camera1;
    public CharacterController controller;
    public bool isJumping;
    private float myAng = 0.0f;
    public bool canJump = true;


    public CastStates castStates = new CastStates();

    public bool debugMode = false;
    public float debugSpeed = 20f;

    public float castTimer = -1;
    public GameObject currentSpellObject;
    public bool isProjectile;
    public float duration;
    public float baseDamage;
    public float baseHeal;
    public GameObject target;


    Spells spells;

    float waitForAnimation;

    public GameObject inGameMenu,
        EnemyHealthCanvas,
         AttackZone;
    public bool menuActive,
        canMove;

    public float attackCoolDown;
    float coolDown;

    public bool itemCanvasOpen;

    public Vector3 spawnPosition;

    public GameObject chestPrefab;

    GameObject tempChest;

    public GameObject deathEffetct;
    public SpellDatabase spellDatabase;

    public bool inDialogue;

    string currentScene,
        prevScene;

    public BoxCollider bCollider;

    void Start ()
    {
        spellDatabase = GameObject.Find("SpellObjects").GetComponent<SpellDatabase>();
        spawnPosition = transform.position;
        controller = GetComponent<CharacterController>();
        originalRotation = transform.rotation;
        castStates = CastStates.notCasting;
        hotBar[0] = KeyCode.Alpha1;
        hotBar[1] = KeyCode.Alpha2;
        hotBar[2] = KeyCode.Alpha3;
        hotBar[3] = KeyCode.Alpha4;
        hotBar[4] = KeyCode.Alpha5;
        hotBar[5] = KeyCode.Alpha6;
        hotBar[6] = KeyCode.Alpha7;
        hotBar[7] = KeyCode.Alpha8;
        hotBar[8] = KeyCode.Alpha9;
        hotBar[9] = KeyCode.Alpha0;
        /*
        coordinates = GameObject.FindGameObjectWithTag("Coordinates").GetComponentsInChildren<Text>();
        commandDisplay = GameObject.Find("Command Display");
        if(commandDisplay)
        commandDisplay.SetActive(false);
        inGameMenu = GameObject.FindGameObjectWithTag("InGameMenu");
        if(inGameMenu)
        inGameMenu.SetActive(false);
        menuActive = false;
        */
        canMove = true;
        DontDestroyOnLoad(gameObject);
        currentScene = SceneManager.GetActiveScene().name;
        prevScene = SceneManager.GetActiveScene().name;
    }

    
    public void DebugChest()
    {
        for (int i = 0; i < FindObjectOfType<ItemManagerScript>().InventoryItemList.Count; i++)
        {
            if(i % 16 == 0)
            {
                tempChest = Instantiate(chestPrefab);

                tempChest.transform.position = gameObject.transform.position + (transform.forward * 2) + transform.up;              
            }
            tempChest.GetComponent<CharacterInventoryScript>().InventoryItemAmount[i] = 1;
            tempChest.GetComponent<CharacterInventoryScript>().InventoryStorage[i] = FindObjectOfType<ItemManagerScript>().InventoryItemList[i];
        }
    }

    void Update()
    {
        currentScene = SceneManager.GetActiveScene().name;

        if(currentScene != prevScene)
        {
            transform.position = GameObject.Find(GameObject.Find("VarStorage").GetComponent<VarStorageScript>().spawnpointname).transform.position;
            foreach(GameObject g in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (g != gameObject)
                    Destroy(g);
            }

        }
        prevScene = SceneManager.GetActiveScene().name;

        if (Input.GetKeyDown(KeyCode.Y))
            DebugChest();

        if(GetComponent<CharacterStatsScript>().currentHealth<=0)
        {
            Death();
        }

        itemCanvasOpen = false;
        foreach (CharacterEquipmentCanvasScript g in FindObjectsOfType<CharacterEquipmentCanvasScript>())
        {
            if (g.gameObject.activeSelf)
                itemCanvasOpen = true;
        }
        foreach (CharacterInventoryCanvasScript g in FindObjectsOfType<CharacterInventoryCanvasScript>())
        {
            if (g.gameObject.activeSelf)
                itemCanvasOpen = true;
        }
        if (GetComponent<CharacterStatsScript>().statUI.statsActive)
            itemCanvasOpen = true;
        
        coolDown -= Time.deltaTime;
            /*
            if(coordinates[0].name == "X-Coordinate")
            {
                coordinates[0].text = ("X = " + ((int)transform.position.x).ToString());
            }

            if(coordinates[1].name == "Y-Coordinate")
            {
                coordinates[1].text = ("Y = " + ((int)transform.position.y).ToString());
            }

            if(coordinates[2].name == "Z-Coordinate")
            {
                coordinates[2].text = ("Z = " + ((int)transform.position.z).ToString());
            }
            */
            //force controller down slope. Disable jumping

        if(!debugMode)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), GameObject.FindGameObjectWithTag("Terrain").GetComponent<Collider>(), false);
            Physics.IgnoreCollision(bCollider, GameObject.FindGameObjectWithTag("Terrain").GetComponent<Collider>(), false);
            if (Input.GetMouseButtonDown(1))
            {

            }

            if (myAng > 50)
            {

                canJump = false;
            }
            else
            {
                canJump = true;
            }
            if (!canMove)
            {
                moveDirection = Vector3.zero;
            }
            if (grounded && canMove && !itemCanvasOpen)
            {
                isJumping = false;

                moveDirection = new Vector3((Input.GetAxis("Horizontal")), 0, Input.GetAxis("Vertical"));

                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= runSpeed;


                if (moveDirection != Vector3.zero)
                {
                    isWalking = true;
                }
                else if (moveDirection == Vector3.zero)
                {
                    isWalking = false;
                }
                
                if (Input.GetKeyDown(KeyCode.Space) && canJump)
                {
                    moveDirection.y = jumpSpeed;
                    isJumping = true;
                }

            }

            // Allow turning at anytime. Keep the character facing in the same direction as the Camera if the right mouse button is down.

            transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);


            //Apply gravity
            moveDirection.y -= gravity * Time.deltaTime;


            //Move controller
            CollisionFlags flags;
            if (isJumping)
            {
                flags = controller.Move(moveDirection * Time.deltaTime);
            }
            else
            {
                flags = controller.Move((moveDirection + new Vector3(0, -100, 0)) * Time.deltaTime);
            }

            grounded = (flags & CollisionFlags.Below) != 0;

        }
        else if(debugMode)
        {
            moveDirection = new Vector3((Input.GetAxis("Horizontal")), 0, Input.GetAxis("Vertical"));

            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= runSpeed * debugSpeed;
                
            Physics.IgnoreCollision(GetComponent<Collider>(), GameObject.FindGameObjectWithTag("Terrain").GetComponent<Collider>(), true);
            Physics.IgnoreCollision(bCollider, GameObject.FindGameObjectWithTag("Terrain").GetComponent<Collider>(), true);

            transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
            if (Input.GetKey(KeyCode.Space))
            {
                moveDirection.y += gravity * Time.deltaTime * debugSpeed * 5;
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveDirection.y -= gravity * Time.deltaTime * debugSpeed * 5;
            }
            CollisionFlags flags;
            flags = controller.Move((moveDirection * Time.deltaTime));
        }


        if (Input.GetMouseButtonDown(0) && canMove && coolDown <= 0 && !itemCanvasOpen)
            {
                AttackZone.SetActive(true);
                coolDown = attackCoolDown;

            }


        if (castStates == CastStates.casting)
        {
            castTimer -= Time.deltaTime;
            if (castTimer <= 0)
            {
                GameObject temp = Instantiate(currentSpellObject, transform.position + transform.forward * 2, Quaternion.identity);
                if (currentSpellObject.GetComponent<Fireball>())
                    currentSpellObject.GetComponent<Fireball>().caster = gameObject;

                temp.transform.rotation = transform.rotation;
                castStates = CastStates.notCasting;
                castTimer = -1;
            }
        }

        if (Input.GetKeyUp(KeyCode.R) && canMove)
            {
                transform.rotation = originalRotation;
                transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
            }
            

            for (int i = 0; i < hotBar.Length; i++)
            {
            if (Input.GetKeyDown(hotBar[i]) && castStates != CastStates.casting)
                {
                    CastSpell(hotbarGameobject[i].GetComponent<HotbarSpellId>().spellId);
                }
            }

            if(Input.GetKeyDown(KeyCode.U))
        {
            Instantiate(Resources.Load("Enemies/MagicEnemyPrefab"), new Vector3(transform.position.x, transform.position.y, transform.position.z + 5), Quaternion.identity);
        }

            if(Input.GetKeyDown(KeyCode.T))
            {
                debugMode = !debugMode;

            }
        
            
            if (Input.GetKeyDown(KeyCode.Escape) && menuActive == false)
            {
                menuActive = true;
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                menuActive = false;
            }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        myAng = Vector3.Angle(Vector3.up, hit.normal);
    }  
    
    public void CastSpell(int id)
    {
        if(GetComponent<CharacterStatsScript>().currentMana > findSpell(id).manacost)
        {
            castTimer = findSpell(id).castTime;
            currentSpellObject = Resources.Load("Spells/" + findSpell(id).spellName) as GameObject;
            duration = findSpell(id).duration;
            baseDamage = findSpell(id).baseDamage;
            baseHeal = findSpell(id).baseHealing;
            isProjectile = findSpell(id).isProjectile;
            GetComponent<CharacterStatsScript>().currentMana -= findSpell(id).manacost;
            castStates = CastStates.casting;
        }
    }

    public Spells findSpell(int id)
    {
        Spells temp = GameObject.Find("SpellObjects").GetComponent<SpellDatabase>().localSpellBook[id];
        return temp;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "AttackZone")
        {
            GetComponent<CharacterStatsScript>().TakeDamage(other.transform.parent.transform.parent.GetComponent<CharacterStatsScript>().strength * 5);
        }
        if (other.gameObject.name == "Heal(Clone)")
        {
            //GetComponent<CharacterStatsScript>().Healed(Mathf.RoundToInt((other.transform.GetComponent<HealSpellScript>().heal)/(other.gameObject.transform.localScale.x)));
        }
    }
    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Spell")
        {
            Instantiate(c.gameObject.GetComponent<Fireball>().deathEffect, gameObject.transform.position, Quaternion.identity, null);
            GetComponent<CharacterStatsScript>().TakeDamage(c.gameObject.GetComponent<Fireball>().damage);
        }
        if(c.gameObject.GetComponent<PushSpellScript>())
        {
            
        }

    }

    public void Death()
    {
        transform.position = spawnPosition;
        GetComponent<CharacterStatsScript>().currentHealth = GetComponent<CharacterStatsScript>().maxHealth;
        GetComponent<CharacterStatsScript>().currentMana = GetComponent<CharacterStatsScript>().maxMana;
        castStates = CastStates.notCasting;
        deathEffetct.gameObject.SetActive(true);
    }
    
}
