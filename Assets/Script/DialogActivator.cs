using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogActivator : MonoBehaviour
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
        if (canActivate == true && Input.GetKeyDown(KeyCode.F) && !DialogManager.instance.dialogBox.activeInHierarchy && chars.characterCategory == Characters.CharacterCategory.NPC)
        {
            DialogManager.instance.ShowDialog(characterLines, isPerson);
        }
        else
        {

        }
    }
}

