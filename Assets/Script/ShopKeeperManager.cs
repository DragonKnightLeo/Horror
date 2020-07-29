using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeperManager : Characters
{
    [SerializeField] Items[] itemsSoldInShop;

    [SerializeField] Items[] itemsCouldBeSoldInShop;
    int numItem = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Items[] getItemInShop()
    {
        return itemsSoldInShop;
    }

    public int getNumItem()
    {
        return numItem;
    }

    public Items[] getItemsCouldBeSoldInShop()
    {
        return itemsCouldBeSoldInShop;
    }
    public void setNumItem(int x)
    {
        numItem = x;
    }

    public override IEnumerator DamageCharacter(float damage, float interval)
    {
        throw new System.NotImplementedException();
    }
}
