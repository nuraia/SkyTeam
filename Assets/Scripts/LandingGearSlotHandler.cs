using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Drawing;

public class LandingGearSlotHandler : MonoBehaviour, IDiceCheckable
{
    public List<int> requiredValues = new List<int>();
    public List<TextMeshProUGUI> BlueTextList = new List<TextMeshProUGUI>();
   

    void Awake()
    {
       
    }
    public void CheckDiceAmount(int diceAmount, GameObject dice)
    {
        if (requiredValues.Contains(diceAmount))
        {
            
            dice.GetComponent<CanvasGroup>().blocksRaycasts = false;
            GameManager.Instance.RangeColour(BlueTextList);

        }

    }
 
}
