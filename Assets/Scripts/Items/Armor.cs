using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ArmorType { Helmet, Shoulders, Chest, Gloves, Feet, Legs, MainHand, OffHand, TwoHand }

[CreateAssetMenu(fileName = "Armor", menuName = "Items/Armor", order = 2)]
public class Armor : Item
{
    [SerializeField]
    private ArmorType armorType;

    [SerializeField]
    private int intellect;
    
    [SerializeField]
    private int strength;

    [SerializeField]
    private int stamina;

    public ArmorType MyArmorType
    {
        get
        {
            return armorType;
        }

        set
        {
            armorType = value;
        }
    }

    public override string GetDescription()
    {
        string stats = string.Empty;

        if(intellect > 0)
        {
            stats += string.Format("\n +{0} intellect", intellect);
        }
        if (strength > 0)
        {
            stats += string.Format("\n +{0} strength", strength);
        }
        if (stamina > 0)
        {
            stats += string.Format("\n +{0} stamina", stamina);
        }

        return base.GetDescription() + stats;
    }

    public void Equip()
    {
        CharacterPanel.MyInstance.EquipArmor(this);
    }
}
