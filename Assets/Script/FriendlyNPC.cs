using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendlyNPC : Characters
{
    
    // Start is called before the first frame update
    void Start()
    {   
    }

    // Update is called once per frame
    void Update()
    {
        //instanceImage.sprite = charImage;
    }

    public override IEnumerator DamageCharacter(float damage, float interval)
    {
        throw new System.NotImplementedException();
    }
}
