using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitality : MonoBehaviour {

    public int
        buffAmount;
    public float
        duration;

    void Start()
    {
        gameObject.GetComponent<CharacterStatsScript>().vitality += buffAmount;
    }

    void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            gameObject.GetComponent<CharacterStatsScript>().vitality -= buffAmount;
            Destroy(GetComponent<Vitality>());
        }
    }
}
