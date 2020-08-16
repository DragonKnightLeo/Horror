using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonCombatNpc : Characters
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override IEnumerator DamageCharacter(float damage, float interval)
    {
        throw new System.NotImplementedException();
    }


}
