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
                        else Debug.Log("Game Over");

                    }
                    else if (GameRoundPanel.transform.childCount > 1)
                    {
                        Destroy(GameRoundPanel.transform.GetChild(0).gameObject);
                        UIManager.Instance.PopMessage("New Turn");
                        currentRound++;
                        //Debug.Log("current Round" + currentRound);
                        NewTurnActivate();
                        
                    }
                }
            }
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
                Destroy(diceComponent.gameObject);
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
