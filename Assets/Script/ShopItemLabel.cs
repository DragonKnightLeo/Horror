using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemLabel : MonoBehaviour
{
    [SerializeField] Text itemName;

    
    public Text getItemName()
    {
        return itemName;
    }

    public void setItemName(string nameOfItem)
    {
        itemName.text = nameOfItem;
    }
}
