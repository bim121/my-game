using UnityEngine;

public abstract class Item : ScriptableObject, IMoveable, IDescribable
{
    [SerializeField]
    private Sprite icon;

    [SerializeField] 
    private int stackSize;

    [SerializeField]
    private string title;

    [SerializeField]
    private Quality quality;

    private SlotScript slot;

    [SerializeField]
    private int price;

    public int MyPrice
    {
        get
        {
            return price;
        }
    }

    public Sprite MyIcon
    {
        get
        {
            return icon;
        }
    }

    public int MyStackSize
    {
        get
        {
            return stackSize;
        }
    }

    public SlotScript MySlot
    {
        get
        {
            return slot;
        }
        set
        {
            slot = value;
        }
    }

    public Quality MyQuality
    {
        get
        {
            return quality;
        }
        set
        {
            quality = value;
        }
    }

    public string MyTitle
    {
        get
        {
            return title;
        }
        set
        {
            title = value;
        }
    }

    public virtual string GetDescription()
    {
        return string.Format("<color={0}> {1}</color>", QualityColor.MyColor[quality], title);
    }

    public void Remove()
    {
        if (MySlot != null) {
            MySlot.RemoveItem(this);
        }
    }
}
