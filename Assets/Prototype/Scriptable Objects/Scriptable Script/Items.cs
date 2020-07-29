using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "item")]
public class Items : ScriptableObject
{
    public string itemName;
    public string itemDesc;
    public Sprite itemImage;
    public int weight;
    public int value;
    public int quantity;
    public float itemDamage;
    public float itemArmor;
    public float durability;
    public bool stackable;


    public enum WeaponType
    {
        MELEE,
        RANGED,
        MAGIC,
        NONE
    }
    public enum ItemType
    {
        WEAPON,
        ARMOR,
        AMMO,
        CONSUMABLE
    }

    public enum Element
    {
        FIRE,
        WATER,
        AIR,
        EARTH,
        ELECTRICITY,
        NONE
    }

    public ItemType itemType;

    public Element itemElement;

    public WeaponType weaponType;

    public string ItemName(){return itemName;}
    public Sprite ItemImage() { return itemImage;}
    public int Quantity() { return quantity; }

    public float ItemDamage() { return itemDamage; }
    public float ItemArmor() { return itemArmor; }

    public float Durability() { return durability; }

    public string ItemDesc() { return itemDesc; }

    public bool Stackable() { return stackable; }






}
