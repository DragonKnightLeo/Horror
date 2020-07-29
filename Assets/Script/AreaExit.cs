using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    public string areaToLoad;
    public int musicToPlay;

    public int music2ToPlay;
    public string areaTransitionName;
    public AreaEntrance theEntrance;
    public float waitToLoad = 1f;
    private bool shouldLoadAfterFade;
    SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        theEntrance.transitionName = areaTransitionName;


        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        soundManager.playMusic(musicToPlay);
        //soundManager.playMusic();
    }

    // Update is called once per frame
    void Update()
    {
        fadeScreen();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        { 
            shouldLoadAfterFade = true;
            GameManager.gameManagerInstance.fadingBetweenAreas = true;
            UIFade.imageInstance.FadeToBlack();
            Player.playerInstance.areaToTransitionName = areaTransitionName;
        }
    }

    void fadeScreen()
    {
        if (shouldLoadAfterFade)
        {
            waitToLoad -= Time.deltaTime;
            if (waitToLoad <= 0)
            {
                shouldLoadAfterFade = false;
                SceneManager.LoadScene(areaToLoad);
            }

        }
    }
}
