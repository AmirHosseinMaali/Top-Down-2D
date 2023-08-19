using TMPro;
using UnityEngine;
public class EconomyManager : Singleton<EconomyManager>
{
    TMP_Text goldCountText;

    int currentGold = 0;

    const string COIN_AMOUNT_TEXT = "GoldAmountText";
    public void UpdateCurrentGold()
    {
        currentGold++;
        if (goldCountText == null)
        {
            goldCountText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }
        goldCountText.text = currentGold.ToString("D4");
    }
}
