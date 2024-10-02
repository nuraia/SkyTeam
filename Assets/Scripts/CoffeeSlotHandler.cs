using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoffeeSlotHandler : MonoBehaviour, IDiceCheckable
{
    public Button coffeeTokenButton;
    public GameObject CoffeeTokenOptionPanel;
    private GameObject currentDice;
    
    void Start()
    {
        coffeeTokenButton.gameObject.SetActive(false);
        CoffeeTokenOptionPanel.SetActive(false);
    }
    public void CheckDiceAmount(int diceAmount, GameObject dice)
    {
       
        
    }

    public void CheckDice(GameObject dice)
    {
        
       coffeeTokenButton.gameObject.SetActive(true);
       currentDice = dice;

        
    }

    public void onButtonClick(int index)
    {
        CoffeeTokenOptionPanel.SetActive(true) ;
        UIManager.Instance.currentlyUsedCoffeeTokenIndex = index;
    }
    
    public void OnDestroy()
    {
        if (currentDice != null) { 
            Destroy(currentDice);
            currentDice = null;
            coffeeTokenButton.gameObject.SetActive(false);
        }
    }
}
