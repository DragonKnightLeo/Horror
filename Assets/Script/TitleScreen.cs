using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public AudioSource buttonSound;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    public void HoverSound()
    {
        buttonSound.PlayOneShot (hoverSound);
    }
    public void ClickSound()
    {
        buttonSound.PlayOneShot (clickSound);
    }
}
