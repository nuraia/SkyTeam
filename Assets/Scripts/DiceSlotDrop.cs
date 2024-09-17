using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiceSlotDrop : MonoBehaviour, IDropHandler
{
    
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("Ondrop");
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DragDrop draggableItem = dropped.GetComponent<DragDrop>();
            draggableItem.parentAfterDrag = transform;
            DiceInstance dice = dropped.GetComponent<DiceInstance>();
            IDiceCheckable checkable = gameObject.GetComponent<IDiceCheckable>();
            if(checkable != null) checkable.CheckDiceAmount(dice.diceNo, dropped);
            
        }
    }
}
