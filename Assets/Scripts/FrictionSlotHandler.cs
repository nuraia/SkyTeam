using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DiceSlotHandler : MonoBehaviour, IDropHandler
{
    public List<int> requiredValues = new List<int>();
    public bool IsMatched;
    public GameObject Dice;
  

    void IDropHandler.OnDrop(PointerEventData eventData)
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
        if (requiredValues.Contains(dice.diceNo) && dice.IsBlueDice)
        {
            draggableItem.parentAfterDrag = transform;
            //gameObject.GetComponent<Image>().color = Color.green;
            
            IsMatched = true;
            Dice = dropped;

        }
        else
        {
            IsMatched = false;

        }
    }
}
