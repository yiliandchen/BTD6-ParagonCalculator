using System;
using UnityEngine;

public class NKCustomRound : MonoBehaviour
{
    public int NKRound(float value)
    {
        float result = (float)Math.Round(value / 5f) * 5;
        return (int)result;
    }
}
