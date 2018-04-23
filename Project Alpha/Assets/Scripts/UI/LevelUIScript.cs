using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIScript : MonoBehaviour
{
    public Text levelText,
        statText;

    public CharacterStatsScript characterStats;

    float flashSpeed = 0.03f;

	void Start ()
    {
        characterStats = GameObject.Find("Player").GetComponent<CharacterStatsScript>();
	}
	
	void Update ()
    {
        levelText.text = "Level: " + characterStats.currentLevel;
        if(characterStats.attributePoints >0)
        {
            statText.gameObject.SetActive(true);
            statText.color = new Color(statText.color.r, statText.color.g, statText.color.b, statText.color.a - flashSpeed);

            if (statText.color.a >= 1 || statText.color.a <= 0)
                flashSpeed *= -1;
        }
        else
            statText.gameObject.SetActive(false);
    }
}
