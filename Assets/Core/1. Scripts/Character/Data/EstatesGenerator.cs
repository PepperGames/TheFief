using UnityEngine;

public static class EstatesGenerator
{
    private static readonly float _peasantsChance = 0.6f;
    private static readonly float _merchantsChance = 0.8f;
    private static readonly float _priestsChance = 0.9f;
    private static readonly float _peersChance = 1f;
   
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

        Debug.Log("Estates " + result);
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
