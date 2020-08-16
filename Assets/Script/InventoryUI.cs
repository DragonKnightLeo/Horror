using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryUI;
    public GameObject backgroundDarken;
    public AudioSource[] audioFX;
    
    void Update()
    {
       if (Input.GetButtonDown("Inventory")) {

            audioFX[0].Play();
            backgroundDarken.GetComponent<Animator>().Play("ScrennBlackout");
            inventoryUI.GetComponent<Animator>().Play("InventoryFadeIn");
            backgroundDarken.SetActive(!backgroundDarken.activeSelf);
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }
}
