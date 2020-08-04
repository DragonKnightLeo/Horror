using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.LWRP;

public class LightTrigger : MonoBehaviour
{
    public Light2D lightToTrigger;
    public float triggerLightCost;
    public Button lightSwitch;
    public void triggerLight()
    {
        Player.playerInstance.lightTimer -= triggerLightCost;
        if (lightToTrigger != null && !lightToTrigger.gameObject.activeInHierarchy)
        {
            lightToTrigger.gameObject.SetActive(true);
        }
        else
        {
            lightToTrigger.gameObject.SetActive(false);
        }
        PlayerMovement.playerMovementInstance.animator.SetBool("isShooting", false);
        PlayerMovement.playerMovementInstance.setBulletsOff(0, false);
        PlayerMovement.playerMovementInstance.playGunSound(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            lightSwitch.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            lightSwitch.gameObject.SetActive(false);
        }
    }
}
