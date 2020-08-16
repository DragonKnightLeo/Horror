using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSprite : MonoBehaviour
{
    // Start is called before the first frame update
    public static BloodSprite instance;

    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
