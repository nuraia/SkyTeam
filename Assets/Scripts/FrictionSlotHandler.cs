using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class DiceSlotHandler : MonoBehaviour, IDiceCheckable
{
    public List<int> requiredValues = new List<int>();
 
    public void CheckDiceAmount(int diceAmount)
    {
        if (requiredValues.Contains(diceAmount))
        {

            Debug.Log("Requirement fullfilled" + diceAmount);

            //GameManager.Instance.RangeColour(OrangeTextList);
            GameManager.Instance.NewSlotOpenFriction();

        }

    }
}
