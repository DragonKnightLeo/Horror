using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Characters
{
    public static Player playerInstance;
    public Healthbar healthBarPrefab;
    Healthbar healthBar;
    public string areaToTransitionName;
    Items hitObject;
    Enemy enemy;
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
        spawnHealthBar();
    }


    //Update is called once per frame
    void Update()
    {
        //levelUP();
        //lvlUP();
    }

    public override IEnumerator DamageCharacter(float damage, float interval)
    {
        throw new System.NotImplementedException();
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
                    currentHitPoints += 10;
                    if (currentHitPoints > maxHitPoints)
                    {
                        currentHitPoints = maxHitPoints;
                    }
                }
                else if (Inventory.instance.items[itemSelectedPos].itemName == "Mana Potion" && currentManaPoints < maxManaPoints)
                {
                    print("reached mana");
                    currentManaPoints += 10;
                    if (currentManaPoints > maxManaPoints)
                    {
                        currentManaPoints = maxManaPoints;
                    }
                }
                Inventory.instance.slots[slotSelectedPos].qtyText.text = (Inventory.instance.slots[slotSelectedPos].buttonValue -= 1).ToString();

            }
            else if (Inventory.instance.items[itemSelectedPos].itemType == Items.ItemType.WEAPON ||
                    Inventory.instance.items[itemSelectedPos].itemType == Items.ItemType.ARMOR)
            {
                if (buttonValue > 0 && slotSelectedPlus > 0)
                {

                    switch (Inventory.instance.items[itemSelectedPos].itemName)
                    {
                        case "Iron Sword":
                            print("Iron Sword Equipped ");
                            attackPower = Inventory.instance.items[itemSelectedPos].itemDamage;
                            break;

                        case "Iron Armor":
                            print("Iron Armor Equipped ");
                            armorPower = Inventory.instance.items[itemSelectedPos].itemArmor;
                            break;

                        case "Leather Armor":
                            print("Leather Armor Equipped ");
                            armorPower = Inventory.instance.items[itemSelectedPos].itemDamage;
                            break;

                        case "Wooden Sword":
                            print("Wooden Sword Equipped ");
                            attackPower = Inventory.instance.items[itemSelectedPos].itemDamage;
                            break;
                        default:
                            break;
                    }
                }
                Inventory.instance.slots[slotSelectedPos].qtyText.text = (Inventory.instance.slots[slotSelectedPos].buttonValue -= 1).ToString();
                print("slot selected plus after is" + slotSelectedPlus);
            }
            Inventory.instance.slotSelectedPlus = 0;
        }

    }
    public void DamageTaken(float damageAmount)
    {
        damageAmount = damageAmount / 2;

        currentHitPoints -= damageAmount;

        if (currentHitPoints <= 0)
        {
            KillCharacter();
        }
    }

    public void spawnHealthBar()
    {
        healthBar = Instantiate(healthBarPrefab);
    }
}

