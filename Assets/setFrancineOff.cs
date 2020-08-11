using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setFrancineOff : MonoBehaviour
{
    [SerializeField] LightIntensityAdjuster generatorState;
    [SerializeField] FrancineBehavior francine;


    private void Start()
    {
        francine = FindObjectOfType<FrancineBehavior>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(francine.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }

    }
}
