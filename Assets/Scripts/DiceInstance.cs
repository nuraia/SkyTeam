using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceInstance : MonoBehaviour
{
    public int diceNo;
    public Sprite diceBlueSprite;
    public Sprite diceOrangeSprite;
    
    public void LoadDiceData(DiceRoll DiceData)
    {
        diceNo = DiceData.diceNumber;
        diceBlueSprite = DiceData.BlueDiceFace;
        diceOrangeSprite = DiceData.OrangeDiceFace;
    }


}
