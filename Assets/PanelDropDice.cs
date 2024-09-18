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
        draggableItem.parentAfterDrag = transform;
        dropped.transform.SetParent(gameObject.transform);
       
    }
}
