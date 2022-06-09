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
            Debug.Log(_faith);
            OnFaithChange?.Invoke();
        }
    }

    public Action OnFaithChange;

    public void Recalculate()
    {
        Faith = Recalculate(services.CharacterManager.AliveCharacters);
        Debug.Log("Recalculate");
    }

    public void Recalculate(Character character)
    {
        Faith = Recalculate(services.CharacterManager.AliveCharacters);
        Debug.Log("Recalculate");
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

        Debug.Log("Recalculate");
        return result;
    }
}