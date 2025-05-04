using UnityEngine;
using TMPro;

public class EconomyManager : Singleton<EconomyManager>
{
    private TMP_Text goldText;
    private int currentGold = 10;

    const string COIN_AMOUNT_TEXT = "Gold Amount Text";

    public int MyCurrentGold
    {
        get
        {
            return currentGold;
        }
    }

    public void UpdateCurrentGold()
    {
        currentGold += 1;

        UpdateTextGold();
    }

    public void UpdateGold(int gold)
    {
        currentGold += gold;

        UpdateTextGold();
    }

    public void UpdateTextGold()
    {
        if (goldText == null)
        {
            goldText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }

        goldText.text = currentGold.ToString("D3");
    }
}
