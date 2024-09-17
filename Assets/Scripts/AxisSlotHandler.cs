using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AxisSlotHandler : MonoBehaviour, IDiceCheckable
{

    public GameObject slot;
    public int slotValue;
    private int AxisPilot = 0;
    private int AxisCoPilot = 0;
    public Image PlaneImage;
    private int subtraction = 0;
    public void CheckDiceAmount(int diceAmount, GameObject dice)
    {
        slotValue = diceAmount;
        GameManager.Instance.Axiscounter++;
        AxisSlotHandler otherAxis = slot.GetComponent<AxisSlotHandler>();
        if (!TurnManager.Instance.IsPilotTurn && otherAxis.slotValue > 0)
        {
            AxisPilot = math.max(AxisPilot, diceAmount);
            AxisCoPilot = math.max(AxisCoPilot, otherAxis.slotValue);
        }
        else
        {
            AxisCoPilot = math.max(AxisCoPilot, diceAmount);
            AxisPilot = math.max(AxisPilot, otherAxis.slotValue);

        }
        //Debug.Log(AxisCoPilot);
        if (AxisPilot > 0 && AxisCoPilot > 0)
        {
            PlaneImageRotate(AxisPilot,AxisCoPilot);
            AxisCoPilot = 0; AxisPilot = 0;
        }
        
        //else Debug.Log($"AxisCoPilot {AxisCoPilot} and AxisPilot{AxisPilot}");

    }

    void PlaneImageRotate(int pilotAxis, int copilotAxis)
    {
        subtraction += pilotAxis - copilotAxis;
        Debug.Log("Sub" + (subtraction));
        if (Math.Abs(subtraction) < 4)
        {
            if (subtraction < 0)
            {
                RectTransform rectTransform = PlaneImage.GetComponent<RectTransform>();
                rectTransform.Rotate(new Vector3(0, 0, subtraction * 30));
            }
            else if (subtraction > 0)
            {
                RectTransform rectTransform = PlaneImage.GetComponent<RectTransform>();
                rectTransform.Rotate(new Vector3(0, 0, subtraction * 30));
            }

        }
        
        else
        {
            GameManager.Instance.IsPlaneStable = false;
        }
    }
}
