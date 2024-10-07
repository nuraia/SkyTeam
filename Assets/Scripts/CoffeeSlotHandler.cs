using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CoffeeSlotHandler : MonoBehaviour, IDropHandler
{
    public Button coffeeTokenButton;
    public GameObject CoffeeTokenOptionPanel;
    private GameObject currentDice;
    
    void Start()
    {
        coffeeTokenButton.gameObject.SetActive(false);
        CoffeeTokenOptionPanel.SetActive(false);
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount != 0) return;
        TurnManager.Instance.turnButton.gameObject.SetActive(true);

        GameObject dropped = eventData.pointerDrag;
        DragDrop draggableItem = dropped.GetComponent<DragDrop>();
        if (GameManager.Instance.currentDraggableDice == null && (draggableItem.parentAfterDrag.gameObject.GetComponent<PanelDropDice>() != null))
        {
            GameManager.Instance.currentDraggableDice = dropped;
            GameManager.Instance.OnDiceDrag.Invoke();
        }
        DiceInstance dice = dropped.GetComponent<DiceInstance>();
        if (!TurnManager.Instance.IsBlueAxisEngineEmpty && !TurnManager.Instance.IsPilotTurn) draggableItem.parentAfterDrag = transform;
        else if (!TurnManager.Instance.IsOrangeAxisEngineEmpty && TurnManager.Instance.IsPilotTurn) draggableItem.parentAfterDrag = transform;
        

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
