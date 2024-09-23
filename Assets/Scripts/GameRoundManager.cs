using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRoundManager : MonoBehaviour
{
    private int GameRound = 6;
    private int currentRound;
    public GameObject GameRoundPanel;
    public GameObject OrangeSlotPanel;
    public GameObject BlueSlotPanel;
    public List<GameObject> AxisEngineSlots = new List<GameObject>();
    public List<GameObject> FlapsFrictionGearSlots = new List<GameObject>();
    public GameObject GameoverPanel;
    public GameObject GamewinPanel;
    void start()
    {
        currentRound = 0;
        GameoverPanel.SetActive(false);
        GamewinPanel.SetActive(false);
            
    }
    public void GameRoundEndCheck()
    {
        if (currentRound < GameRound)
        {
            //Debug.Log(BlueSlotPanel.transform.childCount + OrangeSlotPanel.transform.childCount);
            if (BlueSlotPanel.transform.childCount == 0 && OrangeSlotPanel.transform.childCount == 0)
            {
                if (GameManager.Instance.Axiscounter == 2 && GameManager.Instance.Enginecounter == 2)
                {
                    
                    //Debug.Log("current Round" + currentRound);
                    if (GameRoundPanel.transform.childCount == 1)
                    {
                        if (GameManager.Instance.PlanePanel.transform.childCount == 1)
                        {
                            if (GameManager.Instance.IsPlaneStable && GameManager.Instance.EngineSum <= 6 && GameManager.Instance.FrictionCount >= 3)
                            {
                                GamewinPanel.SetActive(true);
                                TurnManager.Instance.turnButton.gameObject.SetActive(false);
                            }
                            else
                            {
                                GameManager.Instance.GameOver();
                            }
                        }
                        else
                        {
                            GameManager.Instance.GameOver();
                        }

                    }
                    else if (GameRoundPanel.transform.childCount > 1)
                    {
                        Destroy(GameRoundPanel.transform.GetChild(0).gameObject);
                        UIManager.Instance.PopMessage("New Turn");
                        currentRound++;
                        //Debug.Log("current Round" + currentRound);
                        NewTurnActivate();
                        
                    }
                    else
                    {
                        GameManager.Instance.GameOver();
                    }
                }
                else
                {
                    GameManager.Instance.GameOver();
                }
            }
            else
            {
                GameManager.Instance.GameOver();
            }
        }
        else
        {
            GameManager.Instance.GameOver();
        }
    }
  
public void NewTurnActivate()
    {
        TurnManager.Instance.diceManager.BlueDiceList.Clear(); 
        TurnManager.Instance.diceManager.OrangeDiceList.Clear();
        TurnManager.Instance.IsPilotTurn = true;
        TurnManager.Instance.TurnInitiate(TurnManager.Instance.IsPilotTurn);
        ClearSlots();
    }

    public void ClearSlots()
    {
        for (int i = 0; i < AxisEngineSlots.Count; i++)
        {
            if(AxisEngineSlots[i].transform.childCount > 0)
            {
                var diceComponent = AxisEngineSlots[i].transform.GetChild(0);
                var AxisSlot = diceComponent.GetComponent<AxisSlotHandler>();
                if (AxisSlot != null)
                {
                    AxisSlot.AxisPilot = 0;
                    AxisSlot.AxisCoPilot = 0;
                    AxisSlot.slotValue = 0;
                    AxisSlot.slot.GetComponent<AxisSlotHandler>().slotValue = 0;

                }
                Destroy(diceComponent.gameObject);
            }
        }
        foreach (var slot in FlapsFrictionGearSlots)
        {
            if (slot.transform.childCount > 0)
            {
                if (slot.GetComponent<LandingGearSlotHandler>() != null)
                {
                    var dice = slot.GetComponent<LandingGearSlotHandler>();
                    if(dice.IsMatched) dice.Dice.GetComponent<CanvasGroup>().blocksRaycasts = false;
                }
                if(slot.GetComponent<FlapSlotHandler>() != null)
                {
                    var dice = slot.GetComponent<FlapSlotHandler>();
                    if (dice.IsMatched) dice.Dice.GetComponent<CanvasGroup>().blocksRaycasts = false;
                }
                if (slot.GetComponent<DiceSlotHandler>() != null)
                {
                    var dice = slot.GetComponent<DiceSlotHandler>();
                    if (dice.IsMatched) dice.Dice.GetComponent<CanvasGroup>().blocksRaycasts = false;
                }
            }
        }
        GameManager.Instance.Axiscounter = 0;
        GameManager.Instance.Enginecounter = 0;
        GameManager.Instance.EngineSum = 0;
       
    }

    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene(0);
    }

}
