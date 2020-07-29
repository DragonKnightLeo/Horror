using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    public string transitionName;

    // Start is called before the first frame update
    void Start()
    {
        if (transitionName == Player.playerInstance.areaToTransitionName)
        {
            Player.playerInstance.transform.position = transform.position;
        }
        UIFade.imageInstance.FadeFromBlack();
        GameManager.gameManagerInstance.fadingBetweenAreas = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadInToZone()
    {
        if(transitionName == Player.playerInstance.areaToTransitionName)
        {
            Player.playerInstance.transform.position = transform.position;
        }
        UIFade.imageInstance.FadeFromBlack();
        GameManager.gameManagerInstance.fadingBetweenAreas = false;
    }

}
