using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisSlotHandler : MonoBehaviour, IDiceCheckable
{
    public void CheckDiceAmount(int diceAmount)
    {
        if (diceAmount > 0)
        {
            //Debug.Log("Requirement fullfilled" + diceAmount);
            GameManager.Instance.AxisPlaneStable(diceAmount);

        }

    }
}
