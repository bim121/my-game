using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chest : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite openSprite, closedSprite;

    [SerializeField]
    private CanvasGroup canvasGroup;

    private List<Item> items;

    [SerializeField]
    private BagScript bag;

    private bool isPlayerInTrigger = false;

    private bool isOpen;

    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
    }

    private void Interact()
    {
        if (isOpen)
        {
            StopInteract();
        }
        else
        {
            AddItems();
            isOpen = true;  
            spriteRenderer.sprite = openSprite;
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }
    }

    public void StopInteract()
    {
        StoreItems();
        bag.Clear();
        isOpen = false;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        spriteRenderer.sprite = closedSprite;
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

    public void AddItems()
    {
        if(items != null)
        {
            foreach(Item item  in items)
            {
                item.MySlot.AddItem(item);
            }
        }
    }

    public void StoreItems()
    {
        items = bag.GetItems();
    }
}
