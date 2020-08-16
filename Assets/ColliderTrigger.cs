using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] colliderToTrigger;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for (int i = 0; i < colliderToTrigger.Length; i++)
            {
                if (!colliderToTrigger[i].activeInHierarchy)
                {
                    colliderToTrigger[i].SetActive(true);
                }
                gameObject.SetActive(false);
            }
        }
    }
}
