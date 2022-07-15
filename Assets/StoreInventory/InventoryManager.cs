using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryManager : MonoBehaviour
{

    static InventoryManager instance;


    public PlayerInventory player1item;
    public PlayerInventory player2item;
    public PlayerInventory all;


    public Player player1;
    public Player player2;

    public List<Item> saleitems= new List<Item>();

    public GameObject itemgrid;

    public GameObject player1itemgrid;

    public GameObject player2itemgrid;

    public Text iteminfo;

    void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }


    private void OnEnable()
    {
        instance.iteminfo.text = "";
    }


    public static void UpdateItemInfo(string itemdescription)
    {
        instance.iteminfo.text = itemdescription;
    }

    void Start()
    {
        for (int i = 0; i < itemgrid.transform.childCount; i++)
        {
            GameObject grid = itemgrid.transform.GetChild(i).gameObject;

            Item item = saleitems[i];

            Slot slot = grid.GetComponent<Slot>();

            slot.item  = item;
            slot.itemicon.sprite = item.Image;
            slot.cost.text = item.cost.ToString();

        }

        

        
    }




}
