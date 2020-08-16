using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject openDrawerImage;
    [SerializeField] GameObject closedDrawerImage;
    [SerializeField] Transform itemSpawnLocation;
    [SerializeField] GameObject[] itemToSpawn;
    [SerializeField] GameObject objectNotification;
    [SerializeField] GameManager gameManager;
    bool canOpen;
    public int numItemToSpawn;
    

    private void Awake()
    {
        numItemToSpawn = 1;
    }
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        objectNotification = GameObject.FindGameObjectWithTag("ObjectNotification");
    }

    private void Update()
    {
        if (canOpen)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                spawnItem();
            }
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.canShoot = false;
            if (objectNotification != null)
            {
                objectNotification.GetComponent<Text>().enabled = true;
                if (closedDrawerImage.activeInHierarchy)
                {
                    objectNotification.GetComponent<Text>().text = "Open";
                }
            }
            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (objectNotification != null)
        {
            objectNotification.GetComponent<Text>().text = null;

        }
    }

    public void spawnItem()
    {
        if (openDrawerImage != null && closedDrawerImage != null)
        {
            for (int i = 0; i < itemToSpawn.Length; i++)
            {
                if (itemToSpawn != null && numItemToSpawn >= 1)
                {
                    numItemToSpawn--;
                    Instantiate(itemToSpawn[i], itemSpawnLocation.position, Quaternion.identity);
                    print("reached");
                }
            }
            if(!openDrawerImage.activeInHierarchy)
            {
                openDrawerImage.SetActive(true);
                closedDrawerImage.SetActive(false);
                objectNotification.GetComponent<Text>().enabled = false;
            }
        }
    }
}
