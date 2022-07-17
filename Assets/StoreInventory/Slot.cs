using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{

    public Item item;

    public Image itemicon;
    
    public Text cost;

    public void ItemOnclicked()
    {
        InventoryManager.UpdateItemInfo(item.Itemname + item.Description);
    }


}
