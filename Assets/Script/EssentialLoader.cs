using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialLoader : MonoBehaviour
{
    public GameObject UIScreen;

    public GameObject player;

    public GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        if (UIFade.imageInstance == null)
        {
            UIFade.imageInstance = Instantiate(UIScreen).GetComponent<UIFade>();
        }


        if (Player.playerInstance == null)
        {
            Instantiate(player);
        }

        if (GameManager.gameManagerInstance == null)
        {
            Instantiate(gameManager);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
