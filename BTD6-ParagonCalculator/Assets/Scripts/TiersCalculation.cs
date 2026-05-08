using UnityEngine;
using TMPro;
using System.Linq;

public class TiersCalculation : MonoBehaviour
{
    [SerializeField] private TMP_InputField upgradesInp, towersInp;
    [SerializeField] private TextMeshProUGUI noTiers;
    private string upgrades, towers;
    private int noUpgrades, noTowers;
    public void YDC_RunFunction()
    {
        upgrades = upgradesInp.text; towers = towersInp.text;
        if (string.IsNullOrEmpty(towers))
        {
            towers = "1";
        }
        if (string.IsNullOrEmpty(upgrades))
        {
            upgrades = "000";
        }

        noUpgrades = upgrades.ToCharArray().Select(c => c - '0').Sum();
        noTowers = int.Parse(towers);

        noTiers.text = (noTowers * noUpgrades).ToString();
    }
}
