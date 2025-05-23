using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class VendorWindow : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private VendorButton[] vendorButtons;

    [SerializeField]
    private TMP_Text pageNumber;

    private int pageIndex;

    private List<List<VendorItem>> pages = new List<List<VendorItem>>();

    private Vendor vendor;

    public void Open(Vendor vendor)
    {
        this.vendor = vendor;
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public void Close ()
    {
        vendor.IsOpen = false;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        vendor = null;
    }

    public void CreatePages(VendorItem[] items)
    {
        pages.Clear();

        List<VendorItem> page = new List<VendorItem>();

        for(int i = 0; i < items.Length; i++)
        {
            page.Add(items[i]);

            if (page.Count == 8 || i == items.Length - 1)
            {
                pages.Add(page);
                page = new List<VendorItem>();
            }
        }

        AddItems();
    }

    public void AddItems()
    {
        pageNumber.text = pageIndex + 1 + "/" + pages.Count;

        if (pages.Count > 0)
        {
            for (int i = 0; i < pages[pageIndex].Count; i++)
            {
                if (pages[pageIndex][i] != null)
                {
                    vendorButtons[i].AddItem(pages[pageIndex][i]);
                }
            }
        }
    }

    public void NextPage()
    {
        if (pageIndex < pages.Count - 1)
        {
            ClearButtons();
            pageIndex++;
            AddItems();
        }
    }

    public void PreviousPage()
    {
        Debug.Log(pages.Count);
        if (pageIndex > 0)
        {
            ClearButtons();
            pageIndex--;
            AddItems();
        }
    }

    public void ClearButtons()
    {
        foreach (VendorButton btn in vendorButtons)
        {
            btn.gameObject.SetActive(false);
        }
    }
}
