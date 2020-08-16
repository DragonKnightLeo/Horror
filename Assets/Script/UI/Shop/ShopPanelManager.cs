using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanelManager : MonoBehaviour
{
    public static ShopPanelManager instance;
    [SerializeField] GameObject shopDialogWindow;
    [SerializeField] GameObject shopPanel;
    [SerializeField] GameObject itemPriceAndName;
    [SerializeField] GameObject invActionPanel;
    [SerializeField] GameObject invInfoPanel;
    [SerializeField] ShopKeeperManager itemsInShop;
    [SerializeField] GameObject dialogBox;
    [SerializeField] ItemSlot[] shopSlots;
    [SerializeField] EquipButton buttonLabel;
    [SerializeField] EquipButton goldOnHand;
    [SerializeField] ShopItemLabel itemName;
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject[] shopMiscPanels;

    public Text goldAmount;

    int itemPrice;
    int slotSelected;
    int slotSelectedPlus;
    public int itemNum = 100;
    public int decreaseItem = 1;
    int qty = 1;
    int itemSelected;

    Items[] itemsToShow;


    // Start is called before the first frame update
    void Start()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        itemsToShow = itemsInShop.getItemInShop();
    }

    // Update is called once per frame
    void Update()
    {
        goldOnHand.buttonLabel.text = Player.playerInstance.goldOnHand.ToString();
        showItemsInShop();
        slotIsEmpty();
        changeButtonLabels();
    }

    void showItemsInShop()
    {
        for (int i = 0; i < itemsToShow.Length; i++)
        {
            shopSlots[i].ItemInSlot.text = itemsToShow[i].itemName;
            shopSlots[i].buttonImage.sprite = itemsToShow[i].itemImage;
            shopSlots[i].buttonValue = itemsInShop.getNumItem();
        }

    }

    void slotIsEmpty()
    {
        for (int i = 0; i < shopSlots.Length; i++)
        {
            shopSlots[i].qtyText.gameObject.SetActive(false);
            shopSlots[i].ItemInSlot.gameObject.SetActive(false);
            if (shopSlots[i].buttonValue == 0)
            {
                shopSlots[i].buttonImage.gameObject.SetActive(false);
                shopSlots[i].qtyText.gameObject.SetActive(false);

            }

            if (shopSlots[i].buttonValue >= 1)
            {
                shopSlots[i].buttonImage.gameObject.SetActive(true);
            }


        }
    }

    public void selectItem(int i)
    {
        slotSelected = i;
        slotSelectedPlus = i + 1;
        print("Slot Selected is " + slotSelected);
        for (int x = 0; x < itemsToShow.Length; x++)
        {
            if (shopSlots[i].ItemInSlot.text == itemsToShow[x].itemName)
            {
                itemSelected = x;
                print("Item Selected " + itemSelected + "" + itemsToShow[x].itemName);
                itemName.setItemName(itemsToShow[x].itemName);
                goldAmount.text = itemsToShow[x].value.ToString();
            }
        }
        i = 0;
    }

    public void buySellItems()
    {

        if (buttonLabel.buttonLabel.text == "Buy")
        {
            int itemPrice = itemsToShow[itemSelected].value;

            if (shopSlots[slotSelected].ItemInSlot.text != "" && slotSelectedPlus > 0)
            {
                print("slot selected plus is " +  slotSelectedPlus);
                if (Player.playerInstance.goldOnHand >= itemPrice)
                {
                    Inventory.instance.addNewItem(itemsToShow[itemSelected]);
                    Player.playerInstance.goldOnHand -= itemPrice;
                }
                else
                {
                    print("Player is broke!");
                }
                slotSelectedPlus = 0;
            }
            else
            {
                print("Select a slot");
            }    
        }
        else
        {
            if(Inventory.instance.slots[Inventory.instance.slotSelected].buttonValue > 0 && Inventory.instance.slotSelectedPlus > 0)
            {

                //print("goldAmount" + goldAmount);
                Player.playerInstance.goldOnHand = Player.playerInstance.goldOnHand + Inventory.instance.items[Inventory.instance.itemSelected].value / 2;
                addNewShopItem();
                Inventory.instance.discardItems();
                Inventory.instance.slotSelectedPlus = 0;
            }
            else
            {
                print("No Items found in slot");
            }
        }
        goldAmount.text = 0.ToString();
    }

    public void openSellWIndow()
    {
        //shopDialogWindow.SetActive(false);
        goldAmount.text = 0.ToString();
        for (int i = 0; i < shopMiscPanels.Length; i++)
        {
            if (shopMiscPanels[i].activeInHierarchy)
            {
                print(i);
                shopPanel.SetActive(false);
                inventoryPanel.SetActive(true);
                invActionPanel.SetActive(false);
                invInfoPanel.SetActive(false);
            }
            if (!shopMiscPanels[i].activeInHierarchy && !inventoryPanel.activeInHierarchy && !shopPanel.activeInHierarchy)
            {
                for (int x = 0; x < shopMiscPanels.Length; x++)
                {
                    inventoryPanel.SetActive(true);
                    invActionPanel.SetActive(false);
                    invInfoPanel.SetActive(false);
                    shopMiscPanels[x].SetActive(true);
                    /*
                    shopMiscPanels[0].SetActive(true);
                    shopMiscPanels[1].SetActive(true);
                    shopMiscPanels[2].SetActive(true);
                    */
                }
            }
        }
    }

    public void openBuyWIndow()
    {
        goldAmount.text = 0.ToString();
        if (inventoryPanel.activeInHierarchy)
        {
            inventoryPanel.SetActive(false);
            shopPanel.SetActive(true);
        }
        else
        {
            for (int i = 0; i < shopMiscPanels.Length; i++)
            {
                shopPanel.SetActive(true);
                shopMiscPanels[i].SetActive(true);
            }
        }


    }

    public void changeButtonLabels()
    {
        if (shopPanel.activeInHierarchy)
        {
            buttonLabel.buttonLabel.text = "Buy";
        }
        else
        {
            buttonLabel.buttonLabel.text = "Sell";
        }
    }

    public void addNewShopItem()
    {
        for (int i = 0; i < shopSlots.Length; i++)
        {
            if (shopSlots[i].buttonValue == 0 && shopSlots[i].ItemInSlot.text != Inventory.instance.items[Inventory.instance.itemSelected].itemName)
            {
                print(i + "isEmpty");
                addItems(i, Inventory.instance.items[Inventory.instance.itemSelected]);
                    
            }
            else if (shopSlots[i].ItemInSlot.text == Inventory.instance.items[Inventory.instance.itemSelected].itemName)
            {
                print("Item Already Exist adding Item to that slot");
                addItems(i, Inventory.instance.items[Inventory.instance.itemSelected]);
            }
            i = shopSlots.Length;

        }
        //return false;
    }

    public void addItems(int i, Items itemToAdd)
    {
        if (itemToAdd.itemType == Items.ItemType.WEAPON || itemToAdd.itemType == Items.ItemType.ARMOR)
        {

            shopSlots[i].buttonImage.sprite = itemToAdd.itemImage;
            shopSlots[i].ItemInSlot.text = itemToAdd.itemName;
            shopSlots[i].qtyText.text = (shopSlots[i].buttonValue += qty).ToString();
        }
        else if (itemToAdd.itemType == Items.ItemType.AMMO)
        {

            shopSlots[i].buttonImage.sprite = itemToAdd.itemImage;
            shopSlots[i].ItemInSlot.text = itemToAdd.itemName;
            shopSlots[i].qtyText.text = (shopSlots[i].buttonValue += qty).ToString();

        }
        else if (itemToAdd.itemType == Items.ItemType.CONSUMABLE)
        {

            shopSlots[i].buttonImage.sprite = itemToAdd.itemImage;
            shopSlots[i].ItemInSlot.text = itemToAdd.itemName;
            shopSlots[i].qtyText.text = (shopSlots[i].buttonValue += qty).ToString();
        }
    }

    public void closeShopWindow()
    {
        //shopDialogWindow.SetActive(true);
        shopPanel.SetActive(false);
        for(int i = 0; i < shopMiscPanels.Length; i++)
        {
            inventoryPanel.SetActive(false);
            shopMiscPanels[i].SetActive(false);
        }
    }
}

