using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGhulSteps : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]SoundManager soundManager;

    void Awake()
    {    }

    // Update is called once per frame

    public void PlayStepSfx()
    {
        soundManager.PlayGhulSteps();
    }

}
