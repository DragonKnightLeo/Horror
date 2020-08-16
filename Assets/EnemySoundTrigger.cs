using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundTrigger : MonoBehaviour
{
    public int sfxToPlay;
    public int clipNumber;
    public float volumeScale;

    [SerializeField] SoundManager soundManager;
    [SerializeField] GameObject trigger;


    private void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            soundManager.playSfx(sfxToPlay, clipNumber, volumeScale);
        }
        gameObject.SetActive(false);
    }
}
