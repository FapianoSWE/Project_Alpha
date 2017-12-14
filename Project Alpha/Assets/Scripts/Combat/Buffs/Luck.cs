using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luck : MonoBehaviour {

    public int
        buffAmount;
    public float
        duration;

    void Start()
    {
        gameObject.GetComponent<CharacterStatsScript>().luck += buffAmount;
    }

    void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            gameObject.GetComponent<CharacterStatsScript>().luck -= buffAmount;
            Destroy(GetComponent<Luck>());
        }
    }
}
