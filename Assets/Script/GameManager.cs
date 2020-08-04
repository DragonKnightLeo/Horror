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
    public bool fadingBetweenAreas;
    public bool dialogActive;
    public Characters[] charStats = new Characters[numberOfPartyMembers];
    public PlayerMovement playerMovement;
    public Light2D globalLight;
    public Light2D InnerWallIndicators;
    public Light2D OuterWallIndicators;
    public Light2D DoorExitLighting;


    // Start is called before the first frame update
    void Start()
    {
        if (gameManagerInstance != null && gameManagerInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            gameManagerInstance = this;

        }
        DontDestroyOnLoad(gameObject);

        globalLight.intensity = globalLightIntesity;
        InnerWallIndicators.intensity = indicatorLightIntensity;
        OuterWallIndicators.intensity = indicatorLightIntensity;
        DoorExitLighting.intensity = indicatorLightIntensity;


}

    // Update is called once per frame
    private void FixedUpdate()
    {
        StopMovement();
    }

    void StopMovement()
    {

        bool activeMenu = MainMenu.instance.activeMenu;


        if (activeMenu || fadingBetweenAreas || dialogActive)
        {
            playerMovement.moveCharacter(false);
            playerMovement.shootAnimation(false);
        }
        else
        {
            playerMovement.moveCharacter(true);
            playerMovement.shootAnimation(true);
        }
    }
}
