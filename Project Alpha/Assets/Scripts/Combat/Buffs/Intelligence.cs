using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intelligence : MonoBehaviour {

    public int
        buffAmount;
    public float
        duration;

    void Start()
    {
        gameObject.GetComponent<CharacterStatsScript>().intelligence += buffAmount;
    }

    void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            gameObject.GetComponent<CharacterStatsScript>().intelligence -= buffAmount;
            Destroy(GetComponent<Intelligence>());
        }
    }
}
