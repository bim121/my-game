using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(fileName = "quest", menuName = "quest/quest", order = 1)]
[System.Serializable]
public class Quest : ScriptableObject
{
    [SerializeField]
    private string title;

    [SerializeField]
    private string description;

    [SerializeField]
    private CollectObjective[] collectObjectives;

    public QuestScript MyQuestScript { get; set; }

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

    public string MyDescription
    {
        get
        {
            return description;
        }
        set
        {
            description = value;
        }
    }

    public CollectObjective[] MyCollectObjectives
    {
        get
        {
            return collectObjectives;
        }
    }
    
    public bool IsComplete
    {
        get
        {
            foreach (CollectObjective obj in collectObjectives) {
                if (!obj.IsComplete)
                {
                    return false;
                }
            }

            return true;
        }
    }
}


[System.Serializable]
public abstract class Objective
{
    [SerializeField]
    public int amount;

    private int currentAmount;

    [SerializeField]
    private string type;

    public int MyAmount
    {
        get
        {
            return amount;
        }
    }

    public int MyCurrentAmount
    {
        get
        {
            return currentAmount;
        }
        set
        {
            currentAmount = value;
        }
    }

    public string MyType
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
        }
    }

    public bool IsComplete
    {
        get
        {
            return MyCurrentAmount >= MyAmount;
        }
    }
}

[System.Serializable]
public class CollectObjective: Objective
{
    public void UpdateItemCount(Item item)
    {
        if (MyType.ToLower() == item.MyTitle.ToLower())
        {
            MyCurrentAmount = InventoryScript.MyInstance.GetItemCount(item.MyTitle);
            QuestLog.MyInstance.UpdateSelected();
            QuestLog.MyInstance.CheckCompletion();
        }
    }
}
