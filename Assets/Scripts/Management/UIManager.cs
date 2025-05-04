using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    [SerializeField]
    private ActionButton[] actionButtons;

    [SerializeField]
    private CanvasGroup spellBook;

    [SerializeField]
    private GameObject tooltip;

    [SerializeField]
    private CharacterPanel characterPanel;

    private TMP_Text tooltipText;

    public static UIManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }

            return instance;
        }
    }

    private void Awake()
    {
        tooltipText = tooltip.GetComponentInChildren<TMP_Text>();
    }

    void Start()
    {
        SetUseable(actionButtons[0], SpellBook.MyInstance.GetSpell("Fireball"));
        SetUseable(actionButtons[1], SpellBook.MyInstance.GetSpell("FrostBolt"));
        SetUseable(actionButtons[2], SpellBook.MyInstance.GetSpell("LightningBolt"));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            InventoryScript.MyInstance.OpenClose();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            OpenClose(spellBook);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            characterPanel.OpenClose();
        }
    }

    public void UpdateStackSize(IClickable clickable)
    {
        if (clickable.MyCount > 1)
        {
            clickable.MyStackText.text = clickable.MyCount.ToString();
            clickable.MyStackText.color = Color.white;
            clickable.MyIcon.color = Color.white;
        }
        else
        {
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
            clickable.MyIcon.color = Color.white;
        }
        if (clickable.MyCount == 0) 
        {
            clickable.MyIcon.color = new Color(0, 0, 0, 0);
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
        }
    }

    public void SetUseable(ActionButton btn, Spell spell)
    {
        btn.MyIcon.sprite = spell.MyIcon;
        btn.MyIcon.color = Color.white;
    }

    public void OpenClose(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;
        canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;
    }

    public void ShowTooltip(Vector3 position, IDescribable description, bool isActionButton = false)
    {
        tooltip.SetActive(true);
        tooltip.transform.position = position;
        if (isActionButton)
        {
            tooltip.GetComponent<RectTransform>().pivot = new Vector2(0, -0.3f);
        }
        else
        {
            tooltip.GetComponent<RectTransform>().pivot = new Vector2(1, 0);
        }
        tooltipText.text = description.GetDescription();
    }

    public void HideTooltip()
    {
        tooltip.SetActive(false);
    }

    public void RefreshTooltip(IDescribable description)
    {
        tooltipText.text = description.GetDescription();
    }
}
