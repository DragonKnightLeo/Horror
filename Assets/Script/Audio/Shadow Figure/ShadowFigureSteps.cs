using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowFigureSteps : MonoBehaviour
{
    [SerializeField] SoundManager soundManager;
    // Start is called before the first frame update
    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayShadowySteps()
    {
        soundManager.PlayShadowStep(0.2f);
    }

    public void PlayShadowyAttackScream()
    {
        soundManager.PlayShadowyAttack(0.5f);
    }

}
