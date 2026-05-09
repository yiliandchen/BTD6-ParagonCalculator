using System;
using TMPro;
using UnityEngine;

public class CostCalculation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI towerNameInp;
    [SerializeField] private TMP_Dropdown difficultyInp;
    [SerializeField] private TMP_InputField upgradesInp, towersInp;
    [SerializeField] private TextMeshProUGUI cost;
    
    private TowerCost towerCostData;
    private DifficultyCostScale difficultyCostScale;
    private NKCustomRound customRound;

    void Start()
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

        if (string.IsNullOrEmpty(upgrades))
        {
            upgrades = "000";
        }
        if (string.IsNullOrEmpty(towers))
        {
            towers = "1";
        }

        // Calculating tower cost
        float totalCost, upgradeCost;
        float difficultyScale = difficultyCostScale.GetDifficultyCostScale(difficulty);
        
        totalCost = 0;

        upgradeCost = towerCostData.GetUpgradeCost(towerName, 0, 0); // Base cost at [0,0]
        upgradeCost *= difficultyScale;
        totalCost += customRound.NKRound(upgradeCost);

        for (int path = 1; path <= 3; path++)
        {
            int towerTier = upgrades[path - 1] - '0';
            for (int tier = 0; tier <= towerTier; tier++)
            {
                upgradeCost = towerCostData.GetUpgradeCost(towerName, path, tier);
                upgradeCost *= difficultyScale;
                totalCost += customRound.NKRound(upgradeCost);
            }
        }
        
        int noTowers = int.Parse(towers);
        totalCost *= noTowers;
        
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
