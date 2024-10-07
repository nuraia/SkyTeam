using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening.Core.Easing;

public class LandingGearSlotHandler : MonoBehaviour, IDropHandler
{
    public List<int> requiredValues = new List<int>();
   
    public bool IsMatched;
    public GameObject Dice;
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("Ondrop");

        if (transform.childCount != 0 ) return;
        TurnManager.Instance.turnButton.gameObject.SetActive(true);
        GameObject dropped = eventData.pointerDrag;
        DragDrop draggableItem = dropped.GetComponent<DragDrop>();
        
        if (GameManager.Instance.currentDraggableDice == null && (draggableItem.parentAfterDrag.gameObject.GetComponent<PanelDropDice>() != null) ) 
        {
            GameManager.Instance.currentDraggableDice = dropped;
            GameManager.Instance.OnDiceDrag.Invoke(); 
        }
        DiceInstance dice = dropped.GetComponent<DiceInstance>();
        if (requiredValues.Contains(dice.diceNo) && dice.IsBlueDice && !TurnManager.Instance.IsBlueAxisEngineEmpty)
        {
            draggableItem.parentAfterDrag = transform;
            
            //GameManager.Instance.Range();
            IsMatched = true;
            Dice = dropped;
        }
        else
        {
            IsMatched = false;
            
        }

    }
}
