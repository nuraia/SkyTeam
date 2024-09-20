using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class DiceManager : MonoBehaviour
{
    public static DiceManager Instance;
    public List<DiceRoll> DiceFaceList = new List<DiceRoll>();
    public List<GameObject> OrangeDiceList = new List<GameObject>();
    public List<GameObject> BlueDiceList = new List<GameObject>();


    public List<GameObject> CoffeeButtonLists = new List<GameObject>();
    public List<GameObject> CoffeeSlotLists = new List<GameObject>();
    

    public GameObject PilotDiceSlot;
    public GameObject CopilotDiceSlot;
    public GameObject DiceChangeSlot;
    public GameObject PlusOne;
    public GameObject MinusOne;

    private DiceRoll currentDiceRoll;
    int coffee = 0;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(Instance);
    }
    void Start()
    {
        OrangeDiceList.Clear();
        BlueDiceList.Clear();
    }


    public void DiceRolling(bool IsPilot)
    {
        if (IsPilot && PilotDiceSlot.transform.childCount < 4)
        {
            RandomRiceGeneratetion(PilotDiceSlot, true);
            PilotDiceSlot.SetActive(true);
        }
        else if(!IsPilot && CopilotDiceSlot.transform.childCount < 4)
        {
            RandomRiceGeneratetion(CopilotDiceSlot, false);
            CopilotDiceSlot.SetActive(true);
        }
    }

    void RandomRiceGeneratetion(GameObject panel, bool IsPilot)
    {
            if (IsPilot)
            {
                for (int i = 1; i <= 4; i++)
                {
                    int RandomIndex = Random.Range(1, 7);
                    currentDiceRoll = DiceFaceList[RandomIndex - 1];
                    Image InstantiatedDiceImage = currentDiceRoll.Prefab.GetComponentInChildren<Image>();
                    InstantiatedDiceImage.sprite = currentDiceRoll.BlueDiceFace;
                    var IntstantiatedDice = Instantiate(currentDiceRoll.Prefab, panel.transform);
                    IntstantiatedDice.GetComponent<DiceInstance>().LoadDiceData(currentDiceRoll);
                    IntstantiatedDice.GetComponent<DiceInstance>().CheckDice(true);
                    Button button = IntstantiatedDice.GetComponent<Button>();
                    button.onClick.AddListener(() => OnPrfabClick(IntstantiatedDice));
                    BlueDiceList.Add(currentDiceRoll.Prefab);
                    
                }
                
            }
            else
            {

                for (int i = 1; i <= 4; i++)
                {
                    int RandomIndex = Random.Range(1, 7);
                    currentDiceRoll = DiceFaceList[RandomIndex - 1];
                    Image InstantiatedDiceImage = currentDiceRoll.Prefab.GetComponentInChildren<Image>();
                    InstantiatedDiceImage.sprite = currentDiceRoll.OrangeDiceFace;
                    var IntstantiatedDice = Instantiate(currentDiceRoll.Prefab, panel.transform);
                    IntstantiatedDice.GetComponent<DiceInstance>().LoadDiceData(currentDiceRoll);
                    IntstantiatedDice.GetComponent<DiceInstance>().CheckDice(false);
                    Button button = IntstantiatedDice.GetComponent<Button>();
                    button.onClick.AddListener(() => OnPrfabClick(IntstantiatedDice));
                    OrangeDiceList.Add(currentDiceRoll.Prefab);
                }
                

                TurnManager.Instance.IsPilotTurn = true;
                TurnManager.Instance.GameTurn("Game Starts");
                currentDiceRoll.Prefab.GetComponentInChildren<Image>().sprite = null;
            }
        TurnManager.Instance.turnButton.gameObject.SetActive(true);
    }
    public void OnPrfabClick(GameObject DicePrefab)
    {
        Button Plusbutton = PlusOne.GetComponent<Button>();
        Plusbutton.onClick.RemoveAllListeners();
        Button Minusbutton = MinusOne.GetComponent<Button>();
        Minusbutton.onClick.RemoveAllListeners();
        if (UIManager.Instance.coffeePanel.activeSelf)
        {
            var prefabImage = DicePrefab.GetComponentInChildren<Image>();
            DiceChangeSlot.GetComponent<Image>().sprite = prefabImage.sprite;
        }
        Plusbutton.onClick.AddListener(() => DiceAmountIncrease(DicePrefab));
        Minusbutton.onClick.AddListener(() => DiceAmountDecrease(DicePrefab));
    }


    public void DiceAmountIncrease(GameObject dice)
    {
       
        var diceInstance = dice.GetComponent<DiceInstance>();
        Debug.Log(diceInstance.diceNo);
        if (diceInstance.diceNo < 6)
        {
            var diceImage = diceInstance.GetComponentInChildren<Image>();

            if (diceInstance.IsBlueDice) diceImage.sprite = DiceFaceList[diceInstance.diceNo].BlueDiceFace;
            else diceImage.sprite = DiceFaceList[diceInstance.diceNo].OrangeDiceFace;
            dice.GetComponent<DiceInstance>().diceNo = diceInstance.diceNo + 1;
            Button button = dice.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            ClearCoffeeToken();
        }
    }

    public void DiceAmountDecrease(GameObject dice)
    {
        var diceInstance = dice.GetComponent<DiceInstance>();
        if (diceInstance.diceNo > 1)
        {
            var diceImage = diceInstance.GetComponentInChildren<Image>();

            if (diceInstance.IsBlueDice) diceImage.sprite = DiceFaceList[diceInstance.diceNo - 2].BlueDiceFace;
            else diceImage.sprite = DiceFaceList[diceInstance.diceNo - 2].OrangeDiceFace;
            dice.GetComponent<DiceInstance>().diceNo = diceInstance.diceNo - 1;
            ClearCoffeeToken();
            Button button = dice.GetComponent<Button>();
            Destroy(button);
        }

    }
    void ClearCoffeeToken()
    {
       
        if(UIManager.Instance.currentlyUsedCoffeeToken != null) Destroy(UIManager.Instance.currentlyUsedCoffeeToken.gameObject);
        DiceChangeSlot.GetComponent<Image>().sprite = null;
        UIManager.Instance.coffeePanel.SetActive(false);
        if(CoffeeButtonLists.Count > 0){
            CoffeeButtonLists[CoffeeButtonLists.Count-1].gameObject.SetActive(false);
            CoffeeButtonLists.RemoveAt(CoffeeButtonLists.Count-1);
        }
    }
}


  

