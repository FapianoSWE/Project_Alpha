using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agility : MonoBehaviour {

    public int
        buffAmount;
    public float
        duration;

	void Start () {
        gameObject.GetComponent<CharacterStatsScript>().agility += buffAmount;
	}
	
	void Update () {
        duration -= Time.deltaTime;
		if(duration <= 0)
        {
            gameObject.GetComponent<CharacterStatsScript>().agility -= buffAmount;
            Destroy(GetComponent<Agility>());
        }
	}
}
