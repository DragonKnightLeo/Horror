using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Experimental.Rendering.LWRP;


//This 
public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance;
    const int numberOfPartyMembers = 4;
    public float globalLightIntesity;
    public float indicatorLightIntensity;
    bool canAttack;
    public bool canShoot;
    public bool fadingBetweenAreas;
    public bool dialogActive;
    public Characters[] charStats = new Characters[numberOfPartyMembers];
    [SerializeField]PlayerMovement playerMovement;
    [SerializeField] GameObject globalLightInstance;
    [SerializeField] GameObject lightLimiter;
    [SerializeField] LightIntensityAdjuster generatorState;
    [SerializeField] SpawnPoint[] spawnPoints;
    public Light2D globalLight;
    //public Light2D DoorExitLighting;


    // Start is called before the first frame update

    private void Awake()
    {
        canShoot = true;
        if (gameManagerInstance != null && gameManagerInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            gameManagerInstance = this;

        }
        DontDestroyOnLoad(gameObject);
        canShoot = true;
        playerMovement = FindObjectOfType<PlayerMovement>();
        globalLightInstance = GameObject.FindGameObjectWithTag("GlobalLight");
        globalLight = globalLightInstance.gameObject.GetComponent<Light2D>();
        generatorState = FindObjectOfType<LightIntensityAdjuster>();
        if (lightLimiter != null)
        {
            lightLimiter.SetActive(true);
        }
        globalLight.intensity = globalLightIntesity;
    }
    void Start()
    {
        spawnPoints = FindObjectsOfType<SpawnPoint>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        globalLightInstance = GameObject.FindGameObjectWithTag("GlobalLight");
        globalLight = globalLightInstance.gameObject.GetComponent<Light2D>();
        generatorState = FindObjectOfType<LightIntensityAdjuster>();
        globalLight.intensity = globalLightIntesity;
        //spawnPoints[0].SpawnObject();

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        StopMovement();
        globalLightState();
    }

    void StopMovement()
    {

        bool activeMenu = MainMenu.instance.activeMenu;


        if (activeMenu || fadingBetweenAreas || dialogActive)
        {
            playerMovement.moveCharacter(false);
            playerMovement.shootAnimation(canShoot);
        }
        else
        {
            playerMovement.moveCharacter(true);
            playerMovement.shootAnimation(canShoot);
        }
    }

    void globalLightState()
    {
        if(generatorState.getGlobalLightState() == true)
        {
           globalLight.intensity = 0.35f;
        }
    }
}
