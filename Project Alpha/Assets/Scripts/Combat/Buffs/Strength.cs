using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strength : MonoBehaviour {

    public int
        buffAmount;
    public float
        duration;

    void Start()
    {
        gameObject.GetComponent<CharacterStatsScript>().strength += buffAmount;
    }

    void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            gameObject.GetComponent<CharacterStatsScript>().strength -= buffAmount;
            Destroy(GetComponent<Strength>());
        }
    }
}
