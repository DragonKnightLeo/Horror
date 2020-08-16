using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogActivator : MonoBehaviour
{
    public string[] characterLines;

<<<<<<< Updated upstream
    Characters chars;

=======
>>>>>>> Stashed changes
    private bool canActivate;

    [SerializeField] bool justStarted;

    int currentLines;

    public bool isPerson = true;

    public Sprite characterImage;

<<<<<<< Updated upstream
    [SerializeField] GameObject dialogImage;
    [SerializeField] GameObject dialogPanel;
    [SerializeField] GameObject dialogText;
    [SerializeField] Image displayPanel;
    [SerializeField] Image displayImage;
    [SerializeField] Text displayText;
=======
    [SerializeField] GameObject canvasObject;
    [SerializeField] GameObject dialogPanel;
    [SerializeField] GameObject dialogImage;
    [SerializeField] GameObject dialogText;
    [SerializeField] GameObject objectNotification;

>>>>>>> Stashed changes
    public bool conversationEnded;



    private void Awake()
    {
<<<<<<< Updated upstream
=======
        currentLines = -1;
>>>>>>> Stashed changes
    }

    private void Start()
    {
<<<<<<< Updated upstream
        dialogPanel = GameObject.FindGameObjectWithTag("DialogPanel");
        dialogImage = GameObject.FindGameObjectWithTag("DialogImage");
        dialogText = GameObject.FindGameObjectWithTag("DialogText");
        conversationEnded = false;
=======
        conversationEnded = false;
        objectNotification = GameObject.FindGameObjectWithTag("ObjectNotification");
>>>>>>> Stashed changes
    }

    private void Update()
    {
<<<<<<< Updated upstream
        DialogLogic();
        displayLines();
=======
        if (canActivate)
        {
            DialogLogic();
            displayLines();
        }
>>>>>>> Stashed changes
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
<<<<<<< Updated upstream
            print("reached Dialog");
=======
            if (objectNotification != null)
            {
                objectNotification.GetComponent<Text>().enabled = true;
                if (isPerson)
                {
                    objectNotification.GetComponent<Text>().text = "Talk";
                }
                else
                {
                    objectNotification.GetComponent<Text>().text = "Inspect";
                }
            }
>>>>>>> Stashed changes
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
<<<<<<< Updated upstream
=======
            if (objectNotification != null)
            {
                objectNotification.GetComponent<Text>().text = null;
            }
>>>>>>> Stashed changes
            canActivate = false;
        }
    }

    public void displayLines()
    {
<<<<<<< Updated upstream
        if (canActivate == true && Input.GetKeyDown(KeyCode.F) && !dialogPanel.GetComponent<Image>().enabled)
        {
            print("Here");
=======
        if (canActivate == true && Input.GetKeyDown(KeyCode.F) && !dialogPanel.activeInHierarchy)
        {
>>>>>>> Stashed changes
            showDialog();
        }
    }

    void showDialog()
    {
        MainMenu.instance.mainMenuWindows[0].gameObject.SetActive(false);
        Healthbar.instance.gameObject.SetActive(false);
        if (currentLines < characterLines.Length)
        {
<<<<<<< Updated upstream
            dialogPanel.GetComponent<Image>().enabled = true;
            dialogText.GetComponent<Text>().enabled = true;
            dialogImage.GetComponent<Image>().enabled = true;
            dialogImage.GetComponent<Image>().sprite = characterImage;
            dialogText.GetComponent<Text>().text = characterLines[currentLines];
            justStarted = true;
            GameManager.gameManagerInstance.dialogActive = true;
        }
=======
            dialogPanel.SetActive(true);
            dialogText.SetActive(true);
            dialogImage.SetActive(true);
            dialogImage.GetComponent<Image>().sprite = characterImage;
            dialogText.GetComponent<Text>().text = characterLines[0];
            justStarted = true;
            GameManager.gameManagerInstance.dialogActive = true;
            GameManager.gameManagerInstance.canShoot = false;
        }
        objectNotification.GetComponent<Text>().enabled = false;
>>>>>>> Stashed changes
    }

    public void DialogLogic()
    {
<<<<<<< Updated upstream
        if (dialogPanel.GetComponent<Image>().enabled)
        {
=======
>>>>>>> Stashed changes
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!justStarted)
                {
                    currentLines++;
<<<<<<< Updated upstream

                    if (currentLines >= characterLines.Length)
                    {
                        dialogPanel.gameObject.SetActive(false);
                        GameManager.gameManagerInstance.dialogActive = false;
=======
                    if (currentLines >= characterLines.Length)
                    {
                        GameManager.gameManagerInstance.dialogActive = false;
                        GameManager.gameManagerInstance.canShoot = true;
                        dialogPanel.gameObject.SetActive(false);
>>>>>>> Stashed changes
                        MainMenu.instance.mainMenuWindows[0].gameObject.SetActive(true);
                        Healthbar.instance.gameObject.SetActive(true);
                        conversationEnded = true;
                    }
                    else
                    {
<<<<<<< Updated upstream
                        dialogPanel.GetComponentInChildren<Text>().text = characterLines[currentLines];
=======
                        dialogText.GetComponentInChildren<Text>().text = characterLines[currentLines];
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
        }
=======
>>>>>>> Stashed changes

    }
}

