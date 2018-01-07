using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushSpellScript : MonoBehaviour {
    float lifetime;
    public float lifetimeLength;

    public GameObject EffectOnSpawn;
    public bool CastedFromEnemy;

    void Start()
    {
        Instantiate(EffectOnSpawn, gameObject.transform.position, Quaternion.identity, null);
    }

    void Update()
    {
        lifetime += Time.deltaTime;
        transform.localScale *= 1.2f;
        if (lifetime >= lifetimeLength)
        {
            Destroy(gameObject);
        }

    }

}
