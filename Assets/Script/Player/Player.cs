using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.LWRP;

public class Player : Characters
{
    Items hitObject;
    Enemy enemy;
    public static Player playerInstance;
    public string areaToTransitionName;
    public bool shouldDisappear;


    // Start is called before the first frame update
    private void Awake()
    {
        if (playerInstance != null && playerInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            playerInstance = this;
        }
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        startingHitPoints = currentHitPoints;
        //spawnHealthBar();
        lightTimer = 600;
    }


    //Update is called once per frame
    void Update()
    {
        //levelUP();
        //lvlUP();
    }

    private void FixedUpdate()
    {
    }

    public override IEnumerator DamageCharacter(float damage, float interval)
    {
<<<<<<< Updated upstream
        print("Player is Hit");
=======
>>>>>>> Stashed changes
        while (true)
        {
            currentHitPoints = Mathf.Round(currentHitPoints - damage);
            PlayerMovement.playerMovementInstance.isHitAnimation(true);
            if (currentHitPoints <= float.Epsilon)
            {
                KillCharacter();
                break;
            }
            PlayerMovement.playerMovementInstance.isHitAnimation(false);
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

    public void levelUP()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            currentExperience += 100;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("CanBePickedUp"))
        {
            hitObject = other.gameObject.GetComponent<UsuableItems>().usuableItems;
            if (hitObject.itemName == "Battery" && hitObject.itemType == Items.ItemType.CONSUMABLE)
            {
                if (lightTimer < 600 || lightTimer >= 0)
                {
                    lightTimer += 300;
                    other.gameObject.SetActive(false);
                }

                if(lightTimer > 600)
                {
                    lightTimer = 600;
                }
            }
            shouldDisappear = Inventory.instance.addNewItem(hitObject);
            
            if (shouldDisappear == true)
            {
               other.gameObject.SetActive(false);
            }
         }
    }

    public void useItem()
    {
        int itemSelectedPos = Inventory.instance.itemSelected;
        int slotSelectedPos = Inventory.instance.slotSelected;
        int slotSelectedPlus = Inventory.instance.slotSelectedPlus;
        int buttonValue = Inventory.instance.slots[slotSelectedPos].buttonValue;

        if (Inventory.instance.slotSelectedPlus > 0)
        {
            if (Inventory.instance.items[itemSelectedPos].itemType == Items.ItemType.CONSUMABLE && buttonValue > 0)
            {
                if (Inventory.instance.items[itemSelectedPos].itemName == "Health Potion" && currentHitPoints < maxHitPoints)
                {
                    print("reached health");
                    currentHitPoints += 20;
                    if (currentHitPoints > maxHitPoints)
                    {
                        currentHitPoints = maxHitPoints;
                    }
                }
                else if (Inventory.instance.items[itemSelectedPos].itemName == "Stamina Potion" && currentManaPoints < maxManaPoints)
                {
                    print("reached stam");
                    currentStamPoints += 30;
                    if (currentStamPoints > maxStamPoints)
                    {
                        currentStamPoints = maxStamPoints;
                    }
                }
                /*
                else if((Inventory.instance.items[itemSelectedPos].itemName == "Bullet"))
                {
                    if (PlayerMovement.playerMovementInstance.bulletCount < 7 && PlayerMovement.playerMovementInstance.bulletCount >= 0 && Inventory.instance.slots[2].buttonValue > 0)
                    {
                        PlayerMovement.playerMovementInstance.bulletCount += Inventory.instance.slots[2].buttonValue;

                        if (Input.GetKeyDown(KeyCode.R))
                        {
                            PlayerMovement.playerMovementInstance.bulletCount += Inventory.instance.slots[2].buttonValue;
                        }
                    }
                }
            }
            */
                Inventory.instance.slots[slotSelectedPos].qtyText.text = (Inventory.instance.slots[slotSelectedPos].buttonValue -= 1).ToString();
                PlayerMovement.playerMovementInstance.playGunSound(false);
                PlayerMovement.playerMovementInstance.animator.SetBool("isShooting", false);
                PlayerMovement.playerMovementInstance.bulletCount +=1;
                Inventory.instance.slotSelectedPlus = 0;
            }
        }

    }

    public void DamageTaken(float damageAmount)
    {
<<<<<<< Updated upstream
        //damageAmount = damageAmount / 2;

        currentHitPoints -= damageAmount;
        SoundManager.instance.takingDamageSound();
        SoundManager.instance.monster1AttackSound();
=======
        print("Player is Hit");
        currentHitPoints -= damageAmount;
        PlayerMovement.playerMovementInstance.isHitAnimation(true);
        SoundManager.instance.takingDamageSound();
       // SoundManager.instance.monster1AttackSound();
>>>>>>> Stashed changes
        if (currentHitPoints <= 0)
        {
            KillCharacter();
        }
    }
}

