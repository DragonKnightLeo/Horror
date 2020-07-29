using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    const int numberOfButtonedPanels = 4;
    const int numberOfMenuPanel = 4;
    const int numberOfChars = 4;
    public Button[] mainMenuButtons = new Button[numberOfButtonedPanels];
    [SerializeField] GameObject[] mainMenuWindows = new GameObject[numberOfMenuPanel];
    public Button inventorySlots;
    public GameObject slotsHolder;
    public GameObject inventoryPanel;
    public static MainMenuManager mainMenuInstance;
    public bool activeMenu;
    public GameObject infoLabel;
    public GameObject infoStats;
    public Characters[] charStatInfo = new Characters[numberOfChars];
    public Text[] statusInfo;
    public Text[] statusButtonLabel;
    public Image image;
    public GameObject initialStatusImage;
    public GameObject mainMenuPanel;
    public bool canPressKey;


    
    // Start is called before the first frame update
    void Start()
    {
        initialStatusImage.SetActive(false);
        if (mainMenuInstance != null && mainMenuInstance != this)
        {
            Destroy(mainMenuInstance);
        }
        else
        {
            mainMenuInstance = this;
        }
        charStatInfo = GameManager.gameManagerInstance.charStats;

        //createSlot();
    }

    // Update is called once per frame
    void Update()
    {
        charStatInfo = GameManager.gameManagerInstance.charStats;
        TogglePanelsUsingKeys();
        isMenuOpen();
        MoveMainMenuPanel();
        upDateStats();

    }

    //this function toggles Menu Panels using Key codes on the Keyboard
    public void TogglePanelsUsingKeys()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!mainMenuPanel.activeInHierarchy)
            {
                mainMenuPanel.SetActive(true);
            }
            else
            {
                mainMenuPanel.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!inventoryPanel.activeInHierarchy && canPressKey == true)
            {
               mainMenuWindows[0].SetActive(true);
               mainMenuPanel.SetActive(true);
            }
            else
            {
                canPressKey = true;
                mainMenuWindows[0].SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.C) && canPressKey == true)
        {
            if (!mainMenuWindows[1].activeInHierarchy)
            {
               mainMenuPanel.SetActive(true);
               mainMenuWindows[1].SetActive(true);
            }
            else
            {
                canPressKey = true;
               mainMenuWindows[1].SetActive(false);
            }
        }
            
        
        MoveMainMenuBack();
    }


    public void ToggleMainMenuPanels(int openSelectedWindow)
    {
        for (int i = 0; i < mainMenuWindows.Length; i++)
        {
            if (i == openSelectedWindow)
            {
                mainMenuWindows[i].SetActive(!mainMenuWindows[i].activeInHierarchy);

            }
            else
            {
                mainMenuWindows[i].SetActive(false);
            }
        }
        MoveMainMenuBack();
    }

    public void closeMainMenu()
    {
        mainMenuPanel.SetActive(false);
        for (int i = 0; i < mainMenuWindows.Length; i++)
        {
                mainMenuWindows[i].SetActive(false);
        }
    }
    /*
    public void createSlot(int numSlots)
    {
        int slotLimit = 55;
        if (numSlots <= slotLimit)
        {
            for (int i = 0; i < numSlots; i++)
            {
                Button newSlots = Instantiate(inventorySlots);

                newSlots.transform.SetParent(slotsHolder.transform);
            }
        }
    } 
    public void createSlot()
    {
        for (int i = 0; i < numberOfSlots; i++)
        {
            Button newSlots = Instantiate(inventorySlots);

            newSlots.transform.SetParent(slotsHolder.transform);
        }
    }
    */

    public void isMenuOpen()
    {
        for (int i = 0; i < mainMenuWindows.Length; i++)
        {
            if (mainMenuPanel.activeInHierarchy)
            {
                activeMenu = true;
                canPressKey = false;
            }
            else
            {
                activeMenu = false;
                canPressKey = true;

            }
        }
    }
    public void MoveMainMenuPanel()
    {
        for (int i = 0; i < mainMenuWindows.Length; i++)
        {
            if(mainMenuWindows[i].activeInHierarchy && mainMenuPanel.activeInHierarchy)
            {
                mainMenuPanel.transform.position = new Vector3(222f, 540f, 0f);
            }
        }
        /*
        print("no menu is open");
        mainMenuPanel.transform.position = new Vector3(960f, 540f, 0f); ;
        */
    }

    public void CloseWindows(int selectedWindow)
    {
        mainMenuWindows[selectedWindow].SetActive(false);
    }
    public void MoveMainMenuBack()
    {
        mainMenuPanel.transform.position = new Vector3(960f, 540f, 0f);
    }

    public void showCharStat(int selectedChar)
    {
        initialStatusImage.SetActive(true);
        for (int i = 0; i <= charStatInfo.Length; i++)
        {
           
            if (i == selectedChar)
            {
                statusInfo[0].text = charStatInfo[i].charName;
                statusInfo[1].text = charStatInfo[i].currentHitPoints.ToString();
                statusInfo[2].text = charStatInfo[i].currentManaPoints.ToString();
                statusInfo[3].text = charStatInfo[i].level.ToString();
                image.sprite = charStatInfo[i].charImage;
            }
        }    
    }

    public void upDateStats()
    {
        if (mainMenuWindows[1].activeInHierarchy)
        {
            for (int x = 0; x < mainMenuWindows.Length; x++)
            {
                statusButtonLabel[x].text = charStatInfo[x].charName;
            }
        }
        
    }
}   
