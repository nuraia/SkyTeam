using UnityEngine;
using UnityEngine.EventSystems;

public class DiceSlotDrop : MonoBehaviour, IDropHandler
{

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
        draggableItem.parentAfterDrag = transform;
        
        DiceInstance dice = dropped.GetComponent<DiceInstance>();
        IDiceCheckable checkable = gameObject.GetComponent<IDiceCheckable>();
        if (checkable != null) checkable.CheckDiceAmount(dice.diceNo, dropped); 


    }
}
