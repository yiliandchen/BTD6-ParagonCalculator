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

    public int GetUpgradeCost(string towerName, int path, int tier)
    {
        Tower tower = towerCostData[towerName];
        return tower.GetUpgradeCost(path, tier);
        // Base cost at [0,0], Paragon cost at [0,5]
    }
}

public class Tower: ScriptableObject
{
    // Base cost at [0,0], Paragon cost at [0,5]
    private int[,] upgradeCost =
    {
        {0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0},
    };

    public void SetCost(string[] costData)
    {
        for (int i = 0;  i < 3; i++)
        {
            int path = i + 1;
            string[] pathCostData = costData[i].Split(',');

            // Get base and paragon cost
            if (i == 1) { upgradeCost[0,0] = int.Parse(pathCostData[0]); }
            if (i == 2) { upgradeCost[0,5] = int.Parse(pathCostData[0]); }

            // Fill in tier costs
            for (int tier = 1; tier <= 5; tier++)
            {
                upgradeCost[path, tier] = int.Parse(pathCostData[tier]);
            }
        }
    }

    public int GetUpgradeCost(int path, int tier)
    {
        return upgradeCost[path, tier];
    }
}
