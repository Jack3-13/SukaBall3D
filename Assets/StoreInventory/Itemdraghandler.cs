using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Itemdraghandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private Transform originalparent;
    private Item transaction;
    public PlayerInventory player1items;
    public PlayerInventory player2items;



    public void OnBeginDrag(PointerEventData eventData)
    {
        originalparent = transform.parent;
        transaction = originalparent.GetComponent<Slot>().item;
        transform.SetParent(transform.parent.parent.parent.parent);
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

        /* 从商店到角色道具*/
        if (originalparent.parent.gameObject.name == "SellItems")
        { 
  
            if (eventData.pointerCurrentRaycast.gameObject.name == "Image"
                & (eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.parent.gameObject.name == "Items"))
            {

                /* player1*/
                if (eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.parent.parent.gameObject.name == "Player1")
                {

                    if (InventoryManager.AddItem(transaction, player1items))
                    {
                        if (eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.gameObject.GetComponent<Slot>().item != null)
                        {
                            InventoryManager.RefundItem(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.gameObject.GetComponent<Slot>().item, player1items);
                            Switchpos(eventData);
                        }

                        else
                        {
                            Switchpos(eventData);
                        }
                        
                        
                    }
                    else
                    {
                        Backpos(eventData);
                    }

                }

                /* player2*/
                else if (eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.parent.parent.gameObject.name == "Player2")
                {

                    if (InventoryManager.AddItem(transaction, player2items))
                    {
                        if (eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.gameObject.GetComponent<Slot>().item != null)
                        {
                            InventoryManager.RefundItem(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.gameObject.GetComponent<Slot>().item, player2items);
                        }
                        Switchpos(eventData);
                    }
                    else
                    {
                        Backpos(eventData);
                    }

                }

            }

            else
            {
                Backpos(eventData);
            }
        }


        /* 从角色退回到商店*/
        else if (originalparent.parent.gameObject.name == "Items")
        {
            if (eventData.pointerCurrentRaycast.gameObject.name == "Image"
               & (eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.parent.gameObject.name == "SellItems"))
            {

                /* player1*/
                if (originalparent.parent.parent.gameObject.name == "Player1")
                {
                    if (eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.gameObject.GetComponent<Slot>().item != null)
                    {
                        InventoryManager.AddItem(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.gameObject.GetComponent<Slot>().item, player1items);
                    }

                    InventoryManager.RefundItem(transaction, player1items);
                    Switchpos(eventData);
                    

                }

                /* player2*/
                else if (originalparent.parent.parent.gameObject.name == "Player2")
                {
                    if (eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.gameObject.GetComponent<Slot>().item != null)
                    {
                        InventoryManager.AddItem(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.gameObject.GetComponent<Slot>().item, player2items);
                    }
                    InventoryManager.RefundItem(transaction, player2items);
                    Switchpos(eventData);

                }
            }

            else
            {
                Backpos(eventData);
            }
        }
       
    }


    private void Switchpos(PointerEventData eventData)
    {
        Item temp = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<Slot>().item;
        transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
        transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;
        transform.parent.GetComponent<Slot>().item = transaction;
        eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalparent.position;
        eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalparent);
        originalparent.GetComponent<Slot>().item = temp;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        return;
    }



    private void Backpos(PointerEventData eventData)
    {
        transform.SetParent(originalparent);
        transform.position = originalparent.transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        return;
    }


   

}
