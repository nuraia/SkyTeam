using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FlapSlotHandler : MonoBehaviour, IDiceCheckable
{
    public List<int> requiredValues = new List<int>();
    public List<TextMeshProUGUI> OrangeTextList = new List<TextMeshProUGUI>();
    public bool IsMatched;
    public GameObject Dice;


    public void CheckDiceAmount(int diceAmount, GameObject dice)
    {
        if (requiredValues.Contains(diceAmount))
        {
            
            //Debug.Log("Requirement fullfilled" + diceAmount);
            gameObject.GetComponent<Image>().color = Color.green;
            dice.GetComponent<CanvasGroup>().blocksRaycasts = false;
            GameManager.Instance.RangeColour(OrangeTextList);
            GameManager.Instance.NewSlotOpenFlaps();
            Dice = dice;
        }
        else
        {
            IsMatched = false;
        }
        
    }
}
