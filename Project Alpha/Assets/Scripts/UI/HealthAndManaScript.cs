using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthAndManaScript : MonoBehaviour
{
    public Image
        healthImage,
        manaImage;

    public Text
        healthText,
        manaText;

    float
        currentHealthFill,
        currentManaFill;

    CharacterStatsScript characterStats;

	void Start ()
    {
        characterStats = GameObject.Find("Player").GetComponent<CharacterStatsScript>();
	}
	
	void Update ()
    {
		healthText.text = "" + characterStats.currentHealth + " / " + characterStats.maxHealth;
        manaText.text = "" + characterStats.currentMana + " / " + characterStats.maxMana;

        if (characterStats.currentHealth < characterStats.maxHealth)
        {
            currentHealthFill = ((float)characterStats.currentHealth / (float)characterStats.maxHealth);

            if (healthImage.fillAmount > currentHealthFill)
            {
                healthImage.fillAmount -= 0.007f;
            }
            else if (healthImage.fillAmount < currentHealthFill)
            {
                healthImage.fillAmount += 0.005f;
            }
            if (healthImage.fillAmount == currentHealthFill)
            {
                healthImage.fillAmount = healthImage.fillAmount;
            }
        }
        else if (characterStats.currentHealth >= characterStats.maxHealth)
        {
            if (healthImage.fillAmount < 1)
            {
                healthImage.fillAmount += (float)0.005;
            }
        }

        if (characterStats.currentMana < characterStats.maxMana)
        {
            currentManaFill = ((float)characterStats.currentMana / (float)characterStats.maxMana);

            if (manaImage.fillAmount > currentManaFill)
            {
                manaImage.fillAmount -= 0.007f;
            }
            else if (manaImage.fillAmount < currentManaFill)
            {
                manaImage.fillAmount += 0.005f;
            }
            if (manaImage.fillAmount == currentManaFill)
            {
                manaImage.fillAmount = manaImage.fillAmount;
            }
        }
        else if (characterStats.currentMana >= characterStats.maxMana)
        {
            if (manaImage.fillAmount < 1)
            {
                manaImage.fillAmount += (float)0.005;
            }
        }
    }
}
