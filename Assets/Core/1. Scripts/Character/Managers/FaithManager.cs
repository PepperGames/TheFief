using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FaithManager : MonoBehaviour
{
    [Inject] private Services services;

    [SerializeField] private float _faith = 0;

    public float Faith
    {
        get
        {
            if (_faith > 100)
                return 100;
            return _faith;
        }
        private set
        {
            _faith = value;
            OnFaithChange?.Invoke();
        }
    }

    public Action OnFaithChange;

    public void Recalculate()
    {
        Faith = Recalculate(services.CharacterManager.AliveCharacters);
    }

    public void Recalculate(Character character)
    {
        Faith = Recalculate(services.CharacterManager.AliveCharacters);
    }

    public float Recalculate(List<Character> characters)
    {
        float result = 0;
        foreach (Character character in characters)
        {
            result += character.CharacterData.Faith.IndexOfFaith;
        }
        if (characters.Count != 0)
        {
            result /= characters.Count;
        }

        return result;
    }
}