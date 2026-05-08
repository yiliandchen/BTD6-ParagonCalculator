using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class TowerCost : MonoBehaviour
{
    private Dictionary<string, Tower> towerCostData = new Dictionary<string, Tower>();
    [SerializeField] private TextAsset csvFile;
    
    void Awake()
    {
        // Iterating CSV data
        string[] rows = csvFile.text.Split('\n');
        int count = 0;
        string[] costData = new string[3];
        for (int i = 0;  i < rows.Length; i++)
        {
            costData[count] = rows[i];
            if (count == 2)
            {
                string towerName = costData[0].Split(',')[0];
                Tower tower = ScriptableObject.CreateInstance<Tower>();
                tower.SetCost(costData);
                towerCostData.Add(towerName, tower);

                count = -1;
            }

            count++;
        }
    }

    public int GetTowerCost(string towerName, string upgrades)
    {
        Tower tower = towerCostData[towerName];
        
        // Paragon cost
        if (upgrades == "Paragon") { return tower.GetParagonCost(); }

        // Upgrade cost
        return tower.GetUpgradeCost(upgrades);
    }
}

public class Tower: ScriptableObject
{
    private int baseCost, paragonCost;
    private int[,] upgradeCost =
    {
        {0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0},
    };

    public void SetCost(string[] costData)
    {
        for (int i = 0;  i < costData.Length; i++)
        {
            string[] pathCostData = costData[i].Split(',');

            // Get base and paragon cost
            if (i == 1) { baseCost = int.Parse(pathCostData[0]); }
            if (i == 2) { paragonCost = int.Parse(pathCostData[0]); }

            // Fill in tier costs
            for (int j = 1;  j < pathCostData.Length; j++)
            {
                upgradeCost[i, j] = int.Parse(pathCostData[j]);
            }
        }
    }

    public int GetBaseCost()
    {
        return baseCost;
    }
    
    public int GetParagonCost()
    {
        return paragonCost;
    }
    
    public int GetUpgradeCost(string upgrades)
    {
        int cost = baseCost;
        
        // Iterating through path
        for (int i = 0; i < upgrades.Length; i++)
        {
            int upgrade = upgrades[i] - '0';
            
            // Iterating through tier
            for (int j = 0; j < upgrade + 1; j++)
            {
                cost += upgradeCost[i, j];
            }
        }

        return cost;
    }
}
