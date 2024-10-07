using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;
    void Start()
    {
        GameManager.Instance.OnDiceDrag.AddListener(ToggleDiceDrag);
        GameManager.Instance.OnAllEnable.AddListener(ToggleAllDice);
        GameManager.Instance.OnAllDisable.AddListener(ToggleAllDiceFalse);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        
       

            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            image.raycastTarget = false;

      
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log(parentAfterDrag.transform.childCount);
        
        transform.position = Input.mousePosition;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
        transform.SetParent((parentAfterDrag));
        image.raycastTarget = true;
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    void ToggleDiceDrag()
    {
        if(gameObject != GameManager.Instance.currentDraggableDice) this.enabled = !this.enabled;
    }

    void ToggleAllDice()
    {
        this.enabled = true;
    }

    void ToggleAllDiceFalse()
    { this.enabled = false; }
}
