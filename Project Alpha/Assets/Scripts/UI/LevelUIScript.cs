using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIScript : MonoBehaviour
{
    public Text levelText;

    public CharacterStatsScript characterStats;

	void Start ()
    {
        characterStats = GameObject.Find("Player").GetComponent<CharacterStatsScript>();
	}
	
	void Update ()
    {
        levelText.text = "Level: " + characterStats.currentLevel;
	}
}
