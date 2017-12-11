using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUIScript : MonoBehaviour
{
    public Text moneyText;

    CharacterStatsScript characterStats;

	void Start ()
    {
        characterStats = GameObject.Find("Player").GetComponentInChildren<CharacterStatsScript>();
	}
	
	void Update ()
    {
        moneyText.text = "Money: " + characterStats.gold;
	}
}
