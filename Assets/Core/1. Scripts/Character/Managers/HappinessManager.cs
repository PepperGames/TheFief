using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HappinessManager : MonoBehaviour
{
    [Inject] private CharacterManager _characterManager;

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
    }

    public void Recalculate(Character character)
    {
        Recalculate(character.CharacterData.Estates);
    }

    public void Recalculate(Estates estate)
    {
        switch (estate)
        {
            case Estates.Peasants:
                PeasantsHappiness = Recalculate(_characterManager.PeasantsCharacters);
                break;
            case Estates.Merchants:
                MerchantsHappiness = Recalculate(_characterManager.PeasantsCharacters);
                break;
            case Estates.Priests:
                PriestsHappiness = Recalculate(_characterManager.PeasantsCharacters);
                break;
            case Estates.Peers:
                PeersHappiness = Recalculate(_characterManager.PeasantsCharacters);
                break;
        }
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
    }
}
