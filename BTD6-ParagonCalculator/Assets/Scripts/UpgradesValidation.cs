using UnityEngine;
using TMPro;

public class UpgradesValidation : MonoBehaviour
{
    public TMP_InputField inputField;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void CheckUpgrade(string input)
    {
        // Checking number of characters
        if (input.Length != 3)
        {
            inputField.text = "";
            return;
        }

        // Check valid upgrades
        int path = 0, high_tier = 0;
        foreach (char c in input)
        {
            // Check correct tiers
            if (c >= '0' && c <= '5')
            {
                // Checking paths
                if (c > '0')
                {
                    path += 1;
                }

                // Checking tier 3-5
                if (c > '2')
                {
                    high_tier += 1;
                }

                // Checking invalid tiers and paths
                if (path > 2 || high_tier > 1)
                {
                    inputField.text = "";
                    return;
                }
            }
            else
            {
                inputField.text = "";
                return;
            }
        }
    }
}
