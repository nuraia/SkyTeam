using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlapSlotHandler : MonoBehaviour, IDiceCheckable
{
    public List<int> requiredValues = new List<int>();
    public List<TextMeshProUGUI> OrangeTextList = new List<TextMeshProUGUI>();
   

 
    public void CheckDiceAmount(int diceAmount, GameObject dice)
    {
        if (requiredValues.Contains(diceAmount))
        {

            //Debug.Log("Requirement fullfilled" + diceAmount);

            dice.GetComponent<CanvasGroup>().blocksRaycasts = false;
            GameManager.Instance.RangeColour(OrangeTextList);
            GameManager.Instance.NewSlotOpenFlaps();
            
        }

    }
}
