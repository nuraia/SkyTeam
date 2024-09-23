using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;
    public GameObject pilot;
    public GameObject copilot;
    int turn;
    public Button turnButton;
    public bool IsPilotTurn ;
    public DiceManager diceManager;
    public GameRoundManager gameRoundManager;
    public List<GameObject> AllOrangeSlot = new List<GameObject>();
    public List<GameObject> AllBlueSlot = new List<GameObject>();
    int PilotTurn = 0;
    int CoPilotTurn = 0;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(Instance);
    }
    private void Start()
    {
        turnButton.onClick.AddListener(OnClickTurnShift);
        IsPilotTurn = true;
        TurnInitiate(IsPilotTurn);
        OnClickTurnShift();
    }

    public void TurnInitiate(bool IsPilotTurn)
    {
        turn = 0;
        turnButton.gameObject.SetActive(false);
        PilotTurn = 0;
        CoPilotTurn = 0;

    }

    public async UniTask ActivatePilot()
    {
        PilotTurn++;
        pilot.GetComponent<Image>().color = Color.green;
        if (diceManager.BlueDiceList.Count <= 0)
        {
            TurnOffSlot(AllOrangeSlot);
            TurnOffSlot(AllBlueSlot);
            Button btn = pilot.GetComponentInChildren<Button>();
            btn.interactable = true;
            DiceGeneration(IsPilotTurn, btn);
            
        }
        else 
        {
            
            TurnONSlot(AllBlueSlot);
            
        }
        IsPilotTurn = false;
        
    }

    public async UniTask ActivateCopilot()
    {
        CoPilotTurn++;
        copilot.GetComponent<Image>().color = Color.green;
       
        if (diceManager.OrangeDiceList.Count <= 0)
        {
            TurnOffSlot(AllOrangeSlot);
            TurnOffSlot(AllBlueSlot);
            Button btn = copilot.GetComponentInChildren<Button>();
            btn.interactable = true;
            DiceGeneration(IsPilotTurn, btn);
            
        }
        else 
        {
            
            TurnONSlot(AllOrangeSlot);
        }
        IsPilotTurn = true;
        
    }

    public async UniTask GameTurn(string message)
    {
        UIManager.Instance.PopMessage(message);
        await UniTask.Delay(2000);
       
        OnClickTurnShift();

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
    }
    //async UniTask turnShift()
    //{
    //    await UniTask.Delay(3000);
    //    turnButton.gameObject.SetActive(true);
    //}

    public void OnClickTurnShift()
    {
        turnButton.gameObject.SetActive(false);
        //turnShift();
        if (PilotTurn + CoPilotTurn > 9)
        {
            TurnManager.Instance.GameTurn("New Turn");
            gameRoundManager.GameRoundEndCheck();
        }
        else
        {
            turn++;
            if (IsPilotTurn)
            {
                copilot.GetComponent<Image>().color = Color.white;
                diceManager.CopilotDiceSlot.SetActive(false);
                diceManager.PilotDiceSlot.SetActive(true);
                TurnOffSlot(AllOrangeSlot);
                TurnONSlot(AllBlueSlot);
                ActivatePilot();
            }
            else
            {
                pilot.GetComponent<Image>().color = Color.white;
                diceManager.PilotDiceSlot.SetActive(false);
                diceManager.CopilotDiceSlot.SetActive(true);
                TurnOffSlot(AllBlueSlot);
                TurnONSlot(AllOrangeSlot);
                ActivateCopilot();

            }
        }
    }

    public void TurnOffSlot(List< GameObject> slots)
    {
        foreach (var slot in slots)
        {
            var item = slot.GetComponent<Image>();
            item.raycastTarget = false;
        }
    }

    void TurnONSlot(List<GameObject> slots)
    {
        foreach (var slot in slots)
        {
            var item = slot.GetComponent<Image>();
            item.raycastTarget = true;
        }
    }

 
}
