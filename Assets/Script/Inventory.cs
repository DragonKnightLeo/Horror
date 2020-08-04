using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryPanel;
    public static Inventory instance;
    public ItemSlot[] slots;
    public Items[] items;
    //public EquipButton buttonLabel;
    //[SerializeField] EquipButton goldOnHand;
    int equippedItemLimit = 1;
    int potionLimit = 10;
    int ammoItemLimit = 100;
    int qty = 1;
    public int itemSelected;
    public int slotSelected;
    public int slotSelectedPlus;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }


        //checkSlot();
    }

    // Update is called once per frame
    void Update()
    {
        //invAtStart();
        //goldOnHand.buttonLabel.text = Player.playerInstance.goldOnHand.ToString();
        
    }

    public bool addNewItem(Items itemToAdd)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].buttonValue == 0)
            {

                return addItems(i, itemToAdd);

            }
            else if (slots[i].ItemInSlot.text == itemToAdd.itemName)
            {
                if (itemToAdd.itemType == Items.ItemType.WEAPON)
                {

                    return addItems(i, itemToAdd);
                }
                else if(itemToAdd.itemType == Items.ItemType.CONSUMABLE)
                {
                        return addItems(i, itemToAdd);
                }
            }

        }
        return false;
    }

    public bool addItems(int i, Items itemToAdd)
    {
        if (itemToAdd.itemName == slots[0].ItemInSlot.text)
        {
                slots[0].qtyText.text = (slots[0].buttonValue += qty).ToString();
                return true;
        }
        else if (itemToAdd.itemName == slots[1].ItemInSlot.text)
        {
                slots[1].qtyText.text = (slots[1].buttonValue += qty).ToString();
                return true;

        }
        else if (itemToAdd.itemName == slots[2].ItemInSlot.text)
        {
                slots[2].qtyText.text = (slots[2].buttonValue += qty).ToString();
                return true;
        }
        return false;
    }

    public void invAtStart()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if(slots[i].buttonValue == 0)
            {
                slots[i].buttonImage.gameObject.SetActive(false);
                slots[i].qtyText.gameObject.SetActive(false);
                slots[i].ItemInSlot.gameObject.SetActive(false);
            }
            else
            {
                slots[i].buttonImage.gameObject.SetActive(true);
                slots[i].qtyText.gameObject.SetActive(true);
                slots[i].ItemInSlot.gameObject.SetActive(true);
            }
        }
    }

    public void selectItem(int i)
    {
        slotSelected = i;
        slotSelectedPlus = i + 1;

        for (int x = 0; x < items.Length; x++)
        {
            if (slots[i].ItemInSlot.text == items[x].itemName)
            {
                itemSelected = x;


                /*
                if (items[x].itemType == Items.ItemType.CONSUMABLE)
                {
                    //buttonLabel.buttonLabel.text = "Use";
                }
                else
                {
                    //buttonLabel.buttonLabel.text = "Equip";
                }

                ShopPanelManager.instance.goldAmount.text = "" + Inventory.instance.items[Inventory.instance.itemSelected].value / 2;
                */
             }
        }
        
    }

    public void discardItems()
    {
        if (slots[slotSelected].buttonValue > 0)
        {
            slots[slotSelected].qtyText.text = (slots[slotSelected].buttonValue -= 1).ToString();
        }
    }

}

