using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LandingGearSlotHandler : MonoBehaviour, IDiceCheckable
{
    public List<int> requiredValues = new List<int>();
    public List<TextMeshProUGUI> BlueTextList = new List<TextMeshProUGUI>();
    private int startingRangeIndex;

    void Awake()
    {
        startingRangeIndex = 0;
    }
    public void CheckDiceAmount(int diceAmount)
    {
        if (requiredValues.Contains(diceAmount))
        {

            


            GameManager.Instance.RangeColour(BlueTextList);

        }

    }
 
}
