using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorTrigger : MonoBehaviour
{
    bool canToggle;
    public bool keyNeeded; 
    [SerializeField] GameObject objectNotification;
    [SerializeField] GameObject lockedDoorImage;
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject unlockedDoor;
    [SerializeField] GameObject lightLimiter;
    [SerializeField] SoundManager soundManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        soundManager = FindObjectOfType<SoundManager>();
        objectNotification = GameObject.FindGameObjectWithTag("ObjectNotification");
    }

    private void Update()
    {
       toggleDoor();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (objectNotification != null)
            {
                objectNotification.GetComponent<Text>().enabled = true;
            }

            if(lockedDoorImage.activeInHierarchy)
            {
                objectNotification.GetComponent<Text>().text = "Open Door";
            }
            else
            {
                objectNotification.GetComponent<Text>().text = "Close Door";
            }
            canToggle = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (objectNotification != null)
        {
            objectNotification.GetComponent<Text>().enabled = false;
            objectNotification.GetComponent<Text>().text = null;
        }
        canToggle = false;
    }
    public void toggleDoor()
    {
        if (canToggle)
        {
            if (Input.GetKeyDown(KeyCode.F))
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
    }



}
