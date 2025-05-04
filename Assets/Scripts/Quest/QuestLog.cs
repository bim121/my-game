using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class QuestLog : MonoBehaviour
{
    [SerializeField]
    private GameObject questPrefab;

    [SerializeField]
    private Transform questParent;

    private Quest selected;

    [SerializeField]
    private TMP_Text questDescription;

    private List<QuestScript> questScripts = new List<QuestScript>();

    private static QuestLog instance;

    public static QuestLog MyInstance
    {
        get 
        { 
            if(instance == null)
            {
                instance = FindObjectOfType<QuestLog>();
            }
            return instance;
        }
    }

    public void AcceptQuest(Quest quest)
    {
        foreach(CollectObjective o in quest.MyCollectObjectives)
        {
            InventoryScript.MyInstance.itemCountChangedEvent += new ItemCountChanged(o.UpdateItemCount);
        }

        GameObject go = Instantiate(questPrefab, questParent);

        QuestScript qs = go.GetComponent<QuestScript>();
        quest.MyQuestScript = qs;
        qs.MyQuest = quest;

        questScripts.Add(qs);

        go.GetComponent<TextMeshProUGUI>().text = quest.MyTitle;
    }

    public void UpdateSelected()
    {
        ShowDescription(selected);
    }

    public void ShowDescription(Quest quest)
    {
        if (quest != null)
        {
            if (selected != null && selected != quest)
            {
                selected.MyQuestScript.DeSelect();
            }

            string objective = string.Empty;

            selected = quest;

            foreach (Objective obj in quest.MyCollectObjectives)
            {
                objective += obj.MyType + ": " + obj.MyCurrentAmount + "/" + obj.MyAmount + "\n";
            }

            questDescription.text = string.Format("{0}\n<size=32>{1}\n\nObjectives\n{2}</size>", quest.MyTitle, quest.MyDescription, objective);
        }
    }

    public void CheckCompletion()
    {
        foreach(QuestScript qs in questScripts)
        {
            qs.IsComplete();
        }
    }
}
