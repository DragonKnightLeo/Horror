using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShot : MonoBehaviour
{
    public AudioClip gunShot;

    public AudioSource sfx;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pistolSound();
    }

    public void pistolSound()
    {
       sfx.PlayDelayed(0.4f);
    }
}
