using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    float currentHitPoints;
    float currentStamPoints;
    public Player player;

    [SerializeField] Image hpMeter;
    [SerializeField] Image stamMeter;

    float maxHitPoints;
    float maxStamPoints;

    // Start is called before the first frame update
    void Start()
    {

        player = FindObjectOfType<Player>();
        maxHitPoints = player.maxHitPoints;
        maxStamPoints = player.maxStamPoints;
    }

    // Update is called once per frame
    void Update()
    {
        currentStamPoints = player.currentStamPoints;
        currentHitPoints = player.currentHitPoints;
        print("Max stam:" + player.maxStamPoints);
        print("Current HP: " + player.currentStamPoints);
        if (player != null)
        {
            hpMeter.fillAmount = currentHitPoints / maxHitPoints;
            stamMeter.fillAmount = currentStamPoints / maxStamPoints;

            print("Stam: " + stamMeter.fillAmount);
        }
    }
}
