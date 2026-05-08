using System;
using UnityEngine;

public class NKCustomRound : MonoBehaviour
{
    public int NKRound (int value)
    {
        float result = (float)Math.Round((float)value / 5) * 5;
        return (int)result;
    }

    public int NKRound(float value)
    {
        float result = (float)Math.Round(value / 5) * 5;
        return (int)result;
    }
}
