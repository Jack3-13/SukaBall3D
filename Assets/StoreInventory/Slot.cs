using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{

    public Item item;

    public Image itemicon;
    
    public Text cost;

    public GameObject itempos;

    public void start()
    {

    }


    public void Update()
    {
        if (item == null)
        {
            itempos.SetActive(false);
        }
    }

    public void ItemOnclicked()
    {
        InventoryManager.UpdateItemInfo(item.Itemname + item.Description);
    }

   


}
