using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoffeeTokenManager : MonoBehaviour, IDiceCheckable
{
    public void CheckDiceAmount(int diceAmount, GameObject dice)
    {
        if (diceAmount > 0 )
        {
            if (UIManager.Instance.coffee == 1)
            {
                DiceAmountIncrease(diceAmount, dice);
            }
            else if (UIManager.Instance.coffee == -1) 
            {
                DiceAmountDecrease(diceAmount, dice);
            }
        }

    }

    public void DiceAmountIncrease(int diceAmount, GameObject dice)
    {
        if(diceAmount < 6)
        {
            var diceImage = dice.GetComponentInChildren<Image>();

            if(!TurnManager.Instance.IsPilotTurn) diceImage.sprite = DiceManager.Instance.DiceFaceList[diceAmount].BlueDiceFace;
            else diceImage.sprite = DiceManager.Instance.DiceFaceList[diceAmount].OrangeDiceFace;
            dice.GetComponent<DiceInstance>().diceNo = diceAmount+1;
            UIManager.Instance.coffee = 0;
            UIManager.Instance.DonePlusOne.SetActive(true);
            
        }
    }

    public void DiceAmountDecrease(int diceAmount, GameObject dice)
    {
        if (diceAmount > 1)
        {
            var diceImage = dice.GetComponentInChildren<Image>();
            if (!TurnManager.Instance.IsPilotTurn) diceImage.sprite = DiceManager.Instance.DiceFaceList[diceAmount - 2].BlueDiceFace;
            else diceImage.sprite = DiceManager.Instance.DiceFaceList[diceAmount - 2].OrangeDiceFace;
            dice.GetComponent<DiceInstance>().diceNo = diceAmount-1;
            UIManager.Instance.coffee = 0;
            UIManager.Instance.DoneMinusOne.SetActive(true);
            
        }
    }
}
