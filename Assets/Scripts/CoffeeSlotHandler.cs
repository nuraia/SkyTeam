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
            UIManager.Instance.CoffeeSlotLists.Add(gameObject);
            UIManager.Instance.CoffeebuttonLists.Add(coffeeToken);
        }
        
    }
}
