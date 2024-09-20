using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class AxisSlotHandler : MonoBehaviour, IDiceCheckable
{
    public GameObject slot;
    public int slotValue;
   
    public Image PlaneImage;
    private int previousSubtraction = 0;
    private int AxisPilot;
    private int AxisCoPilot;

    void Start()
    {
        ResetValues();
    }
    public void CheckDiceAmount(int diceAmount, GameObject dice)
    {
     
        slotValue = diceAmount;
        GameManager.Instance.Axiscounter++;

        AxisSlotHandler otherAxis = slot.GetComponent<AxisSlotHandler>();
        if (dice.GetComponent<DiceInstance>().IsBlueDice)
        {
            Debug.Log(dice.GetComponent<DiceInstance>().IsBlueDice);
            if (otherAxis.slotValue > 0) {
                AxisPilot = diceAmount;
                AxisCoPilot = otherAxis.slotValue; 
            }
            else
            {
                ResetValues();
            }
           
        }
    
        else
        {
            if (otherAxis.slotValue > 0)
            {
                AxisPilot = otherAxis.slotValue;
                AxisCoPilot = diceAmount;
            }
            else
            {
                ResetValues();
            }
        }
        Debug.Log($"AxisCoPilot {AxisCoPilot} and AxisPilot {AxisPilot}");

        if (AxisPilot > 0 && AxisCoPilot > 0)
        {
            int currentSubtraction = AxisPilot - AxisCoPilot;
            ResetValues();
            HandleRotation(currentSubtraction);
        }
        else
        {
            ResetValues();
        }
    }

    private void HandleRotation(int currentSubtraction)
    {
        int totalSubtraction = previousSubtraction + currentSubtraction;
        Debug.Log($"Total Subtraction: {totalSubtraction}");

        if (Math.Abs(totalSubtraction) >= 4)
        {
            GameOver();
            return;
        }
      
        RectTransform rectTransform = PlaneImage.GetComponent<RectTransform>();
        rectTransform.Rotate(new Vector3(0, 0, currentSubtraction * 30));
        previousSubtraction = totalSubtraction;
        
}

  void ResetValues()
    {
        AxisPilot = 0;
        AxisCoPilot = 0;
    }
    private void GameOver()
    {
        GameManager.Instance.IsPlaneStable = false; 
        Debug.Log("Game Over! The plane has crashed!");
        
    }
}
