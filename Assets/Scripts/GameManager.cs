using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<GameObject>Flaps = new List<GameObject>();
    public List<GameObject>Friction = new List<GameObject>();
    public GameObject PlanePanel;
    public GameObject GameoverPanel;
    public Image PlaneImage;
    public int previousSubtraction = 0;
    public int EndingRangeIndex;
    public int StaringRangeIndex;
    public int AxisDifference;
    public int EngineSum;
    public int Enginecounter;
    public int Axiscounter;
    public bool IsPlaneStable = true;
    public int FrictionCount;
    void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(Instance);
        EndingRangeIndex = 0;
        StaringRangeIndex = 0;
        TurnManager.Instance.TurnOffSlot(Flaps);
        TurnManager.Instance.TurnOffSlot(Friction);
        AxisDifference = 0;
        EngineSum = 0;
        Enginecounter = 0;
        FrictionCount = 0;
    }

    public void RangeColour(List<TextMeshProUGUI> RangeList)
    {
        if(!TurnManager.Instance.IsPilotTurn)
        {
            //if(StaringRangeIndex > 1) RangeList[StaringRangeIndex-1].GetComponent<TextMeshProUGUI>().color = Color.gray;
            TextMeshProUGUI rangeText = RangeList[StaringRangeIndex].GetComponent<TextMeshProUGUI>();
            rangeText.color = Color.cyan;
            StaringRangeIndex++;
        }
        else
        {
            TextMeshProUGUI rangeText = RangeList[EndingRangeIndex].GetComponent<TextMeshProUGUI>();
            rangeText.color = Color.red;
            EndingRangeIndex++;

        }

    }

    public void NewSlotOpenFlaps()
    {
        if (Flaps.Count > 0)
        {
            var item = Flaps[0].GetComponent<Image>();
            item.raycastTarget = true;

            Flaps.RemoveAt(0);
        }
       
    }

    public void NewSlotOpenFriction()
    {
        if (Friction.Count > 0)
        {
            var item = Friction[0].GetComponent<Image>();
            item.raycastTarget = true;

            Friction.RemoveAt(0);
        }

    }

    public void GameOver()
    {
        IsPlaneStable = false;
        GameoverPanel.SetActive(true);
        TurnManager.Instance.GameTurn("GameOver");
        TurnManager.Instance.turnButton.gameObject.SetActive(false);
    }

    public void EngineSlotChecker(int slotAmount)
    {
        Enginecounter++;
        EngineSum += slotAmount;
        int bluedot = 4 + StaringRangeIndex;
        int orangedot = 8 + EndingRangeIndex;
        

       // Debug.Log($"Engine Sum {EngineSum} starting Index {5 + StaringRangeIndex} and ending Index {8 + EndingRangeIndex}");
        if (Enginecounter >= 2)
        {
            if (EngineSum >= bluedot && EngineSum <= orangedot)
            {
                if (PlanePanel.transform.childCount > 0)
                {
                    Destroy(PlanePanel.transform.GetChild(0).gameObject);
            
                }

            }
            else if (EngineSum > orangedot)
            {

                if (PlanePanel.transform.childCount > 1)
                {
                    Destroy(PlanePanel.transform.GetChild(0).gameObject);
                    Destroy(PlanePanel.transform.GetChild(1).gameObject);
                    

                }

            }

        }

    }

}
