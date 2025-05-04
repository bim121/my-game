using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField]
    private Quest[] quests;

    [SerializeField]
    private QuestGiverWindow questGiverWindow;

    public Quest[] MyQuests
    {
        get
        {
            return quests;
        }
    }

    private bool isPlayerInTrigger = false;

    public bool IsOpen { get; set; }

    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
    }

    private void Interact()
    {
        if (!IsOpen)
        {
            IsOpen = true;
            questGiverWindow.Open(this);
        }
    }

    public void StopInteract()
    {
        if (IsOpen)
        {
            IsOpen = false;
            questGiverWindow.Close();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            isPlayerInTrigger = false;
        }
    }
}
