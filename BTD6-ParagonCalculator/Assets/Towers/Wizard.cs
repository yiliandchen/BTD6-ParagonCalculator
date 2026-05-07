using UnityEngine;

[CreateAssetMenu(fileName = "Wizard", menuName = "Scriptable Objects/Wizard")]
public class Wizard : ScriptableObject
{
    public int base_cost = 250;
    public int paragon_cost = 750000;

    public int[,] upgrades =
    {
        {0, 150, 600, 1300, 10900, 32000},
        {0, 300, 900, 3000, 4000, 54000},
        {0, 300, 300, 1700, 2800, 24000}
    };
}
