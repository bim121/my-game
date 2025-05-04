using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class SpellBook : MonoBehaviour
{
    private static SpellBook instance;

    public static SpellBook MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SpellBook>();
            }

            return instance;
        }
    }

    [SerializeField]
    private Image castingBar;

    [SerializeField]
    private TMP_Text spellName;

    [SerializeField]
    private TMP_Text castTime;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private Spell[] spells;

    private Spell[] actionSpells;

    private Coroutine spellRoutine;

    private Coroutine fadeRoutine;

    public Spell[] MySpells
    {
        get { return spells; } 
        set { spells = value; } 
    }

    public Spell[] MyActionSpells
    {
        get { return actionSpells; }
        set { actionSpells = value; }
    }

    void Start()
    {
        actionSpells = spells.ToArray();
    }

    public Spell CastSpell(int index)
    {
        if (spellRoutine != null) {
            return null;
        }

        castingBar.fillAmount = 0;

        castingBar.color = actionSpells[index].MyBarColor;

        spellName.text = actionSpells[index].MyName;

        icon.sprite = actionSpells[index].MyIcon;

        spellRoutine = StartCoroutine(Progress(index));

        fadeRoutine = StartCoroutine(FadeBar());

        return actionSpells[index];
    }

    private IEnumerator Progress(int index)
    {
        float timePassed = Time.deltaTime;

        float rate = 1.0f / actionSpells[index].MyCastTime;

        float progress = 0.0f;

        while (progress <= 1.0)
        {
            castingBar.fillAmount = Mathf.Lerp(0, 1, progress);

            progress += rate * Time.deltaTime;

            timePassed += Time.deltaTime;

            castTime.text = (actionSpells[index].MyCastTime - timePassed).ToString("F2");

            if(spells[index].MyCastTime - timePassed < 0)
            {
                castTime.text = "0.00";
            }

            yield return null;
        }
    }

    public void StopCasting()
    {
        if (spellRoutine != null)
        {
            StopCoroutine(spellRoutine);
            spellRoutine = null;
        }

        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
            canvasGroup.alpha = 0;
            fadeRoutine = null;
        }
    }

    private IEnumerator FadeBar()
    {
        float timeLeft = Time.deltaTime;

        float rate = 1.0f / 0.50f;

        float progress = 0.0f;

        while (progress <= 1.0)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, progress);

            progress += rate * Time.deltaTime;

            yield return null;
        }
    }

    public Spell GetSpell(string spellName)
    {
        Spell spell = Array.Find(spells, x => x.MyName == spellName);

        return spell;
    }

    public Spell GetActionSpell(string spellName)
    {
        Spell spell = Array.Find(actionSpells, x => x.MyName == spellName);

        return spell;
    }
}
