using UnityEngine;
using System;

[CreateAssetMenu(fileName = "vendorItem", menuName = "VendorItems/vendorItem", order = 1)]
public class VendorItem : ScriptableObject
{
    [SerializeField]
    private Item item;

    [SerializeField]
    private int quantity;

    [SerializeField]
    private bool unlimited;

    public Item MyItem
    {
        get
        {
            return item;
        }

        set
        {
            item = value; 
        }
    }

    public int MyQuantity
    {
        get
        {
            return quantity;
        }

        set
        {
            quantity = value;
        }
    }

    public bool MyUnlimited
    {
        get
        {
            return unlimited;
        }

        set
        {
            unlimited = value;
        }
    }
}
