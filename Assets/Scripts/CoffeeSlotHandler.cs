using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeSlotHandler : MonoBehaviour, IDiceCheckable
{
    public GameObject coffeeToken;
    public void CheckDiceAmount(int diceAmount, GameObject dice)
    {
        if (dice!= null)
        {
            coffeeToken.gameObject.SetActive(true);
            DiceManager.Instance.CoffeeSlotLists.Add(gameObject);
            DiceManager.Instance.CoffeeButtonLists.Add(coffeeToken);
            

        }
        
    }
}
