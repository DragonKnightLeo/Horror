using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deinstantiate : MonoBehaviour
{
    [SerializeField] GameObject[] objectToDeinstantiate;



    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            for (int i = 0; i < objectToDeinstantiate.Length; i++)
            {
                objectToDeinstantiate[0].SetActive(false);
                objectToDeinstantiate[1].SetActive(false);
            }
            gameObject.SetActive(false);
        }
    }
}
