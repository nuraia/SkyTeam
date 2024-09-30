using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameRoundManager : MonoBehaviour
{
    private int GameRound = 6;
    private int currentRound;
    public GameObject GameRoundPanel;
    public GameObject OrangeSlotPanel;
    public GameObject BlueSlotPanel;
    public List<GameObject> AxisEngineSlots = new List<GameObject>();
    
    public GameObject GameoverPanel;
    public GameObject GamewinPanel;
    public TurnShiftManager TurnShiftManager;
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
            if (GameRoundPanel.transform.childCount > 1)
            {


                Destroy(GameRoundPanel.transform.GetChild(0).gameObject);
                UIManager.Instance.PopMessage("New Turn");
               
                GameManager.Instance.EngineFlag = false;
                NewTurnActivate();
            }
        }
        


        else 
        {
            Debug.Log("CR" + currentRound);
            if (GameRoundPanel.transform.childCount == 1)
            {

                if (TurnShiftManager.PlanePanel.transform.childCount == 1)
                {
                    Debug.Log("1");
                    if (GameManager.Instance.IsPlaneStable && TurnShiftManager.EngineSum <= 6 && GameManager.Instance.FrictionCount >= 3)
                    {
                        
                        GamewinPanel.SetActive(true);
                        TurnManager.Instance.turnButton.gameObject.SetActive(false);
                    }
                    else
                    {
                        GameManager.Instance.GameOver();
                    }
                }
                

            }
            
        }
        currentRound++;
        Debug.Log("CR" + currentRound);
        //}
        //else
        //{
        //    TurnManager.Instance.PilotTurn--;
        //    TurnManager.Instance.CoPilotTurn--;
        //    TurnManager.Instance.turnButton.gameObject.SetActive(true);
        //}


        //else if (currentRound == GameRound)
        //{
        //    if (BlueSlotPanel.transform.childCount == 0 && OrangeSlotPanel.transform.childCount == 0)
        //    {

        //        //Debug.Log("current Round" + currentRound);
        //        if (GameRoundPanel.transform.childCount == 1)
        //        {

        //            if (TurnShiftManager.PlanePanel.transform.childCount == 1)
        //            {
        //                if (GameManager.Instance.IsPlaneStable && TurnShiftManager.EngineSum <= 6 && GameManager.Instance.FrictionCount >= 3)
        //                {
        //                    GamewinPanel.SetActive(true);
        //                    TurnManager.Instance.turnButton.gameObject.SetActive(false);
        //                }
        //                else
        //                {
        //                    GameManager.Instance.GameOver();
        //                }
        //            }
        //            else
        //            {
        //                GameManager.Instance.GameOver();
        //            }

        //        }
        //    }
        //}

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
        GameManager.Instance.EngineFlag = false;
        
        
   
       
    }

    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene(0);
    }

}
