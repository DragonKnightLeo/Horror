using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Characters
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void DamageTaken(float damageAmount)
    {
        damageAmount = damageAmount/2;
        print("Enemy Damaged: " + damageAmount);

        currentHitPoints -= damageAmount;

        if(currentHitPoints <= 0)
        {
            KillCharacter();
        }
    }

    public override IEnumerator DamageCharacter(float damage, float interval)
    {
        while (true)
        {
            currentHitPoints = Mathf.Round(currentHitPoints - damage);
            print("Enemy Hit");
            if (currentHitPoints <= float.Epsilon)
            {
                KillCharacter();
                break;
            }
            
            if (interval > float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            }
            else
            {
                break;
            }
    
        }
    }

    public void playMonsterGrowl()
    {
        SoundManager.instance.monster1GrowlSound();
    }

    public void playMonster1Steps()
    {
        SoundManager.instance.monster1StepsSound();
    }
   
}
