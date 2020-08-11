using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogActivator : MonoBehaviour
{
    public string[] characterLines;

    Characters chars;

    private bool canActivate;

    [SerializeField] bool justStarted;

    int currentLines;

    public bool isPerson = true;

    public Sprite characterImage;

    [SerializeField] GameObject dialogImage;
    [SerializeField] GameObject dialogPanel;
    [SerializeField] GameObject dialogText;
    [SerializeField] Image displayPanel;
    [SerializeField] Image displayImage;
    [SerializeField] Text displayText;
    public bool conversationEnded;



    private void Awake()
    {
    }

    private void Start()
    {
        dialogPanel = GameObject.FindGameObjectWithTag("DialogPanel");
        dialogImage = GameObject.FindGameObjectWithTag("DialogImage");
        dialogText = GameObject.FindGameObjectWithTag("DialogText");
        conversationEnded = false;
    }

    private void Update()
    {
        DialogLogic();
        displayLines();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            print("reached Dialog");
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canActivate = false;
        }
    }

    public void displayLines()
    {
        if (canActivate == true && Input.GetKeyDown(KeyCode.F) && !dialogPanel.GetComponent<Image>().enabled)
        {
            print("Here");
            showDialog();
        }
    }

    void showDialog()
    {
        MainMenu.instance.mainMenuWindows[0].gameObject.SetActive(false);
        Healthbar.instance.gameObject.SetActive(false);
        if (currentLines < characterLines.Length)
        {
            dialogPanel.GetComponent<Image>().enabled = true;
            dialogText.GetComponent<Text>().enabled = true;
            dialogImage.GetComponent<Image>().enabled = true;
            dialogImage.GetComponent<Image>().sprite = characterImage;
            dialogText.GetComponent<Text>().text = characterLines[currentLines];
            justStarted = true;
            GameManager.gameManagerInstance.dialogActive = true;
        }
    }

    public void DialogLogic()
    {
        if (dialogPanel.GetComponent<Image>().enabled)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!justStarted)
                {
                    currentLines++;

                    if (currentLines >= characterLines.Length)
                    {
                        dialogPanel.gameObject.SetActive(false);
                        GameManager.gameManagerInstance.dialogActive = false;
                        MainMenu.instance.mainMenuWindows[0].gameObject.SetActive(true);
                        Healthbar.instance.gameObject.SetActive(true);
                        conversationEnded = true;
                    }
                    else
                    {
                        dialogPanel.GetComponentInChildren<Text>().text = characterLines[currentLines];
                    }
                }
                else
                {
                    justStarted = false;
                }
            }
            else
            {
                justStarted = false;
            }
        }

    }
}

