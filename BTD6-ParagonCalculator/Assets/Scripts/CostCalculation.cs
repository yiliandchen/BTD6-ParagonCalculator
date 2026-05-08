using System;
using TMPro;
using UnityEngine;

public class CostCalculation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI towerNameInp;
    [SerializeField] private TMP_Dropdown difficultyInp;
    [SerializeField] private TMP_InputField upgradesInp, towersInp, discountInp;
    [SerializeField] private TextMeshProUGUI cost;
    private TowerCost towerCostData;
    private DifficultyCostScale difficultyCostScale;
    private NKCustomRound customRound;

    public void Start()
    {
        towerCostData = GameObject.FindWithTag("Tower Cost").GetComponent<TowerCost>();
        difficultyCostScale = GameObject.FindWithTag("Difficulty Cost Scale").GetComponent<DifficultyCostScale>();
        customRound = GameObject.FindWithTag("Custom Round").GetComponent<NKCustomRound>();
    }

    public void YDC_RunFunction()
    {
        string towerName = towerNameInp.text;
        string difficulty = difficultyInp.options[difficultyInp.value].text;
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

        int towerCost = towerCostData.GetTowerCost(towerName, upgrades);
        int noTowers = int.Parse(towers);
        float costFraction = (100 - float.Parse(discount)) / 100;

        float difficultyScale = difficultyCostScale.GetDifficultyCostScale(difficulty);

        float totalCost = (towerCost * noTowers * costFraction * difficultyScale);
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
