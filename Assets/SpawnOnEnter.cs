using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnEnter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] SpawnPoint spawnPoint;
    [SerializeField] GameObject spawnObject;
    [SerializeField] GameObject soundTrigger;
    [SerializeField] GameObject objectDeActivator;
    void Start()
    {
        spawnObject = GameObject.FindGameObjectWithTag("MonsterSpawnPoint");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        spawnPoint = spawnObject.GetComponentInChildren<SpawnPoint>();
        spawnPoint.enabled = true; 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spawnPoint =  spawnObject.GetComponentInChildren<SpawnPoint>();
            soundTrigger.gameObject.SetActive(true);
            objectDeActivator.gameObject.SetActive(true);
            spawnPoint.enabled = true;
        }
    }

}
