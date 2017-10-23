using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rejuvenation : MonoBehaviour {


    bool canSetHealing = true;
    float duration;
    float tickTimer = 3f;
    float tickHeal;
    CharacterStatsScript characterStats;
    PlayerController playerController;


    void Start () {
        characterStats = GameObject.Find("Player").GetComponent<CharacterStatsScript>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        duration = playerController.duration;
	}
	

	void Update () {

        tickTimer -= Time.deltaTime;
        duration -= Time.deltaTime;

        if(canSetHealing)
        {
            tickHeal = Mathf.Round(playerController.baseHeal + (characterStats.intelligence * 1.5f));
            canSetHealing = false;
        }

        
        if(tickTimer <= 0)
        {
            characterStats.currentHealth += (int)tickHeal;
            tickTimer = 3f;
        }
        if(duration <= 0)
        {
            Destroy(this);
        }
	}
}
