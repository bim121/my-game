using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class LootTable : MonoBehaviour
{
    [SerializeField]
    private Loot[] loot;

    public LootBag lootBag;

    private List<Item> droppedItems = new List<Item>();

    private bool rolled;

    public void ShowLoot()
    {
        if (!rolled)
        {
            RollLoot();
        }

        LootWindow.MyInstance.CreatePages(droppedItems, lootBag);
    }

    private void RollLoot()
    {
        foreach(Loot item in loot)
        {
            int roll = Random.Range(0, 100);

            if(roll <= item.MyDropChance)
            {
                droppedItems.Add(item.MyItem);
            }
        }

        rolled = true;
    }
}
