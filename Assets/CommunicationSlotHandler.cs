using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CommunicationSlotHandler : MonoBehaviour, IDropHandler
{
    public List<GameObject> PlaneList = new ();
    public TurnShiftManager turnShiftManager;
 
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
        if (PlaneList[turnShiftManager.PlanePanel.transform.childCount - dice.diceNo].transform.childCount > 0)
        {
            turnShiftManager.IsPlaneAvailable = true;
            turnShiftManager.Plane = PlaneList[turnShiftManager.PlanePanel.transform.childCount - dice.diceNo].transform.GetChild(0).gameObject;
        }
       
    }
}
