using System.Collections.Generic;
using UnityEngine;

public class DifficultyCostScale : MonoBehaviour
{
    private Dictionary<string, float> difficultyScaleData = new Dictionary<string, float>();
    [SerializeField] private TextAsset csvFile;

    // Update is called once per frame
    void Awake()
    {
        // Iterating CSV data
        string[] rows = csvFile.text.Split('\n');
        foreach(string row in rows)
        {
            if (string.IsNullOrEmpty(row)) { continue; }

            string[] scaleData = row.Split(',');
            difficultyScaleData.Add(scaleData[0], float.Parse(scaleData[1]));
        }
    }

    public float GetDifficultyCostScale(string difficulty)
    {
        return difficultyScaleData[difficulty];
    }
}
