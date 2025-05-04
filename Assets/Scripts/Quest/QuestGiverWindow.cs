using UnityEngine;
using TMPro;
public class QuestGiverWindow : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    private QuestGiver questGiver;

    [SerializeField]
    private GameObject questPrefab;

    [SerializeField]
    private Transform questArea;

    public void Open(QuestGiver questGiver)
    {
        ShowQuest(questGiver);
        this.questGiver = questGiver;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public void Close()
    {
        questGiver.IsOpen = false;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        questGiver = null;
    }

    public void ShowQuest(QuestGiver questGiver)
    {
        this.questGiver = questGiver;

        foreach(Quest quest in questGiver.MyQuests)
        {
            GameObject go = Instantiate(questPrefab, questArea);
            go.GetComponent<TextMeshProUGUI>().text = quest.MyTitle;
        }
    }
}
