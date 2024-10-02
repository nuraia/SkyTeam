using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnShiftManager : MonoBehaviour
{
    public List<GameObject> FlapsSlots = new List<GameObject>();
    public List<GameObject> GearSlots = new List<GameObject>();
    public List<GameObject> FrictionSlots = new List<GameObject>();
    public List<GameObject> CoffeeSlots = new List<GameObject>();
    public List<GameObject> BlueComs = new List<GameObject>();
    public List<GameObject> OrangeComs = new List<GameObject>();
    

    public List<TextMeshProUGUI> BlueTextList = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> OrangeTextList = new List<TextMeshProUGUI>();
    
    public GameObject PlanePanel;
    public bool IsPlaneAvailable = false;
    public GameObject Plane = null;
    public int EndingRangeIndex;
    public int StaringRangeIndex;
    public int EngineSum;
    int bluedot;
    int orangedot;
    private TextMeshProUGUI prevRangeBlueText;
    private TextMeshProUGUI prevRangeOrgText;
    private void Start()
    {
        EndingRangeIndex = 0;
        StaringRangeIndex = 0;
        EngineSum = 0;
  
    }
    public void CalculateCopilotActivities()
    {

        for (int i = 0; i < FlapsSlots.Count; i++)
        {

            if (FlapsSlots[i].transform.childCount > 0)
            {

                if (FlapsSlots[i].GetComponent<FlapSlotHandler>() != null)
                {
                    var dice = FlapsSlots[i].GetComponent<FlapSlotHandler>();
                    if (dice.IsMatched)
                    {
                        FlapsSlots[i].GetComponent<Image>().color = Color.green;
                        FlapsSlots[i].GetComponent<FlapSlotHandler>().enabled = false;
                        FlapsSlots[i].GetComponent<Image>().raycastTarget = false;
                        Destroy(FlapsSlots[i].transform.GetChild(0).gameObject);
                        EndingRangeIndex++;
                        Debug.Log("End" + EndingRangeIndex);
                        FlapsSlots.Remove(FlapsSlots[i]);
                    }
                }
            }
        }
        RangeColor();
        if (IsPlaneAvailable) PlaneRemoveFromPlaneSlot(Plane);
    }

    public void CalculatePilotActivities()
    {
       
        for (int i = 0; i < GearSlots.Count; i++)
        {

            if (GearSlots[i].transform.childCount > 0)
            {

                if (GearSlots[i].GetComponent<LandingGearSlotHandler>() != null)
                {
                    var dice = GearSlots[i].GetComponent<LandingGearSlotHandler>();
                    if (dice.IsMatched)
                    {
                        GearSlots[i].GetComponent<Image>().color = Color.green;
                        GearSlots[i].GetComponent<LandingGearSlotHandler>().enabled = false;
                        Destroy(GearSlots[i].transform.GetChild(0).gameObject);
                        StaringRangeIndex++;
                        Debug.Log("START" + StaringRangeIndex);
                        GearSlots.Remove(GearSlots[i]);
                    }
                }
            }
        }
        RangeColor();
        if(IsPlaneAvailable) PlaneRemoveFromPlaneSlot(Plane);
        
    }

    public void CalculateFrictionActivities()
    {
        for (int i = 0; i < FrictionSlots.Count; i++)
        {

            if (FrictionSlots[i].transform.childCount > 0)
            {

                if (FrictionSlots[i].GetComponent<DiceSlotHandler>() != null)
                {
                    var dice = FrictionSlots[i].GetComponent<DiceSlotHandler>();
                    if (dice.IsMatched)
                    {
                        FrictionSlots[i].GetComponent<Image>().color = Color.green;
                        GearSlots[i].GetComponent<DiceSlotHandler>().enabled = false;
                        FrictionSlots[i].GetComponent<Image>().raycastTarget = false;
                        Destroy(FrictionSlots[i].transform.GetChild(0).gameObject);
                        
                        GameManager.Instance.FrictionCount++;
                        GameManager.Instance.NewSlotOpenFriction();
                        //Debug.Log("FRICTION" + GameManager.Instance.FrictionCount);
                        FrictionSlots.Remove(FrictionSlots[i]);
                    }
                }
            }
        }

    }

    public void RangeColor()
    {
        if (BlueTextList.Count >= StaringRangeIndex && StaringRangeIndex > 0)
        {
           
            if (prevRangeBlueText != null)
            {
                prevRangeBlueText.color = Color.grey; 
            }

            TextMeshProUGUI rangeBlueText = BlueTextList[StaringRangeIndex - 1].GetComponent<TextMeshProUGUI>();
            rangeBlueText.color = Color.cyan;
            prevRangeBlueText = rangeBlueText;
        }

        if (OrangeTextList.Count >= EndingRangeIndex && EndingRangeIndex > 0)
        {
            
            if (prevRangeOrgText != null)
            {
                prevRangeOrgText.color = Color.grey;
            }


            TextMeshProUGUI rangeOrgText = OrangeTextList[EndingRangeIndex - 1].GetComponent<TextMeshProUGUI>();
            rangeOrgText.color = Color.red;
            prevRangeOrgText = rangeOrgText;
        }

      
    }

    public void PlaneSlotDecreaser()
    {
        int PilotEngineAmount = TurnManager.Instance.PilotEngine.GetComponentInChildren<DiceInstance>().diceNo;
        int CoPilotEngineAmount = TurnManager.Instance.CopilotEngine.GetComponentInChildren<DiceInstance>().diceNo;
        EngineSum = PilotEngineAmount + CoPilotEngineAmount;


       
        bluedot = StaringRangeIndex + 5;
        orangedot = EndingRangeIndex + 8 ;
        //Debug.Log($"Engine Sum {EngineSum} b {bluedot} or {orangedot}  ");
        if (EngineSum >= bluedot && EngineSum <= orangedot)
        {
            if (PlanePanel.transform.childCount > 0)
            {
                if (PlanePanel.transform.GetChild(PlanePanel.transform.childCount-1).gameObject.transform.childCount == 0)
                {
                    Destroy(PlanePanel.transform.GetChild(PlanePanel.transform.childCount - 1).gameObject);
                }
                else
                {
                    GameManager.Instance.GameOver();
                }
            }

        }
        else if (EngineSum > orangedot)
        {

            if (PlanePanel.transform.childCount > 1)
            {
                if (PlanePanel.transform.GetChild(PlanePanel.transform.childCount - 1).gameObject.transform.childCount == 0 && PlanePanel.transform.GetChild(PlanePanel.transform.childCount - 2).gameObject.transform.childCount == 0)
                {
                    Destroy(PlanePanel.transform.GetChild(PlanePanel.transform.childCount - 1).gameObject);
                    Destroy(PlanePanel.transform.GetChild(PlanePanel.transform.childCount - 2).gameObject);
                }
                else
                {
                    GameManager.Instance.GameOver();
                }
          


            }

        }

       
        
        GameManager.Instance.EngineFlag = true;
    }

    public void PlaneRemoveFromPlaneSlot(GameObject Plane)
    {
        Destroy (Plane);
        IsPlaneAvailable = false;
        Plane = null;
    }

    public void CheckCoffeeSlots()
    {
        for(int i = 0; i < CoffeeSlots.Count; i++)
        {
            if (CoffeeSlots[i].transform.childCount > 0)
            {
                CoffeeSlots[i].gameObject.GetComponent<CoffeeSlotHandler>().CheckDice(CoffeeSlots[i].transform.GetChild(0).gameObject);
                CoffeeSlots[i].transform.GetChild(0).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            }
        }
    }
}