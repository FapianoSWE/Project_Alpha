using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBarScript : MonoBehaviour
{
    public Image expBar;

    public Text expAmount;

    CharacterStatsScript characterStats;

	void Start ()
    {
        characterStats = GameObject.Find("Player").GetComponent<CharacterStatsScript>();
        expBar.fillAmount = 0.0f;
        expAmount = GameObject.Find("Experience").GetComponentInChildren<Text>();
	}
	
	void Update ()
    {
        if (characterStats.currentXp > 0.0f)
        {
            expBar.fillAmount = (float)((float)characterStats.currentXp / (float)characterStats.xpNeeded);
        }
        expAmount.text = "Experience: " + (characterStats.currentXp) + "/" + (characterStats.xpNeeded);
    }
}
