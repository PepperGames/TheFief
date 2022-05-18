using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HappinessManager : MonoBehaviour
{
    [Inject] private Services services;

    [SerializeField] private float _peasantsHappiness = 0;
    [SerializeField] private float _merchantsHappiness = 0;
    [SerializeField] private float _priestsHappiness = 0;
    [SerializeField] private float _peersHappiness = 0;

    public float PeasantsHappiness
    {
        get
        {
            if (_peasantsHappiness > 100)
                return 100;
            return _peasantsHappiness;
        }
        private set
        {
            _peasantsHappiness = value;
        }
    }

    public float MerchantsHappiness
    {
        get
        {
            if (_merchantsHappiness > 100)
                return 100;
            return _merchantsHappiness;
        }
        private set
        {
            _merchantsHappiness = value;
        }
    }

    public float PriestsHappiness
    {
        get
        {
            if (_priestsHappiness > 100)
                return 100;
            return _priestsHappiness;
        }
        private set
        {
            _priestsHappiness = value;
        }
    }

    public float PeersHappiness
    {
        get
        {
            if (_peersHappiness > 100)
                return 100;
            return _peersHappiness;
        }
        private set
        {
            _peersHappiness = value;
        }
    }

    public void Recalculate()
    {
        Recalculate(Estates.Peasants);
        Recalculate(Estates.Merchants);
        Recalculate(Estates.Priests);
        Recalculate(Estates.Peers);
        Debug.Log("Recalculate");
    }

    public void Recalculate(Character character)
    {
        Recalculate(character.CharacterData.Estates);
        Debug.Log("Recalculate");
    }

    public void Recalculate(Estates estate)
    {
        switch (estate)
        {
            case Estates.Peasants:
                PeasantsHappiness = Recalculate(services.CharacterManager.PeasantsCharacters);
                break;
            case Estates.Merchants:
                MerchantsHappiness = Recalculate(services.CharacterManager.PeasantsCharacters);
                break;
            case Estates.Priests:
                PriestsHappiness = Recalculate(services.CharacterManager.PeasantsCharacters);
                break;
            case Estates.Peers:
                PeersHappiness = Recalculate(services.CharacterManager.PeasantsCharacters);
                break;
        }
        Debug.Log("Recalculate");
    }

    public float Recalculate(List<Character> characters)
    {
        float result = 0;
        foreach (Character character in characters)
        {
            result += character.CharacterData.Happiness.IndexOfHappiness;
        }
        result /= characters.Count;
        return result;
        Debug.Log("Recalculate");
    }
}
