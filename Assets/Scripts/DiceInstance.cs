using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceInstance : MonoBehaviour
{
    public int diceNo;
    
    public void LoadDiceData(DiceRoll DiceData)
    {
        diceNo = DiceData.diceNumber;
      
    }


}
