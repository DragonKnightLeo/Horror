using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotesBooksTrigger : MonoBehaviour
{
    //[SerializeField] string message;
    [SerializeField] GameObject noteBookPanel;
    //[SerializeField] Text textToDisplay;
    [SerializeField] GameObject noteBookObject;
    [SerializeField] SoundManager soundManager;
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject itemNotification;
    bool canOpen;
    // Start is called before the first frame update
    void Start()
    {
        canOpen = false;
        gameManager = FindObjectOfType<GameManager>();
        soundManager = FindObjectOfType<SoundManager>();
        itemNotification = GameObject.FindGameObjectWithTag("ObjectNotification");
        //textToDisplay = noteBookPanel.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        readNote();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (itemNotification != null)
            {
                itemNotification.GetComponent<Text>().enabled = true;
                itemNotification.GetComponent<Text>().text = "Read";
            }
            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (itemNotification != null)
            {
                itemNotification.GetComponent<Text>().enabled = false;
                itemNotification.GetComponent<Text>().text = null;
            }
            canOpen = false;
        }
    }

    public void readNote()
    {
        if(canOpen)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!noteBookPanel.activeInHierarchy)
                {
                    noteBookPanel.SetActive(true);
                    // soundManager.PlayNoteSfx(1f);
                    // gameManager.noteOpen = true;
                    gameManager.canShoot = false;
                }
                else if (noteBookPanel.activeInHierarchy)
                {

                        noteBookPanel.SetActive(false);
                        // soundManager.PlayNoteSfx(1f);
                        // gameManager.noteOpen = false;
                        gameManager.canShoot = true;
                        gameObject.SetActive(false);
                }
            }

        }

    }
}
