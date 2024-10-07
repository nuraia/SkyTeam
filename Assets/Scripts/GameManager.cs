using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

using DG.Tweening;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<GameObject>Flaps = new List<GameObject>();
    public List<GameObject>Friction = new List<GameObject>();
    
    
    public GameObject GameoverPanel;
    public Image PlaneImage;
    public int previousSubtraction = 0;
   
    public int AxisDifference;
   
    public int Enginecounter;
    public int Axiscounter;
    public bool IsPlaneStable = true;
    public bool EngineFlag = false;
    public int FrictionCount;
  
    
    public GameObject currentDraggableDice;
    public UnityEvent OnDiceDrag;
    public UnityEvent OnAllEnable;
    public UnityEvent OnAllDisable;
    void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(Instance);
      
        TurnManager.Instance.TurnOffSlot(Flaps);
        TurnManager.Instance.TurnOffSlot(Friction);
        
        Enginecounter = 0;
        FrictionCount = 0;
      
        currentDraggableDice = null;
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
        //UIManager.Instance.PopMessage("GameOver");
        TurnManager.Instance.turnButton.gameObject.SetActive(false);
    }

  



    public void HandleRotation()

    {
        Axiscounter = 2;
        int AxisPilot = TurnManager.Instance.PilotAxis.GetComponentInChildren<DiceInstance>().diceNo;
        int AxisCoPilot = TurnManager.Instance.CopilotAxis.GetComponentInChildren<DiceInstance>().diceNo;
        
        int currentSubtraction = AxisPilot - AxisCoPilot;
        
        int totalSubtraction = previousSubtraction + currentSubtraction;
        

        if (Math.Abs(totalSubtraction) >= 4)
        {
           GameOver();
            return;
        }

        RectTransform rectTransform = PlaneImage.GetComponent<RectTransform>();
        rectTransform.Rotate(new Vector3(0, 0, currentSubtraction * 20));
        previousSubtraction = totalSubtraction;
       
    }

}
