using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;
    const int numberOFWindows = 4;
    [SerializeField] GameObject mainMenuPanel;
    public GameObject[] mainMenuWindows = new GameObject[numberOFWindows];
    bool canClick;
    public bool activeMenu;
    // Start is called before the first frame update
    void Start()
    {
        if(instance != null && instance != this)
        {
            Destroy(instance);
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
        openMainMenu();
        openInventory();
        openStatWindow();
        moveMainMenu();
        isMenuActive();
        //menuToCenter();
    }

    public void openMainMenu()
    {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SoundManager.instance.uiMainSlotSound();
            if (!mainMenuPanel.activeInHierarchy)
            {
                mainMenuActivate();
            }
            else
            {
                mainMenuDeactivate();
                for (int i = 0; i < mainMenuWindows.Length; i++)
                {
                    mainMenuWindows[i].SetActive(false);
                }
            }
            
        }
    }

    void openInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SoundManager.instance.uiMainSlotSound();
            if (!mainMenuWindows[0].activeInHierarchy && canClick == true)
            {
                mainMenuWindows[0].SetActive(true);
                mainMenuActivate();

            }
            else
            {
                    mainMenuWindows[0].SetActive(false);
            }

        }
        menuToCenter();
    }

    void openStatWindow()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SoundManager.instance.uiMainSlotSound();
            if (!mainMenuWindows[1].activeInHierarchy && canClick == true)
            {
                mainMenuWindows[1].SetActive(true);
                mainMenuActivate();
            }
            else
            {
                mainMenuWindows[1].SetActive(false);
            }

        }
        menuToCenter();
    }

    public void closeMainMenu()
    {
        for (int i = 0; i < mainMenuWindows.Length; i++)
        {
                mainMenuDeactivate();
            if (mainMenuWindows[i].activeInHierarchy)
            {
                mainMenuWindows[i].SetActive(false);
            }
        }
    }
    
    public void moveMainMenu()
    {
        for(int i = 0; i < mainMenuWindows.Length; i++)
        {
            if(mainMenuWindows[i].activeInHierarchy && mainMenuPanel.activeInHierarchy)
            {
                mainMenuPanel.transform.position = new Vector3(222f, 540f, 0f);
            }
        }
    }

    public void menuToCenter()
    {
        mainMenuPanel.transform.position = new Vector3(960f, 540f, 0f);
    }

    public void closeMenuWindows(int selectedWinow)
    {
        SoundManager.instance.uiMainSlotSound();
        mainMenuWindows[selectedWinow].SetActive(false);
    }

    public void mainMenuActivate()
    {

        mainMenuPanel.SetActive(true);
    }
    public void mainMenuDeactivate()
    {
        mainMenuPanel.SetActive(false);
    }

    public void toggleMenuWindows(int selectedWindows)
    {   
        SoundManager.instance.uiMainSlotSound();
        for (int i = 0; i < mainMenuWindows.Length; i++)
        {
            if (mainMenuWindows[i].activeInHierarchy)
            {
                mainMenuWindows[i].SetActive(false);
            }
            else
            {
                mainMenuWindows[selectedWindows].SetActive(true);
            }
            
        }
    }


    public void isMenuActive()
    {
        for (int i = 0; i < mainMenuWindows.Length; i++)
        {
            if(mainMenuPanel.activeInHierarchy)
            {
                activeMenu = true;
                canClick = false;
            }
            else
            {
                activeMenu = false;
                canClick = true;
            }
        }
    }




}
