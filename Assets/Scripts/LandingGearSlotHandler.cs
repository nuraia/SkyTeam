using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class LandingGearSlotHandler : MonoBehaviour, IDiceCheckable
{
    public List<int> requiredValues = new List<int>();
    public List<TextMeshProUGUI> BlueTextList = new List<TextMeshProUGUI>();
    public bool IsMatched;
    public GameObject Dice;
    public void CheckDiceAmount(int diceAmount, GameObject dice)
    {
        if (requiredValues.Contains(diceAmount))
        {
            gameObject.GetComponent<Image>().color = Color.green;
            dice.GetComponent<CanvasGroup>().blocksRaycasts = false;
            GameManager.Instance.RangeColour(BlueTextList);
            IsMatched = true;
            Dice = dice;
        }
        else
        {
            IsMatched = false;
        }
    }
 
}
