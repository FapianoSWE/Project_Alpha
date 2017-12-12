using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dexterity : MonoBehaviour {

    public int
        buffAmount;
    public float
        duration;

    void Start()
    {
        gameObject.GetComponent<CharacterStatsScript>().dexterity += buffAmount;
    }

    void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            gameObject.GetComponent<CharacterStatsScript>().dexterity -= buffAmount;
            Destroy(GetComponent<Dexterity>());
        }
    }
}
