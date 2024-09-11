using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<TextMeshProUGUI> OrangeTextList = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> BlueTextList = new List<TextMeshProUGUI>();
    private int EndingRangeIndex;
    private int startingRangeIndex;
    
    void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(Instance);
        EndingRangeIndex = 0;
        startingRangeIndex = 0;
    }

    public void RangeColour()
    {
        if (TurnManager.Instance.IsPilotTurn == false)
        {
            TextMeshProUGUI rangeText = BlueTextList[startingRangeIndex].GetComponent<TextMeshProUGUI>();
            rangeText.color = Color.cyan;
            startingRangeIndex++;
        }
        else
        {
            TextMeshProUGUI rangeText = OrangeTextList[EndingRangeIndex].GetComponent<TextMeshProUGUI>();
            rangeText.color = Color.red;
            EndingRangeIndex++;
        }
    }
    
}
