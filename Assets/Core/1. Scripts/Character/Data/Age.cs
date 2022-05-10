using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Age
{
    private const float a = 0.00001f;
    private const float b = 0.0002f;
    private const float c = -0.001f;

    public int years = 0;
    //public int years = 0;

    public void Test()
    {
        for (int i = 0; i < 100; i++)
        {
            years++;
            GrowOld();
        }
    }

    private bool GrowOld()
    {
        float probabilityToDie = ProbabilityToDie();
        float chance = Random.Range(0, 1f);

        Debug.Log("chance " + chance);
        if (probabilityToDie >= chance)
        {
            Debug.Log("years" + years);
            Debug.Log("Die");
            return true;
        }
        return false;
    }

    private float ProbabilityToDie()
    {
        float result = (float)(a * Math.Pow(years, 2) + b * years + c);
        Debug.Log("result " + result);
        return result;
    }
}