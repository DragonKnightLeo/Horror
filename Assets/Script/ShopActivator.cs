using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopActivator : MonoBehaviour
{
    public string[] characterLines;

    Characters chars;

    private bool canActivate;

    public bool isTalking;

    public bool isPerson = true;

    private void Update()
    {
        displayLines();

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        chars = this.GetComponent<Characters>();

        if (other.tag == "Player")
        {
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canActivate = false;
        }
    }

    void displayLines()
    {
        if (canActivate == true && Input.GetButtonDown("Fire1") && !ShopDialogManager.instance.dialogBox.activeInHierarchy)
        {
            ShopDialogManager.instance.ShowDialog(characterLines, isPerson);
        }
    }
}
