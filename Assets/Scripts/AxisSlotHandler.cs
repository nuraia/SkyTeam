using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class AxisSlotHandler : MonoBehaviour, IDiceCheckable
{
    //public GameObject slot;
    //public int slotValue;
   
    //public Image PlaneImage;
    //private int previousSubtraction;
    //public int AxisPilot;
    //public int AxisCoPilot;

    void Start()
    {
       
        //ResetValues();
        //Initialize();
    }

    private void Initialize()
    {
       
       // previousSubtraction = GameManager.Instance.previousSubtraction;

    }
    public void CheckDiceAmount(int diceAmount, GameObject dice)
    {
        
        //slotValue = diceAmount;
        //GameManager.Instance.Axiscounter++;

        //AxisSlotHandler otherAxis = slot.GetComponent<AxisSlotHandler>();
        //if (dice.GetComponent<DiceInstance>().IsBlueDice)
        //{
        //    //Debug.Log(dice.GetComponent<DiceInstance>().IsBlueDice);
        //    if (otherAxis.slotValue > 0) {
        //        AxisPilot = diceAmount;
        //        AxisCoPilot = otherAxis.slotValue; 
        //    }
           
           
        //}
    
        //else
        //{
        //    if (otherAxis.slotValue > 0)
        //    {
        //        AxisPilot = otherAxis.slotValue;
        //        AxisCoPilot = diceAmount;
        //    }
            
        //}
        //Debug.Log($"AxisCoPilot {AxisCoPilot} and AxisPilot {AxisPilot}");

       
    }

    //void ResetValues()
    //{
    //    AxisPilot = 0;
    //    AxisCoPilot = 0;
    //    slotValue = 0;
    //}
}
