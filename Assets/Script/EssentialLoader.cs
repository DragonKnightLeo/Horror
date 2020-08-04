using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialLoader : MonoBehaviour
{
    //public GameObject UIScreen;

    public GameObject player;

    public GameObject gameManager;

    public GameObject globalLight;

    public GameObject wallIndicator;

    public GameObject healthBar;

    public GameObject horrorUI;

    public GameObject mainCamera;

    public GameObject virtualCamera;

    public GameObject soundManager;




    // Start is called before the first frame update
    void Start()
    {
        /*
        if (UIFade.imageInstance == null)
        {
            UIFade.imageInstance = Instantiate(UIScreen).GetComponent<UIFade>();
        }
        */

        if (Player.playerInstance == null)
        {
            Instantiate(player);
        }

        if (GameManager.gameManagerInstance == null)
        {
            Instantiate(gameManager);
        }

        if(MainMenu.instance == null)
        {
            Instantiate(horrorUI);
        }

        if(Healthbar.instance == null)
        {
            Instantiate(healthBar);
        }

        if(VirtualCam.instance == null)
        {
            Instantiate(virtualCamera);
        }

        if(MainCam.instance == null)
        {
            Instantiate(mainCamera);
        }
        
        if(SoundManager.instance == null)
        {
            Instantiate(soundManager);
        }
    }

}
