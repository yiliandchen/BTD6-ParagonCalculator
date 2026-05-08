using UnityEngine;
using TMPro;
using System.Linq;

public class TiersCalculation : MonoBehaviour
{
    [SerializeField] private TMP_InputField upgradesInp, towersInp;
    [SerializeField] private TextMeshProUGUI noTiers;

    public void YDC_RunFunction()
    {
        string upgrades = upgradesInp.text, towers = towersInp.text;
        if (string.IsNullOrEmpty(upgrades))
        {
            upgrades = "000";
        }
        if (string.IsNullOrEmpty(towers))
        {
            towers = "1";
        }

        int noUpgrades = upgrades.ToCharArray().Select(c => c - '0').Sum();
        int noTowers = int.Parse(towers);

        noTiers.text = (noTowers * noUpgrades).ToString();
    }
}
