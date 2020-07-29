using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Characters : MonoBehaviour
{
    public string charName;
    public int level;
    public float currentHitPoints;
    public float maxHitPoints;
    public float startingHitPoints;
    public float currentManaPoints;
    public float maxManaPoints;
    public float startingManaPoints;
    public float maxStamPoints;
    public float currentStamPoints;
    public float startingStamPoints;
    public float attackPower;
    public float armorPower;
    public float currentExperience;
    public float expToLvlUp;
    public int goldOnHand;
    public int maxLvl;
    public int attack;
    public Sprite charImage;


    public enum CharacterCategory
    {
        PLAYER,
        ENEMY,
        SHOPKEEPER,
        NPC
    }

    public CharacterCategory characterCategory;

    public virtual void KillCharacter()
    {
        Destroy(gameObject);
    }


    public virtual IEnumerator FlickerCharacter(Color color, float flickerSeconds)
    {
        GetComponent<SpriteRenderer>().color = color;

        yield return new WaitForSeconds(flickerSeconds);

        GetComponent<SpriteRenderer>().color = Color.white;
    }

    //public abstract void ResetCharacter();

    public abstract IEnumerator DamageCharacter(float damage, float interval);
    

    public void lvlUP()
    {

        if(currentExperience >= expToLvlUp && level <= maxLvl)
        {
            level = level + 1;
            if (currentHitPoints < maxHitPoints || currentManaPoints < maxManaPoints)
            {
                currentHitPoints += 10;
                currentManaPoints += 10;
                currentStamPoints += 10;
            }
            attackPower += 2;
            armorPower += 2;
            expToLvlUp = expToLvlUp * 1.10f;
            currentExperience = 0;

        }
    }

}
