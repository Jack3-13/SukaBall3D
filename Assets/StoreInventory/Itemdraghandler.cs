using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Itemdraghandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public Transform originalparent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalparent = transform.parent;
        transform.SetParent(transform.parent.parent.parent);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        
        /*Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);*/
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    
        if (eventData.pointerCurrentRaycast.gameObject.name == "Image")
        {
          
            transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
            transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;
            eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalparent.position;
            eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalparent);
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            return;
        }

        else
        {
            transform.SetParent(originalparent);
            transform.position = originalparent.transform.position;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            return;
        }

       
    }

    
}
