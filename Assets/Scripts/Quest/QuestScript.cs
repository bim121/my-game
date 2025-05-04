using UnityEngine;
using TMPro;

public class QuestScript : MonoBehaviour
{
    public Quest MyQuest { get; set; }

    private bool markedComplete = false; 

    public void Select()
    {
        GetComponent<TextMeshProUGUI>().color = Color.red;

        QuestLog.MyInstance.ShowDescription(MyQuest);
    }

    public void DeSelect()
    {
        GetComponent<TextMeshProUGUI>().color = Color.white;
    }    

    public void IsComplete()
    {
        if (MyQuest.IsComplete && !markedComplete)
        {
            markedComplete = true;
            GetComponent<TextMeshProUGUI>().text += "(Complete)";
        }
        else if (!MyQuest.IsComplete)
        {
            markedComplete = false;
            GetComponent<TextMeshProUGUI>().text = MyQuest.MyTitle;
        }
    }
}
