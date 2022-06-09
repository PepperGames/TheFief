using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharacterFoodManager : MonoBehaviour
{
    [Inject] private Services services;

    [SerializeField] private float _peasantsRequiresFood;
    [SerializeField] private float _merchantsRequiresFood;
    [SerializeField] private float _priestsRequiresFood;
    [SerializeField] private float _peersRequiresFood;

    private void Start()
    {
        InGameTime.OnHourChange += TryToEat;
    }

    private void TryToEat()
    {
        if (InGameTime.Hour == 7 || InGameTime.Hour == 15 || InGameTime.Hour == 20)
        {
            foreach (Character character in services.CharacterManager.AliveCharacters)
            {
                switch (character.CharacterData.Estates)
                {
                    case Estates.Peasants:
                        Feed(character, _peasantsRequiresFood);
                        break;
                    case Estates.Merchants:
                        Feed(character, _merchantsRequiresFood);
                        break;
                    case Estates.Priests:
                        Feed(character, _priestsRequiresFood);
                        break;
                    case Estates.Peers:
                        Feed(character, _peersRequiresFood);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void Feed(Character character, float _requiresFood)
    {
        Resources resources = new Resources() { Food = _requiresFood };
        if (services.ResourcesManager.EnoughResources(resources))
        {
            services.ResourcesManager.SpendResources(resources);
            character.CharacterData.Happiness.IndexOfHappiness += 0.2f;
        }
        else
        {
            character.CharacterData.Happiness.IndexOfHappiness -= 0.3f;
        }
    }

    private void OnDestroy()
    {
        InGameTime.OnHourChange += TryToEat;
    }
}
