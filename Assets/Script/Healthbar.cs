using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    float currentHitPoints;
    float currentStamPoints;
    float lightTimer;
    public Text timer;
    public Player player;
    public static Healthbar instance;

    [SerializeField] Image hpMeter;
    [SerializeField] Image stamMeter;
    public Image[] bulletImage;

    float maxHitPoints;
    float maxStamPoints;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        maxHitPoints = player.maxHitPoints;
        maxStamPoints = player.maxStamPoints;

        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentStamPoints = player.currentStamPoints;
        currentHitPoints = player.currentHitPoints;
        lightTimer = player.lightTimer;
        if (player != null)
        {
            hpMeter.fillAmount = currentHitPoints / maxHitPoints;
            stamMeter.fillAmount = currentStamPoints / maxStamPoints;
        }

        timer.text = Mathf.Round(lightTimer / 60).ToString();

    }
}
