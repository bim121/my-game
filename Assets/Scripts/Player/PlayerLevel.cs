using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class PlayerLevel : Singleton<PlayerLevel>
{
    const string EXP_SLIDER_TEXT = "Exp Slider";
    const string LEVEL_TEXT = "Level Text";
    private TMP_Text lvlText;
    private Slider expSlider;
    private int currentExp = 0;
    private int maxExp = 3;
    private int level = 1;
    private int levelMax = 4;

    private List<int> expToNextLvl = new List<int> { 
        5, 7, 9 
    };

    private void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        UpdateLevelSlider();
    }

    public void GainExp()
    {

        if (currentExp < maxExp)
        {
            currentExp += 1;
            UpdateLevelSlider();
        }

        if(currentExp == maxExp)
        {
            UpdateLevelUp();
        }
    }

    private void UpdateLevelSlider()
    {
        if (expSlider == null)
        {
            expSlider = GameObject.Find(EXP_SLIDER_TEXT).GetComponent<Slider>();
        }

        expSlider.maxValue = maxExp;
        expSlider.value = currentExp;
    }

    private void UpdateLevelUp()
    {
        if (level == levelMax)
        {
            return;
        }

        currentExp = 0;
        maxExp = expToNextLvl[level - 1];
        level++;
        UpdateLevelSlider();

        if (lvlText == null)
        {
            lvlText = GameObject.Find(LEVEL_TEXT).GetComponent<TMP_Text>();
        }

        lvlText.text = level.ToString() + " Lvl";
    }
}
