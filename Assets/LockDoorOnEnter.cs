using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoorOnEnter : MonoBehaviour
{
    [SerializeField]GameObject lockedDoor;
    [SerializeField] SoundManager soundManager;

    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            lockedDoor.SetActive(true);

        }
    }
}
