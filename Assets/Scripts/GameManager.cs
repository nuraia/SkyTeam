using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<GameObject>Flaps = new List<GameObject>();
    public List<GameObject>Friction = new List<GameObject>();
    public GameObject PlanePanel;

    public Image PlaneImage;
    
    private int EndingRangeIndex;
    private int StaringRangeIndex;
    private int AxisDifference;
    private int EngineSum;
    private int Enginecounter;
    bool Pilot = false;
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
    }

    public void RangeColour(List<TextMeshProUGUI> RangeList)
    {
        if(!TurnManager.Instance.IsPilotTurn)
        {
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

    public void AxisPlaneStable(int axisAmount)
    {
        if (AxisDifference == 0)
        {
            AxisDifference = axisAmount;
            if (!TurnManager.Instance.IsPilotTurn) Pilot = true;
            Debug.Log($"{Pilot} first");
        }
        else
        {
            AxisDifference -= axisAmount;
            PlaneImageRotate(AxisDifference);
        }
        
        
    }

    void PlaneImageRotate(int AxisDifference)
    {
        if (Pilot && AxisDifference <= 3)
        {
            RectTransform rectTransform = PlaneImage.GetComponent<RectTransform>();
            rectTransform.Rotate(new Vector3(0, 0, AxisDifference * 30));
        }
        else if (!Pilot && AxisDifference <= 3)
        {
            RectTransform rectTransform = PlaneImage.GetComponent<RectTransform>();
            rectTransform.Rotate(new Vector3(0, 0, -AxisDifference * 30));
        }
        else
        {
            Debug.Log($"GameOver");
        }
    }
    public void EngineSlotChecker(int slotAmount)
    {
        Enginecounter++;
        EngineSum += slotAmount;
        int bluedot = 5 + StaringRangeIndex;
        int orangedot = 8 + EndingRangeIndex;
        

        Debug.Log($"Engine Sum {EngineSum} starting Index {5 + StaringRangeIndex} and ending Index {8 + EndingRangeIndex}");
        if (Enginecounter >= 2)
        {
            if (EngineSum >= bluedot && EngineSum <= orangedot)
            {
                if (PlanePanel.transform.childCount > 0)
                {
                    Destroy(PlanePanel.transform.GetChild(0).gameObject);
                    Debug.Log("yay");
                }

            }
            else if (EngineSum > orangedot)
            {

                if (PlanePanel.transform.childCount > 1)
                {
                    Destroy(PlanePanel.transform.GetChild(0).gameObject);
                    Destroy(PlanePanel.transform.GetChild(1).gameObject);
                    Debug.Log("yay2");

                }

            }

        }

    }

}
