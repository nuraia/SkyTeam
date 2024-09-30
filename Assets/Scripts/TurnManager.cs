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

    public GameObject CopilotEngine;
    public GameObject PilotEngine;
    public GameObject CopilotAxis;
    public GameObject PilotAxis;
    int turn;
    public Button turnButton;
    public bool IsPilotTurn ;
    public DiceManager diceManager;
    public GameRoundManager gameRoundManager;
    public TurnShiftManager turnShiftManager;
    public List<GameObject> AllOrangeSlot = new List<GameObject>();
    public List<GameObject> AllBlueSlot = new List<GameObject>();
    public int PilotTurn = 0;
    public int CoPilotTurn = 0;
    int PlaneImage = 0;
    int planeSlot = 0;
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
        UIManager.Instance.PopMessage("Roll Dice");
        PlaneImage = 1;
        planeSlot = 1;
    }

    public async UniTask ActivatePilot()
    {
        PilotTurn++;
        pilot.GetComponent<Image>().color = Color.green;

        if (PilotTurn > 3)
        {
            if (!GameManager.Instance.EngineFlag && GameManager.Instance.Enginecounter != 2)
            {
                UIManager.Instance.PopMessage("Fill Engines or Axis");
                //TurnOffAllWithoutAxisEngine(true);
            }
        }
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
        if (CoPilotTurn > 3)
        {
            if (!GameManager.Instance.EngineFlag && GameManager.Instance.Enginecounter != 2)
            {
                UIManager.Instance.PopMessage("Fill Engines or Axis");
                //TurnOffAllWithoutAxisEngine(false);
            }
        }
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
  
    public void OnClickTurnShift()
    {
        turnButton.gameObject.SetActive(false);
        
        CheckEnginSlots();
        CheckAxisSlot();
        GameManager.Instance.currentDraggableDice = null;
        GameManager.Instance.OnAllEnable.Invoke();

        if (PilotTurn + CoPilotTurn > 9  )
        {
            _ = GameTurn("New Turn");
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
                turnShiftManager.CalculateCopilotActivities();
                TurnOffSlot(AllOrangeSlot);
                TurnONSlot(AllBlueSlot);
                ActivatePilot();
            }
            else
            {
                pilot.GetComponent<Image>().color = Color.white;
                diceManager.PilotDiceSlot.SetActive(false);
                diceManager.CopilotDiceSlot.SetActive(true);
                turnShiftManager.CalculateFrictionActivities();
                turnShiftManager.CalculatePilotActivities();
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

    public void TurnONSlot(List<GameObject> slots)
    {
        foreach (var slot in slots)
        {
            var item = slot.GetComponent<Image>();
            item.raycastTarget = true;
        }
    }

    public void CheckEnginSlots()
    {
        if (CopilotEngine.transform.childCount == 1 && PilotEngine.transform.childCount == 1)
        {
            if (planeSlot == 1)
            {
                GameManager.Instance.Enginecounter = 2;
                turnShiftManager.PlaneSlotDecreaser();
                planeSlot = 0;
            }
        }

        if(CopilotEngine.transform.childCount == 1) CopilotEngine.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = false;
        if(PilotEngine.transform.childCount == 1) PilotEngine.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void CheckAxisSlot()
    {
        if (CopilotAxis.transform.childCount == 1 && PilotAxis.transform.childCount == 1)
        {
            if (PlaneImage == 1)
            {
                GameManager.Instance.Axiscounter = 2;
                GameManager.Instance.HandleRotation();
                PlaneImage = 0;
            }
        }
        if (CopilotAxis.transform.childCount == 1) CopilotAxis.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = false;
        if (PilotAxis.transform.childCount == 1) PilotAxis.transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = false;
    }


    //void TurnOffAllWithoutAxisEngine(bool IsBlue)
    //{
    //    if(IsBlue)
    //    {
    //        TurnOffSlot(turnShiftManager.GearSlots);
    //        TurnOffButton(turnShiftManager.FrictionSlots[0]);
    //        TurnOffSlot(turnShiftManager.BlueComs);
    //    }
    //    else
    //    {
    //        TurnOffSlot(turnShiftManager.OrangeComs);
    //        TurnOffSlot(turnShiftManager.FlapsSlots);
    //    }

    //    TurnOffSlot(turnShiftManager.CoffeeSlots);

    //}
}
