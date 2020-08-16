using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    public AudioClip[] clip;

    public AudioSource[] Bgm;

    public AudioSource[] sfx;


    // Start is called before the first frame update
    void Start()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void LateUpdate()
    {
    }
    public void playMusic(int musicToPlay)
    {
        if (!Bgm[musicToPlay].isPlaying)
        {
            stopMusic();

            if (musicToPlay < Bgm.Length)
            {
                Bgm[musicToPlay].Play();
            }
        }
    }

    public void playSfx(int sfxToPlay, int clipNumber, float volumeScale)
    {
            if(sfxToPlay < sfx.Length)
            {
                sfx[sfxToPlay].PlayOneShot(clip[clipNumber], volumeScale);
            }
    }

    public void playLoopSfx(int sfxToPlay)
    {
        if (sfxToPlay < sfx.Length)
        {
            sfx[sfxToPlay].Play();
        }
    }

    public void stopMusic()
    {
        for (int i = 0; i < Bgm.Length; i++)
        {
                Bgm[i].Stop();
        }
    }

    public void uiMainSlotSound()
    {
        sfx[4].Play();
    }

    public void uiWindowsSounds()
    {
        sfx[5].Play();
    }

    public void useItemsSounds()
    {
        sfx[6].Play();
    }

    public void buttonSounds()
    {
        sfx[7].Play();
    }
    public void gunShotSound(bool shooting)
    {
        if (shooting)
        {
            sfx[8].Play();
        }
        else
        {
            sfx[8].Stop();
        }
    }

    public void footSteps(float volumeScale)
    {
        if(!sfx[9].isPlaying)
        {
            sfx[9].PlayOneShot(clip[1], 0.7f);
        }
        else if(sfx[9].isPlaying)
        {
            sfx[9].Stop();
        }
    }

    public void runningSteps(float volumeScale)
    {
        if (!sfx[10].isPlaying)
        {
            sfx[10].PlayOneShot(clip[1], 0.7f);
        }
        else
        {
            sfx[10].Stop();
        }
    }

    public void takingDamageSound()
    {
        if(!sfx[11].isPlaying)
        {
            sfx[11].PlayOneShot(clip[2], 0.7f);
        }
        else
        {
            sfx[11].Stop();
        }
    }

    public void BreathingRun()
    {
            {
                sfx[12].PlayOneShot(clip[3], 0.7f);
            }
    }

    public void monster1AttackSound()
    {
        if (!sfx[15].isPlaying)
        {
            sfx[15].PlayOneShot(clip[4], 0.7f);
        }
    }

    public void monster1GrowlSound()
    {
        if (!sfx[13].isPlaying)
        {
            sfx[13].PlayOneShot(clip[5], 0.7f);
        }
    }

    public void monster1StepsSound()
    {
        if (!sfx[23].isPlaying)
        {
            sfx[23].PlayOneShot(clip[13], 0.7f);
        }
        else
        {
            sfx[23].Stop();
        }
    }

    public void OpenLockedDoor()
    {
        if (!sfx[7].isPlaying)
        {
            sfx[7].PlayOneShot(clip[7], 0.7f);
        }
        else
        {
            sfx[7].Stop();
        }
    }

    public void PlayGhulSteps()
    {
        if (!sfx[23].isPlaying)
        {
            sfx[23].PlayOneShot(clip[13], 0.7f);
        }
    }

    public void PlayShadowStep(float volumeScale)
    {
        if (!sfx[7].isPlaying)
        {
            sfx[7].PlayOneShot(clip[7], volumeScale);
        }
    }

    public void PlayShadowyAttack(float volumeScale)
    {
        if (!sfx[25].isPlaying)
        {
            sfx[25].PlayOneShot(clip[15], volumeScale);
        }
    }
}
