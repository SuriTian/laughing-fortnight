using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform originalParent; // where item is being moved from 
    CanvasGroup canvasGroup;

    
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent; //save og parent
        transform.SetParent(transform.root); // above other canvas
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f; // semi-transparency 
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; // follow mouse
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // can click on it again
        canvasGroup.alpha = 1f;

        Slot dropSlot = eventData.pointerEnter?.GetComponent<Slot>(); // new slot, ? means nullable
        
        if (dropSlot == null) // if we didn't find a slot 
        {
            GameObject dropItem = eventData.pointerEnter;

            if (dropItem) // if we have an item instead
            {
                dropSlot = dropItem.GetComponentInParent<Slot>(); // slot we want to drop into is the one behind the item 
            }
        }

        Slot originalSlot = originalParent.GetComponent<Slot>();

        if (dropSlot) // != null
        {
            // item swap
            if (dropSlot.currItem)
            {
                dropSlot.currItem.transform.SetParent(originalSlot.transform);
                originalSlot.currItem = dropSlot.currItem;
                dropSlot.currItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // vector2.zero snaps into middle
            }
            else
            {
                originalSlot.currItem = null;
            }

            // move targetted item into new slot
            transform.SetParent(dropSlot.transform);
            dropSlot.currItem = gameObject;
        }
        else
        {
            // player did not give reasonable attempt to get it into another slot
            transform.SetParent(originalParent); // snap back 
        }

        GetComponent<RectTransform>().anchoredPosition = Vector2.zero; 
    }
}
