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
    [SerializeField] Button lightSwitch;
    bool canActivate;

    private void Awake()
    {
      canActivate =  false;
    }

    private void Update()
    {
    }

    public void triggerLight()
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            lightSwitch.gameObject.SetActive(true);
            canActivate = true;
        }
        GameManager.gameManagerInstance.canShoot = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            lightSwitch.gameObject.SetActive(false);
        }
        GameManager.gameManagerInstance.canShoot = true;
    }
}
