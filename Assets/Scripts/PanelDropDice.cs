using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelDropDice : MonoBehaviour, IDropHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("dropped");
        GameObject dropped = eventData.pointerDrag;
        DragDrop draggableItem = dropped.GetComponent<DragDrop>();
        
        
        if (dropped.GetComponent<DiceInstance>().IsBlueDice == true && gameObject.CompareTag("Pilot") && dropped == GameManager.Instance.currentDraggableDice)
        {
            dropped.transform.SetParent(gameObject.transform);
            draggableItem.parentAfterDrag = transform;
            GameManager.Instance.OnDiceDrag.Invoke();
            GameManager.Instance.currentDraggableDice = null;
        }
        else if (dropped.GetComponent<DiceInstance>().IsBlueDice == false && gameObject.CompareTag("CoPilot") && dropped == GameManager.Instance.currentDraggableDice)
        {
            dropped.transform.SetParent(gameObject.transform);
            draggableItem.parentAfterDrag = transform;
            GameManager.Instance.OnDiceDrag.Invoke();
            GameManager.Instance.currentDraggableDice = null;
        }

       
       
    }
}
