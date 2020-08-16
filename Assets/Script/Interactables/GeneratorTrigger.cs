using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.LWRP;

public class GeneratorTrigger : MonoBehaviour
{
    public float triggerLightCost;
    [SerializeField] GameObject lightSwitch;
    [SerializeField] GameManager gameManager;
    [SerializeField] LightIntensityAdjuster generatorState;
    [SerializeField] PlayerMovement playerMovement;


    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        generatorState = FindObjectOfType<LightIntensityAdjuster>();
    }

    public void triggerLight()
    {
        Player.playerInstance.lightTimer -= triggerLightCost;
        generatorState.setGlobalLightState(true);
        playerMovement.adjustIntensity(0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            lightSwitch.gameObject.SetActive(true);
            generatorState.gameObject.SetActive(false);
        }
        gameManager.canShoot = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            lightSwitch.gameObject.SetActive(false);
        }
        gameManager.canShoot = true;
    }


}
