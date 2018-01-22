using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUIScript : MonoBehaviour
{
    public bool
        statsActive;

    public GameObject statBackground;

    public CharacterStatsScript characterStats;

    public Text attributePointsText;

    public Text[] statLevelText = new Text[6];
    public int[] statLevel = new int[6];

    void Start ()
    {
        statsActive = false;
        characterStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStatsScript>();
	}
	
	void Update ()
    {
        if (statsActive)
        {
            statBackground.SetActive(true);
        }
        else if (!statsActive)
        {
            statBackground.SetActive(false);
        }

        statLevel[0] = characterStats.intelligence;
        statLevel[1] = characterStats.strength;
        statLevel[2] = characterStats.dexterity;
        statLevel[3] = characterStats.vitality;
        statLevel[4] = characterStats.resistance;
        statLevel[5] = characterStats.luck;

        attributePointsText.text = "" + characterStats.attributePoints;

        for (int i = 0; i < statLevelText.Length; i++)
        {
            statLevelText[i].text = "" + statLevel[i];
        }
	}
}
