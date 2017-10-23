using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSpellScript : MonoBehaviour
{

    float lifetime;
    public float lifetimeLength;

    public int baseHeal;
    public int heal;

    public GameObject EffectOnSpawn;
    public string caster;
    bool canSetHeal;

    void Start()
    {
        canSetHeal = true;
        Instantiate(EffectOnSpawn,gameObject.transform.position,Quaternion.identity,null);
    }

    void Update()
    {
        lifetime += Time.deltaTime;
        transform.localScale *= 1.04f;
        if (lifetime >= lifetimeLength)
        {
            Destroy(gameObject);
        }

        if (canSetHeal)
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player"))
            {

                
                    baseHeal = 10 * g.GetComponent<CharacterStatsScript>().currentLevel;
                    g.GetComponent<CharacterStatsScript>().Healed(baseHeal);
                    canSetHeal = false;
                
            }
        }
    }
}
