using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.LWRP;

public class LightTrigger : MonoBehaviour
{
    [SerializeField] GameObject thisObject;
    [SerializeField] Light2D lightToTrigger;
    [SerializeField] float triggerLightCost;
<<<<<<< Updated upstream
    [SerializeField] Button lightSwitch;
=======
    [SerializeField] GameObject objectNotification;
>>>>>>> Stashed changes
    bool canActivate;

    private void Awake()
    {
<<<<<<< Updated upstream
      canActivate =  false;
    }

    private void Update()
    {
=======
    }

    private void Start()
    {
        objectNotification = GameObject.FindGameObjectWithTag("ObjectNotification");
    }
    private void Update()
    {
        triggerLight();
>>>>>>> Stashed changes
    }

    public void triggerLight()
    {
<<<<<<< Updated upstream
            if (lightToTrigger != null && !lightToTrigger.gameObject.activeInHierarchy)
            {
                Player.playerInstance.lightTimer -= triggerLightCost;
                lightToTrigger.gameObject.SetActive(true);
            }
            else
            {
                lightToTrigger.gameObject.SetActive(false);
            }
=======
        if (canActivate)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (lightToTrigger != null && !lightToTrigger.gameObject.activeInHierarchy)
                {
                    Player.playerInstance.lightTimer -= triggerLightCost;
                    lightToTrigger.gameObject.SetActive(true);
                }
                else
                {
                    lightToTrigger.gameObject.SetActive(false);
                }
            }
        }
>>>>>>> Stashed changes
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
<<<<<<< Updated upstream
            lightSwitch.gameObject.SetActive(true);
            canActivate = true;
        }
        GameManager.gameManagerInstance.canShoot = false;
=======
            objectNotification.GetComponent<Text>().enabled = true;
            if (!lightToTrigger.gameObject.activeInHierarchy)
            {
                objectNotification.GetComponent<Text>().text = "Turn Light On";
            }
            else
            {
                objectNotification.GetComponent<Text>().text = "Turn Light Off";
            }
            canActivate = true;
        }
>>>>>>> Stashed changes
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
<<<<<<< Updated upstream
            lightSwitch.gameObject.SetActive(false);
        }
        GameManager.gameManagerInstance.canShoot = true;
=======
            objectNotification.GetComponent<Text>().enabled = false;
            objectNotification.GetComponent<Text>().text = null;
            canActivate = false;
        }
>>>>>>> Stashed changes
    }
}
