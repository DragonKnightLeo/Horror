using Pathfinding.Ionic.Zip;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    public int sfxToPlay;
    public int sfxToPlay2;
    public int clipNumber;
    public int volumeScale;

    float secPassed;
    [SerializeField] Collider2D sceneRadius;
    [SerializeField] GameObject[] imageToSpawn;
    [SerializeField] GameObject[] imageToDespawn;
    [SerializeField] FrancineBehavior francine;
    [SerializeField] SoundManager soundManager;
    [SerializeField] GameObject trigger;


    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        francine = FindObjectOfType<FrancineBehavior>();
        sceneRadius = GetComponent<Collider2D>();
    }

    private void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < imageToSpawn.Length; i++)
            {
                if (imageToSpawn[i] != null)
                {
                    imageToSpawn[i].SetActive(true);
                }
            }
            for (int i = 0; i < imageToDespawn.Length; i++)
            {
                if (imageToDespawn[i] != null)
                {
                    imageToDespawn[i].SetActive(false);
                }
            }
            sceneRadius.enabled = true;
            soundManager.playSfx(sfxToPlay, clipNumber, 1f);

            secPassed = Time.deltaTime * 100;

            print(secPassed);
            if (secPassed >= 2f)
            {
                soundManager.playSfx(19, 9, 0.5f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            soundManager.playSfx(18, 8, 0.5f);
            trigger.gameObject.SetActive(false);
        }
    }
}
