using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class LootButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField]
    private Image icon;

    [SerializeField]
    private TMP_Text title;

    private LootWindow lootWindow;

    public Image MyIcon
    {
        get
        {
            return icon;
        }
    }

    public TMP_Text MyTitle
    {
        get
        {
            return title;
        }
    }

    private void Awake()
    {
        lootWindow = GetComponentInParent<LootWindow>();
    }

    public Item MyLoot { get; set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (InventoryScript.MyInstance.AddItem(MyLoot))
        {
            gameObject.SetActive(false);
            lootWindow.TakeLoot(MyLoot);
            UIManager.MyInstance.HideTooltip();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.MyInstance.ShowTooltip(transform.position, MyLoot);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();
    }
}
