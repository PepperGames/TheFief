using UnityEngine;
using Random = UnityEngine.Random;

public static class GenderGenerator
{
    private static readonly float _femaleChance = 0.6f;

    public static Genders GetRandomGender()
    {
        float chance = Random.Range(0, 1f);

        if (chance <= _femaleChance)
        {
            return Genders.Female;
        }
        else
        {
            return Genders.Male;
        }
    }
}

public enum Genders
{
    Female = 0, //женский пол
    Male = 1 //мужской пол
}