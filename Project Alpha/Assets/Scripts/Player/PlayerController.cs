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

    public bool debugMovement = false;
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

    public bool debugMode = false;
    GameObject debugText;
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
        debugText = GameObject.Find("DebugMode");
        debugText.SetActive(false);
    }

    
    public void DebugChest()
    {
        int x = 0;
        for (int i = 0; i < FindObjectOfType<ItemManagerScript>().InventoryItemList.Count; i++)
        {
            if (i % 16 == 0)
            {
                tempChest = Instantiate(chestPrefab);

                tempChest.transform.position = gameObject.transform.position + (transform.forward * 2) + (transform.up *(((i+1)/16)*5));
                if(x>=16)
                x = 0;
            }         
            tempChest.GetComponent<CharacterInventoryScript>().InventoryStorage[x] = FindObjectOfType<ItemManagerScript>().InventoryItemList[i];
            tempChest.GetComponent<CharacterInventoryScript>().InventoryItemAmount[x] = tempChest.GetComponent<CharacterInventoryScript>().InventoryStorage[x].itemMaxAmount;
            x++;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Home))
        {
            debugMode = !debugMode;
            debugText.SetActive(debugMode);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 50.0f))
            {
                if (hit.transform != null)
                {
                    if (hit.transform.gameObject.GetComponent<CharacterInventoryScript>())
                    {
                        hit.transform.gameObject.GetComponent<CharacterInventoryScript>().OpenInventoryCanvas();
                    }
                    if (hit.transform.gameObject.GetComponent<ShopScript>())
                    {
                        hit.transform.gameObject.GetComponent<ShopScript>().OpenInventoryCanvas();
                    }
                    if (hit.transform.gameObject.GetComponent<QuestGiverScript>())
                    {
                        hit.transform.gameObject.GetComponent<QuestGiverScript>().OnPress(gameObject);
                    }
                    if (hit.transform.gameObject.GetComponent<QuestTalkScript>())
                    {
                        hit.transform.gameObject.GetComponent<QuestTalkScript>().OnPress(gameObject);
                    }

                }
            }

        }

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

        if (Input.GetKeyDown(KeyCode.Y) && debugMode)
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

        if(!debugMovement)
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
                moveDirection *= (runSpeed * (GetComponent<CharacterStatsScript>().speed/100));


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
        else if(debugMovement)
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


        if (Input.GetMouseButtonDown(0) && canMove && coolDown <= 0 && !FindObjectOfType<MenuOpenScript>().MenuOpen)
            {
                AttackZone.SetActive(true);
                coolDown = attackCoolDown;
            FindObjectOfType<AudioPlayerScript>().PlayAudio("Swipe", transform.position, true);
        }


        if (castStates == CastStates.casting)
        {
            castTimer -= Time.deltaTime;
            if (castTimer <= 0)
            {
                GameObject temp = Instantiate(currentSpellObject, transform.position + transform.forward * 2, Quaternion.identity);
                temp.transform.rotation = transform.rotation;
                castStates = CastStates.notCasting;
                castTimer = -1;
            }
        }

           if(Input.GetKeyDown(KeyCode.Q))
        {
            foreach (ItemManagerScript.InventoryItem i in GetComponent<CharacterInventoryScript>().InventoryStorage)
            {
                if(i.itemId == 0)
                {
                    GetComponent<CharacterInventoryScript>().OnUseItem(0);
                    GetComponent<CharacterInventoryScript>().RemoveItem(0, 1);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (ItemManagerScript.InventoryItem i in GetComponent<CharacterInventoryScript>().InventoryStorage)
            {
                if (i.itemId == 1)
                {
                    GetComponent<CharacterInventoryScript>().OnUseItem(1);
                    GetComponent<CharacterInventoryScript>().RemoveItem(1, 1);
                }
            }
        }

        for (int i = 0; i < hotBar.Length; i++)
            {
            if (Input.GetKeyDown(hotBar[i]) && castStates != CastStates.casting)
                {
                if (GameObject.Find("SpellObjects").GetComponent<SpellDatabase>().localSpellBook.Contains(GameObject.Find("SpellObjects").GetComponent<SpellDatabase>().masterSpellBook[hotbarGameobject[i].GetComponent<HotbarSpellId>().spellId]))
                {
                    CastSpell(hotbarGameobject[i].GetComponent<HotbarSpellId>().spellId);
                }
                else
                {
                    print("You Don't Know This Spell Yet!");
                }

                }
            }

            if(Input.GetKeyDown(KeyCode.U) && debugMode)
        {
            Instantiate(Resources.Load("Enemies/MagicEnemyPrefab"), new Vector3(transform.position.x, transform.position.y, transform.position.z + 5), Quaternion.identity);
        }

            if(Input.GetKeyDown(KeyCode.T) && debugMode)
            {
                debugMovement = !debugMovement;

            }
        
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menuActive = !menuActive;
            }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        myAng = Vector3.Angle(Vector3.up, hit.normal);
    }  
    
    public void CastSpell(int id)
    {
        if(GetComponent<CharacterStatsScript>().currentMana >= findSpell(id).manacost)
        {
            FindObjectOfType<AudioPlayerScript>().PlayAudio("SpellCast", transform.position, true);
            castTimer = findSpell(id).castTime;
            currentSpellObject = Resources.Load("Spells/" + findSpell(id).objectName) as GameObject;
            duration = findSpell(id).duration;
            GetComponent<CharacterStatsScript>().currentMana -= findSpell(id).manacost;
            currentSpellObject.GetComponent<SpellBase>().SpellId = id;
            currentSpellObject.GetComponent<SpellBase>().caster = gameObject;
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
        if(other.gameObject.name == "AttackZone" && other.transform.parent.name != "Player")
        {
            GetComponent<CharacterStatsScript>().TakeDamage(other.transform.parent.transform.parent.GetComponent<CharacterStatsScript>().strength * 5);
        }
    }
    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Spell")
        {

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
