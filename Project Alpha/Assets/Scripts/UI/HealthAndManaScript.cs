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

    void Start()
    {
        characterStats = GameObject.Find("Player").GetComponent<CharacterStatsScript>();
    }

    void Update()
    {
        healthText.text = "" + characterStats.currentHealth + " / " + characterStats.maxHealth + " HP";
        manaText.text = "" + characterStats.currentMana + " / " + characterStats.maxMana + " MP";

        if (characterStats.currentHealth < characterStats.maxHealth)
        {
            currentHealthFill = ((float)characterStats.currentHealth / (float)characterStats.maxHealth);
            bool losingHealth = currentHealthFill < healthImage.fillAmount;

            if (healthImage.fillAmount > currentHealthFill)
            {
                healthImage.fillAmount -= 0.007f;

                if (losingHealth)
                {
                    if (currentHealthFill >= healthImage.fillAmount)
                    {
                        healthImage.fillAmount = currentHealthFill;
                    }
                }
            }
            else if (healthImage.fillAmount < currentHealthFill)
            {
                healthImage.fillAmount += 0.005f;

                if (!losingHealth)
                {
                    if (currentHealthFill <= healthImage.fillAmount)
                    {
                        healthImage.fillAmount = currentHealthFill;
                    }
                }
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
            bool losingMana = currentManaFill < manaImage.fillAmount;

            if (manaImage.fillAmount > currentManaFill)
            {
                manaImage.fillAmount -= 0.007f;

                if (losingMana)
                {
                    if (currentManaFill >= manaImage.fillAmount)
                    {
                        manaImage.fillAmount = currentManaFill;
                    }
                }
            }
            else if (manaImage.fillAmount < currentManaFill)
            {
                manaImage.fillAmount += 0.005f;

                if (!losingMana)
                {
                    if (currentManaFill <= manaImage.fillAmount)
                    {
                        manaImage.fillAmount = currentManaFill;
                    }
                }
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
