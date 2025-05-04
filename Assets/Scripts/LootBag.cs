using UnityEngine;

public class LootBag : MonoBehaviour
{
    private bool isPlayerInTrigger = false;

    private LootTable myLootTable;

    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
    }

    private void Interact()
    {
        myLootTable.ShowLoot();
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

    public void SetLootData(LootTable lootTable)
    {
        myLootTable = lootTable;

        myLootTable.lootBag = this;
    }

    public void DestroyLootBag()
    {
        Destroy(gameObject);
    }
}
