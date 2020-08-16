using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] Button doorSwitch;
    [SerializeField] GameObject lockedDoorImage;
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject unlockedDoor;
    [SerializeField] GameObject lightLimiter;
    [SerializeField] SoundManager soundManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            gameManager.canShoot = false;
            doorSwitch.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gameManager.canShoot = true;
        if(doorSwitch != null)
        {
            doorSwitch.gameObject.SetActive(false);
        }
    }

    public void toggleDoor()
    {
        if (lockedDoorImage.activeInHierarchy)
        {
            soundManager.OpenLockedDoor();
            lockedDoorImage.SetActive(false);
            unlockedDoor.SetActive(true);
        }
        else
        {
            lockedDoorImage.SetActive(true);
            unlockedDoor.SetActive(false);
        }
        if (lightLimiter != null)
        {
            lightLimiter.SetActive(false);
        }
    }



}
