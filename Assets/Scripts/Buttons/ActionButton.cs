using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class ActionButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Button MyButton { get; private set; }

    public Image MyIcon
    {
        get
        {
            return icon;
        }

        set
        {
            icon = value;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log(HandScript.MyInstance.MyMoveable);
            if (HandScript.MyInstance.MyMoveable != null)
            {
                UpdateVisual();
            }
        }
    }

    [SerializeField]
    private Image icon;

    [SerializeField]
    private int spellIndex;

    void Start()
    {
        MyButton = GetComponent<Button>();
    }

    public void UpdateVisual()
    {
        Spell newSpell = SpellBook.MyInstance.GetSpell((HandScript.MyInstance.MyMoveable as Spell).MyName);
        SpellBook.MyInstance.MyActionSpells[spellIndex] = newSpell;
        MyIcon.sprite = HandScript.MyInstance.Put().MyIcon;
        MyIcon.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Spell spell = SpellBook.MyInstance.MyActionSpells[spellIndex];
        UIManager.MyInstance.ShowTooltip(transform.position, spell, true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();
    }
}
