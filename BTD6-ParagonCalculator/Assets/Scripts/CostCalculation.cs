using System;
using TMPro;
using UnityEngine;

public class CostCalculation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI towerNameInp;
    [SerializeField] private TMP_InputField upgradesInp, towersInp, discountInp;
    [SerializeField] private TextMeshProUGUI cost;
    private TowerCost towerCostData;
    private NKCustomRound customRound;

    public void Start()
    {
        towerCostData = GameObject.FindWithTag("Tower Cost").GetComponent<TowerCost>();
        customRound = GameObject.FindWithTag("Custom Round").GetComponent<NKCustomRound>();
    }

    public void YDC_RunFunction()
    {
        string towerName = towerNameInp.text;
        string upgrades = upgradesInp.text;
        string towers = towersInp.text;
        string discount = discountInp.text;

        if (string.IsNullOrEmpty(upgrades))
        {
            upgrades = "000";
        }
        if (string.IsNullOrEmpty(towers))
        {
            towers = "1";
        }
        if (string.IsNullOrEmpty(discount))
        {
            discount = "0";
        }

        int towerCost = towerCostData.getTowerCost(towerName, upgrades);
        int noTowers = int.Parse(towers);
        float costFraction = (100 - float.Parse(discount)) / 100;

        float totalCost = (towerCost * noTowers * costFraction);
        totalCost = customRound.NKRound(totalCost);
        if (totalCost >= 1000000)
        {
            cost.text = Math.Round(totalCost / 1000000, 2).ToString() + 'M';
        }
        else if (totalCost >= 100000)
        {
            cost.text = Math.Round(totalCost/1000).ToString() + 'K';
        }
        else
        {
            cost.text = totalCost.ToString();
        }
    }
}
