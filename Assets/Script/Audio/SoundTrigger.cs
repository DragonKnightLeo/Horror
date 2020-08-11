using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public int sfxToPlay;
    public int loopingSfxToPlay;
    public int sfxToStop;
    public int clipNumber;
    public float volumeScale;
    SoundManager soundManager;
    public GameObject trigger;
    public GameObject trigger2;
    public GameObject objectToDisable;


    private void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (soundManager.sfx[sfxToStop].isPlaying)
            {
                soundManager.sfx[sfxToStop].Stop();
            }
            soundManager.playSfx(sfxToPlay, clipNumber, volumeScale);
            soundManager.playLoopSfx(loopingSfxToPlay);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            trigger.gameObject.SetActive(false);
            if(trigger2 != null)
            {
                soundManager.sfx[trigger2.GetComponent<SoundTrigger>().sfxToPlay].Stop();
                trigger2.gameObject.SetActive(false);
            }
            if (objectToDisable != null)
            {
                objectToDisable.gameObject.SetActive(false);
            }
        }
    }
}
