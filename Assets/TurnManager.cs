using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class TurnManager : MonoBehaviour
{
    public GameObject pilot;
    public GameObject copilot;
    int turn;
    public Button turnButton;
    public bool IsPilotTurn = true;
    public DiceManager diceManager;
    
    private void Start()
    {
        turnButton.onClick.AddListener(OnClickTurnShift);
        if (IsPilotTurn) ActivatePilot();
        turn = 4;
    }

    public async UniTask ActivatePilot()
    {
        
        pilot.GetComponent<Image>().color = Color.green;
        if (diceManager.BlueDiceList.Count <= 0)
        {
            Button btn = pilot.GetComponentInChildren<Button>();
            DiceGeneration(IsPilotTurn, btn);
            
        }
        else 
        {
            if (turn > 0) 
            {
                Debug.Log("P");
            }
        }
        IsPilotTurn = false;
       
    }

    public async UniTask ActivateCopilot()
    {
        copilot.GetComponent<Image>().color = Color.green;
       
       
        if (diceManager.OrangeDiceList.Count <= 0)
        {
            Button btn = copilot.GetComponentInChildren<Button>();
            DiceGeneration(IsPilotTurn, btn);
            
        }
        else 
        {
            if (turn > 0)
            {
                Debug.Log("cP");
            }
        }
        IsPilotTurn = true;
        
    }

    void DiceGeneration(bool IsPilot, Button btn) 
    {
        btn.onClick.AddListener(() => {
            diceManager.DiceRolling(IsPilot);
            ButtonUnInteract(btn);
        });
    }
    async UniTask ButtonUnInteract(Button btn)
    {
        await UniTask.Delay(1000);
        btn.interactable = false;
        //ActivateCopilot();
    }

    public void OnClickTurnShift()
    {
        if (IsPilotTurn)
        {
            copilot.GetComponent<Image>().color = Color.white;
            diceManager.CopilotDiceSlot.SetActive(false);
            diceManager.PilotDiceSlot.SetActive(true);
            ActivatePilot();
        }
        else
        {
            pilot.GetComponent<Image>().color = Color.white;
            diceManager.PilotDiceSlot.SetActive(false);
            diceManager.CopilotDiceSlot.SetActive(true);
            ActivateCopilot();

        }
    }
}
