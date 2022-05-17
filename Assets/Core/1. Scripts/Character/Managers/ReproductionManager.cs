using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class ReproductionManager : MonoBehaviour
{
    [Inject] private Services _services;


    [SerializeField] private float _minDelay;
    [SerializeField] private float _maxDelay;

    [SerializeField] private float _delay;

    private void Start()
    {
        _delay = GetRandomDelay();
    }

    private float GetRandomDelay()
    {
        float randomDelay = Random.Range(_minDelay, _maxDelay);
        return randomDelay;
    }

    public void Reproduce()
    {
        Debug.Log("Reproduce");
        if (GetFamilyPair() != null)
        {
            FamilyPair familyPair = GetFamilyPair();
            if (familyPair != null)
            {
                CharacterData characterData = _services.CharacterManager.GenerateBornedCharacterData(familyPair.mother, familyPair.father);
                _services.CharacterManager.SpawnCharacter(characterData);
            }
        }
    }

    private ResidentialStructure GetResidentialStructure()
    {
        Debug.Log("GetResidentialStructure");
        if (_services.StructureManager.woodenHutList.Count > 0)
        {
            int randomIndex = Random.Range(0, _services.StructureManager.woodenHutList.Count);
            ResidentialStructure residentialStructure = _services.StructureManager.woodenHutList[randomIndex];
            return residentialStructure;
        }
        return null;
    }

    private FamilyPair GetFamilyPair()
    {
        Debug.Log("GetFamilyPair");
        ResidentialStructure residentialStructure = GetResidentialStructure();

        if (residentialStructure != null)
        {
            if (residentialStructure.CharacterPlaces.Characters.Count >= 2)
            {
                List<int> unverifiedIndexes = new List<int>(residentialStructure.CharacterPlaces.Characters.Count);
                for (int i = 0; i < residentialStructure.CharacterPlaces.Characters.Count; i++)
                {
                    unverifiedIndexes.Add(i);
                }

                int randomIndex = Random.Range(0, residentialStructure.CharacterPlaces.Characters.Count);
                unverifiedIndexes.Remove(randomIndex);

                Character character = residentialStructure.CharacterPlaces.Characters[randomIndex];
                FamilyPair familyPair = new FamilyPair();
                Genders desiredPartnerGender;
                if (character.CharacterData.Gender == Genders.Female)
                {
                    familyPair.mother = character;
                    desiredPartnerGender = Genders.Male;
                }
                else
                {
                    familyPair.father = character;
                    desiredPartnerGender = Genders.Female;
                }

                return GetFamilyPair(residentialStructure, familyPair, desiredPartnerGender, unverifiedIndexes);
            }
        }
        return null;
    }

    private FamilyPair GetFamilyPair(ResidentialStructure residentialStructure, FamilyPair familyPair, Genders desiredPartnerGender, List<int> unverifiedIndexes)
    {
        Debug.Log("GetFamilyPair2");
        int randomIndex = Random.Range(0, residentialStructure.CharacterPlaces.Characters.Count);
        while (!unverifiedIndexes.Contains(randomIndex))
        {
            Debug.Log("+");
            randomIndex = Random.Range(0, residentialStructure.CharacterPlaces.Characters.Count);
        }

        unverifiedIndexes.Remove(randomIndex);

        Character character = residentialStructure.CharacterPlaces.Characters[randomIndex];

        if (character.CharacterData.Gender == desiredPartnerGender)
        {
            switch (character.CharacterData.Gender)
            {
                case Genders.Female:
                    familyPair.mother = character;
                    break;
                case Genders.Male:
                    familyPair.father = character;
                    break;
            }
            return familyPair;
        }
        else if (unverifiedIndexes.Count <= 0)
        {
            return null;
        }
        else
        {
            return GetFamilyPair(residentialStructure, familyPair, desiredPartnerGender, unverifiedIndexes);
        }
    }

    private void Update()
    {
        _delay -= Time.deltaTime;
        if (_delay <= 0)
        {
            Reproduce();
            _delay = GetRandomDelay();
        }
    }

    private class FamilyPair
    {
        public Character father;
        public Character mother;

        public FamilyPair()
        {
            father = null;
            mother = null;
        }

        public FamilyPair(Character father, Character mother)
        {
            this.father = father;
            this.mother = mother;
        }
    }
}
