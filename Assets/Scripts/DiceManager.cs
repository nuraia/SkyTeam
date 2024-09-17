using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour
{
    public List<DiceRoll> DiceFaceList = new List<DiceRoll>();
    public List<GameObject> OrangeDiceList = new List<GameObject>();
    public List<GameObject> BlueDiceList = new List<GameObject>();
    
    public GameObject PilotDiceSlot;
    public GameObject CopilotDiceSlot;
    
    private DiceRoll currentDiceRoll;
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
                OrangeDiceList.Add(currentDiceRoll.Prefab);
            }

            TurnManager.Instance.IsPilotTurn = true;
            TurnManager.Instance.GameTurn("Game Starts");
            currentDiceRoll.Prefab.GetComponentInChildren<Image>().sprite = null;


        }


    }


    //void RandomRiceGeneratetion(GameObject panel, bool IsPilot)
    //{

    //    if (IsPilot)
    //    {
    //        for (int i = 0; i <6; i++)
    //        {
    //            //int RandomIndex = Random.Range(1, 7);
    //            currentDiceRoll = DiceFaceList[i];
    //            Image InstantiatedDiceImage = currentDiceRoll.Prefab.GetComponentInChildren<Image>();
    //            InstantiatedDiceImage.sprite = currentDiceRoll.BlueDiceFace;
    //            var IntstantiatedDice = Instantiate(currentDiceRoll.Prefab, panel.transform);
    //            IntstantiatedDice.GetComponent<DiceInstance>().LoadDiceData(currentDiceRoll);
    //            BlueDiceList.Add(currentDiceRoll.Prefab);

    //        }



    //    }

    //    else
    //    {
    //        for (int i = 0; i < 6; i++)
    //        {
    //            //int RandomIndex = Random.Range(1, 7);
    //            currentDiceRoll = DiceFaceList[i];
    //            Image InstantiatedDiceImage = currentDiceRoll.Prefab.GetComponentInChildren<Image>();
    //            InstantiatedDiceImage.sprite = currentDiceRoll.OrangeDiceFace;
    //            var IntstantiatedDice = Instantiate(currentDiceRoll.Prefab, panel.transform);
    //            IntstantiatedDice.GetComponent<DiceInstance>().LoadDiceData(currentDiceRoll);
    //            OrangeDiceList.Add(currentDiceRoll.Prefab);
    //        }

    //        TurnManager.Instance.IsPilotTurn = true;
    //        TurnManager.Instance.GameTurn("Game Starts");
    //        currentDiceRoll.Prefab.GetComponentInChildren<Image>().sprite = null;


    //    }


    //}
}
