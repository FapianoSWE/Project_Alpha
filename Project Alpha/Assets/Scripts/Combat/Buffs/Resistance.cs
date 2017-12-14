using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resistance : MonoBehaviour {

    public int
        buffAmount;
    public float
        duration;

    void Start()
    {
        gameObject.GetComponent<CharacterStatsScript>().resistance += buffAmount;
    }

    void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            gameObject.GetComponent<CharacterStatsScript>().resistance -= buffAmount;
            Destroy(GetComponent<Resistance>());
        }
    }
}
