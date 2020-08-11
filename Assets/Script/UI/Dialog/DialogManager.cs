using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    public Text dialogText;

    public Image displayImage;

    public Text nameText;

    public GameObject dialogBox;

    //public GameObject nameBox;

    public string[] dialogLines;

    public int currentLine;

    public bool justStarted;



    // Start is called before the first frame update
    void Start()
    {

        if (instance == null)
        {
            instance = this;
        }

    }

    // Update is called once per frame
    void Update()
    {
        DialogLogic();
    }

    void DialogLogic()
    {
        if (dialogBox.activeInHierarchy)
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                if (!justStarted)
                {

                    currentLine++;
                    if (currentLine >= dialogLines.Length)
                    {
                        dialogBox.SetActive(false);

                        GameManager.gameManagerInstance.dialogActive = false;

                    }
                    else
                    {
                        checkIfName();
                        dialogText.text = dialogLines[currentLine];
                    }
                }
                else
                {
                    justStarted = false;
                }
            }
        }
    }

    public void ShowDialog(string[] newLines, bool isPerson, Sprite image)
    {
        dialogLines = newLines;

        currentLine = 0;

        //checkIfName();

        displayImage.sprite = image;

        dialogText.text = dialogLines[currentLine];

        dialogBox.SetActive(isPerson);

        justStarted = true;

        //nameBox.SetActive(isPerson);

        GameManager.gameManagerInstance.dialogActive = true;
    }

    public void checkIfName()
    {
        if (dialogLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogLines[currentLine].Replace("n-", "");
            currentLine++;
        }
    }
}
