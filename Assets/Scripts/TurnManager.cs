using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;
    public GameObject pilot;
    public GameObject copilot;
    int turn;
    public Button turnButton;
    public bool IsPilotTurn = true;
    public DiceManager diceManager;
    public GameRoundManager gameRoundManager;
    public List<GameObject> AllOrangeSlot = new List<GameObject>();
    public List<GameObject> AllBlueSlot = new List<GameObject>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(Instance);
    }
    private void Start()
    {
        turnButton.onClick.AddListener(OnClickTurnShift);
        if (IsPilotTurn) ActivatePilot();
        turn = 0;
    }

    public async UniTask ActivatePilot()
    {
        
        pilot.GetComponent<Image>().color = Color.green;
        if (diceManager.BlueDiceList.Count <= 0)
        {
            TurnOffSlot(AllOrangeSlot);
            TurnOffSlot(AllBlueSlot);
            Button btn = pilot.GetComponentInChildren<Button>();
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
        copilot.GetComponent<Image>().color = Color.green;
       
       
        if (diceManager.OrangeDiceList.Count <= 0)
        {
            TurnOffSlot(AllOrangeSlot);
            TurnOffSlot(AllBlueSlot);
            Button btn = copilot.GetComponentInChildren<Button>();
            DiceGeneration(IsPilotTurn, btn);
            
        }
        else 
        {
            
            TurnONSlot(AllOrangeSlot);
        }
        IsPilotTurn = true;
        
    }

    public async UniTask GameTurn()
    {
        UIManager.Instance.PopMessage("Game Starts");
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

    public void OnClickTurnShift()
    {
        turn++;
        if (turn > 9)
        {
            
            gameRoundManager.GameRoundEndCheck();
        }
        else
        {
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
