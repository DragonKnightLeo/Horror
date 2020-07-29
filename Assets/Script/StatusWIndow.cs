using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusWIndow : MonoBehaviour
{
    public static StatusWIndow instance;
    const int numberOfChar = 3;
    const int numberOfstats = 6;
    [SerializeField] Text[] statButtonLabel = new Text[numberOfChar];
    [SerializeField] Text[] statValues = new Text[numberOfstats];
    [SerializeField ]GameObject initialImage;
    [SerializeField] Characters[] characters;

    public Image charImage;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        characters = GameManager.gameManagerInstance.charStats;
        setUPStatWinOnStart();
    }

    // Update is called once per frame
    void Update()
    { 
    }

    public void updateStats(int selectedChar)
    {
        initialImage.SetActive(true);
        for (int i = 0; i < statValues.Length; i++)
        {
            if(i == selectedChar)
            {
                statValues[0].text = characters[selectedChar].charName;
                statValues[1].text = characters[selectedChar].currentHitPoints + "/" + characters[selectedChar].maxHitPoints;
                statValues[2].text = characters[selectedChar].currentManaPoints + "/" + characters[selectedChar].maxManaPoints;
                statValues[3].text = characters[selectedChar].level.ToString();
                statValues[4].text = characters[selectedChar].attackPower.ToString();
                statValues[5].text = characters[selectedChar].armorPower.ToString();
                charImage.sprite = characters[selectedChar].charImage;
            }
        }
    }

    public void setUPStatWinOnStart()
    {
        initialImage.SetActive(false);
        for (int i = 0; i < characters.Length; i++)
        {
            statButtonLabel[i].text = characters[i].charName;
        }
        statValues[0].text = "";
        statValues[1].text = "";
        statValues[2].text = "";
        statValues[3].text = "";
        statValues[4].text = "";
        statValues[5].text = "";
    }

}
