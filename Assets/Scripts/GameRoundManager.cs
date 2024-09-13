using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameRoundManager : MonoBehaviour
{
    private int GameRound = 6;
    private int currentRound;
    public GameObject GameRoundPanel;
    public GameObject OrangeSlotPanel;
    public GameObject BlueSlotPanel;

    void start()
    {
        currentRound = 0;
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
                            if (GameManager.Instance.IsPlaneStable && GameManager.Instance.EngineSum < 6)
                            {
                                Debug.Log("Game Wins");
                            }
                            else Debug.Log("Game Over");
                        }
                        
                    }
                    else if (GameRoundPanel.transform.childCount > 1)
                    {
                        Destroy(GameRoundPanel.transform.GetChild(0).gameObject);
                        UIManager.Instance.PopMessage("New Turn");
                        currentRound++;
                        Debug.Log("current Round" + currentRound);
                        NewTurnActivate();
                    }
                }
            }
        }
    }

    void NewTurnActivate()
    {
        TurnManager.Instance.diceManager.BlueDiceList.Clear(); 
        TurnManager.Instance.diceManager.OrangeDiceList.Clear();
        TurnManager.Instance.ActivatePilot();
    }
}
