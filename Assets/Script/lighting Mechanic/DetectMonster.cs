using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class DetectMonster : MonoBehaviour
{
    EnemyController enemy;
    InsEnemyController instantiatedEnemy;
    public Collider2D enemyDetector;
    private LightTrigger lights;

    private void Start()
    {
        lights = FindObjectOfType<LightTrigger>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy = other.gameObject.GetComponent<EnemyController>();
            instantiatedEnemy = other.gameObject.GetComponent<InsEnemyController>();

            instantiatedEnemy.minRange = 6.0f;
            instantiatedEnemy.maxRange = 1.0f;
            enemy.minRange = 6.0f;
            enemy.maxRange = 1.0f;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy = other.gameObject.GetComponent<EnemyController>();
            instantiatedEnemy = other.gameObject.GetComponent<InsEnemyController>();

            enemy.minRange = 2.7f;
            instantiatedEnemy.minRange = 2.7f;
        }
    }

}
