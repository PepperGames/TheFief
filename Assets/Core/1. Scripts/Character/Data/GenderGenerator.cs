using UnityEngine;
using Random = UnityEngine.Random;

public static class GenderGenerator
{
    private static readonly float _femaleChance = 0.6f;

    public static Genders GetRandomGender()
    {
        float chance = Random.Range(0, 1f);

        Debug.Log("chance " + chance);
        if (chance <= _femaleChance)
        {
            Debug.Log(Genders.Female);
            return Genders.Female;
        }
        else
        {
            Debug.Log(Genders.Male);
            return Genders.Male;
        }
    }
}

public enum Genders
{
    Female = 0, //женский пол
    Male = 1 //мужской пол
}