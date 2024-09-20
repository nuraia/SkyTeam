using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceInstance : MonoBehaviour
{
    public int diceNo;
    public Sprite diceBlueSprite;
    public Sprite diceOrangeSprite;
    public bool IsBlueDice;
    public void LoadDiceData(DiceRoll DiceData)
    {
        diceNo = DiceData.diceNumber;
        diceBlueSprite = DiceData.BlueDiceFace;
        diceOrangeSprite = DiceData.OrangeDiceFace;
        if(diceBlueSprite != null )  IsBlueDice = true;
        else IsBlueDice = false;
    }
    public void CheckDice(bool isBlue)
    {
        IsBlueDice = isBlue;
       
    }
   


}
