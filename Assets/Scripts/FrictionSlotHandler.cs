using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class DiceSlotHandler : MonoBehaviour, IDiceCheckable
{
    public List<int> requiredValues = new List<int>();
    public bool IsMatched;
    public GameObject Dice;
    public void CheckDiceAmount(int diceAmount, GameObject dice)
    {
        if (requiredValues.Contains(diceAmount))
        {
            dice.GetComponent<CanvasGroup>().blocksRaycasts = false;
            //Debug.Log("Requirement fullfilled" + diceAmount);
            gameObject.GetComponent<Image>().color = Color.green;
            GameManager.Instance.FrictionCount++;
            
            //GameManager.Instance.RangeColour(OrangeTextList);
            GameManager.Instance.NewSlotOpenFriction();
            Dice = dice;
        }
        else
        {
            IsMatched = false;
        }

    }
}
