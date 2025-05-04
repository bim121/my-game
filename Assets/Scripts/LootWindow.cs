using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class LootWindow : MonoBehaviour
{
    private static LootWindow instance;

    public static LootWindow MyInstance
    {
        get {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<LootWindow>();
            }
            return instance; 
        }
        set { 
            instance = value; 
        }
    }

    private List<List<Item>> pages = new List<List<Item>>();

    private List<Item> droppedLoot = new List<Item>();

    private int pageIndex = 0;

    private LootBag myLootBag;

    [SerializeField]
    private LootButton[] lootButtons;

    [SerializeField]
    private Item[] items;

    public bool IsOpen
    {
        get
        {
            return canvasGroup.alpha > 0;
        }
    }

    [SerializeField]
    private TMP_Text pageNumber;

    [SerializeField]
    private GameObject nextBtn, previousBtn;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
       
    }

    public void CreatePages(List<Item> items, LootBag lootBag)
    {
        if (!IsOpen)
        {
            myLootBag = lootBag;

            List<Item> page = new List<Item>();

            droppedLoot = items;

            for (int i = 0; i < items.Count; i++)
            {
                page.Add(items[i]);

                if (page.Count == 4 || i == items.Count - 1)
                {
                    pages.Add(page);
                    page = new List<Item>();
                }
            }

            AddLoot();

            Open();
        }
    }

    private void AddLoot()
    {
        if (pages.Count > 0)
        {
            pageNumber.text = pageIndex + 1 + "/" + pages.Count;

            previousBtn.SetActive(pageIndex > 0);

            nextBtn.SetActive(pages.Count > 1 && pageIndex < pages.Count - 1);

            for (int i = 0; i < pages[pageIndex].Count; i++)
            {
                if (pages[pageIndex][i] != null)
                {
                    lootButtons[i].MyIcon.sprite = pages[pageIndex][i].MyIcon;

                    lootButtons[i].MyLoot = pages[pageIndex][i];

                    lootButtons[i].gameObject.SetActive(true);

                    string title = string.Format("<color={0}>{1}</color>", QualityColor.MyColor[pages[pageIndex][i].MyQuality], pages[pageIndex][i].MyTitle);

                    lootButtons[i].MyTitle.text = title;
                }
            }
        }
    }

    public void ClearButtons()
    {
        foreach(LootButton btn in lootButtons)
        {
            btn.gameObject.SetActive(false);
        }
    }

    public void NextPage()
    {
        if (pageIndex < pages.Count - 1) {
            pageIndex++;
            ClearButtons();
            AddLoot();
        }
    }

    public void PreviousPage()
    {
        if(pageIndex > 0)
        {
            pageIndex--;
            ClearButtons();
            AddLoot();
        }
    }

    public void TakeLoot(Item loot)
    {
        pages[pageIndex].Remove(loot);

        droppedLoot.Remove(loot);

        if (pages[pageIndex].Count == 0) {
            pages.Remove(pages[pageIndex]);

            if (pageIndex == pages.Count && pageIndex > 0)
            {
                pageIndex--;
            }

            AddLoot();
        }
    }

    public void Close()
    {
        pages.Clear();
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        ClearButtons();
        if (droppedLoot.Count == 0)
        {
            myLootBag.DestroyLootBag();
        }
    }

    public void Open()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
}
