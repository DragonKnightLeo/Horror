using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{

    public static UIFade imageInstance;

    public Image fadeScreen;

    public float fadeSpeed;

    private bool shouldFadeToBlack;

    private bool shouldadeFromBlack;

    // Start is called before the first frame update
    void Start()
    {
        if(imageInstance != null && imageInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            imageInstance = this;
        }

    }

    // Update is called once per frame
    void Update()
    {
        FadingScreen();

    }

    void FadingScreen()
    {
        if(shouldFadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 1f)
            {
                shouldFadeToBlack = false;
            }
        }

        if (shouldadeFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 0f)
            {
                shouldadeFromBlack = false;
            }
        }
    }

    public void FadeToBlack()
    {
        shouldFadeToBlack = true;
        shouldadeFromBlack = false;
    }

    public void FadeFromBlack()
    {
        shouldFadeToBlack = false;
        shouldadeFromBlack = true;
    }
}
