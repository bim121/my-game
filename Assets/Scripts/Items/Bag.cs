using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public interface IUseable
{
    void Use();
}

[CreateAssetMenu(fileName="Bag", menuName="Items/Bag", order =1)]
public class Bag : Item, IUseable
{
    [SerializeField]
    public int slots;

    [SerializeField]
    private GameObject bagPrefab;

    public BagScript MyBagScript {  get; set; }

    public BagButton MyBagButton { get; set; }

    public int Slots
    {
        get
        {
            return slots;
        }
    }

    public int MyNoEmptySlotCount
    {
        get
        {
            int count = 0;

            foreach (SlotScript slot in MyBagScript.MySlots)
            {
                if (!slot.IsEmpty)
                {
                    count++;
                }
            }

            return count;
        }
    }

    public void Initialize(int slots)
    {
        this.slots = slots;
    }

    public void Use()
    {
        if (InventoryScript.MyInstance.CanAddBag)
        {
            Remove();
            MyBagScript = Instantiate(bagPrefab, InventoryScript.MyInstance.transform).GetComponent<BagScript>();
            MyBagScript.AddSlots(slots);

            if(MyBagButton == null)
            {
                InventoryScript.MyInstance.AddBag(this);
            }
            else
            {
                InventoryScript.MyInstance.AddBag(this, MyBagButton);
            }
        }
    }

    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n{0} slot bag", slots);
    }
}
