using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlapSlotHandler : MonoBehaviour, IDiceCheckable
{
    public List<int> requiredValues = new List<int>();
    public List<TextMeshProUGUI> OrangeTextList = new List<TextMeshProUGUI>();
    private int EndingRangeIndex;

 
    public void CheckDiceAmount(int diceAmount)
    {
        if (requiredValues.Contains(diceAmount))
        {

            //Debug.Log("Requirement fullfilled" + diceAmount);
           
            
            GameManager.Instance.RangeColour(OrangeTextList);
            GameManager.Instance.NewSlotOpenFlaps();
            
        }

    }
}
