using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SerializeCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    public static SerializeCharacter instance;

    private void Awake()
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

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
