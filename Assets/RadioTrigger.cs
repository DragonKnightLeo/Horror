using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class RadioTrigger : MonoBehaviour
{
    float time = 30f;
    public int firstSfxToPlay;
    public int firstClip;
    public int secondSfxToPlay;
    public int thirdSfxToPlay;
    public int secondClip;
    bool canToggleRadio;
    int playerEnterCounter;
    [SerializeField] GameManager gameManager;
    [SerializeField] SoundManager soundManager;
    [SerializeField] GameObject objectNotification;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        soundManager = FindObjectOfType<SoundManager>();
        objectNotification = GameObject.FindGameObjectWithTag("ObjectNotification");
    }

    private void Update()
    {
        toggleRadio();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        { 
            if (!soundManager.sfx[firstSfxToPlay].isPlaying)
            {
                if (objectNotification != null)
                {
                    objectNotification.GetComponent<Text>().enabled = true;
                    objectNotification.GetComponent<Text>().text = "Toggle Radio";
                }
                canToggleRadio = true;
            } 
            gameManager.canShoot = false;
        }
    }

    private void FixedUpdate()
    {
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            if (objectNotification != null)
            {
                objectNotification.GetComponent<Text>().enabled = false;
                objectNotification.GetComponent<Text>().text = null;
            }
            gameManager.canShoot = true;

            if(playerEnterCounter > 1)
            {
                StartCoroutine(autoPlaySound());
            }
            canToggleRadio = false;
        }
    }

    public void  toggleRadio()
    {
        if (canToggleRadio)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                playerEnterCounter++;
                if (playerEnterCounter <= 1 && !soundManager.sfx[firstSfxToPlay].isPlaying)
                {
                    soundManager.playLoopSfx(firstSfxToPlay);
                    StartCoroutine(playSound());
                }
                if (playerEnterCounter > 1 && soundManager.sfx[thirdSfxToPlay].isPlaying)
                {
                    soundManager.sfx[thirdSfxToPlay].Stop();
                }
                if (playerEnterCounter > 1 && soundManager.sfx[secondSfxToPlay].isPlaying)
                {
                    soundManager.sfx[secondSfxToPlay].Stop();
                }
            }
        }

    }

    IEnumerator playSound()
    {
        yield return new WaitForSeconds(soundManager.sfx[firstSfxToPlay].clip.length + 0.5f);
        soundManager.playLoopSfx(thirdSfxToPlay);
    }

    IEnumerator autoPlaySound()
    {
        yield return new WaitForSeconds(3.0f);
        soundManager.playLoopSfx(secondSfxToPlay);
    }


}
