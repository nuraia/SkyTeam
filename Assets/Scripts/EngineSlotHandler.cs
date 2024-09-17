using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSlotHandler : MonoBehaviour, IDiceCheckable
{
    public void CheckDiceAmount(int diceAmount, GameObject dice)
    {
        if (diceAmount > 0)
        {
            //Debug.Log("Requirement fullfilled" + diceAmount);
            GameManager.Instance.EngineSlotChecker(diceAmount);

        }

    }
}
