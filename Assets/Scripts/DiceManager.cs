using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour
{
    public List<DiceRoll> DiceFaceList = new List<DiceRoll>();
    public List<DiceRoll> OrangeDiceList = new List<DiceRoll>();
    public List<DiceRoll> BlueDiceList = new List<DiceRoll>();
    private Sprite DiceFaceImage;
    private int DiceFaceIndex;
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
        if (IsPilot)
        {
            RandomRiceGeneratetion(PilotDiceSlot, true);
            PilotDiceSlot.SetActive(true);
        }
        else
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
                BlueDiceList.Add(currentDiceRoll);
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
                OrangeDiceList.Add(currentDiceRoll);
            }
            
            currentDiceRoll.Prefab.GetComponentInChildren<Image>().sprite = null;
            
            
        }

        
    }

}
