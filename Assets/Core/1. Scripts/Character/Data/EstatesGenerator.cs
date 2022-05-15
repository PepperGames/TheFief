using UnityEngine;

public static class EstatesGenerator
{
    private static readonly float _peasantsChance = 0.357f;   //0.35714285714 0.357
    private static readonly float _merchantsChance = 0.643f;  //0.28571428571 0.286
    private static readonly float _priestsChance = 0.857f;    //0.21428571428 0.214
    //private static readonly float _peersChance = 1f;          //0.14285714285 0.143

    public static Estates GetRandomEstates()
    {
        float chance = Random.Range(0, 1f);
        Estates result;

        if (chance < _peasantsChance)
        {
            result = Estates.Peasants;
        }
        else if (chance < _merchantsChance)
        {
            result = Estates.Merchants;
        }
        else if (chance < _priestsChance)
        {
            result = Estates.Priests;
        }
        else
        {
            result = Estates.Peers;
        }

        return result;
    }
}

public enum Estates
{
    Peasants = 0,   //Крестьяне
    Merchants = 1,  //Торговцы
    Priests = 2,    //Священники
    Peers = 3,      //Лорд
}
