using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIntensityAdjuster : MonoBehaviour
{
    public static LightIntensityAdjuster instance;
    bool isGeneratorOn;
    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

        }
        DontDestroyOnLoad(gameObject);
    }
    public bool getGlobalLightState()
    {
        return isGeneratorOn;
    }

    public void setGlobalLightState(bool state)
    {
        isGeneratorOn = state;
    }


}
