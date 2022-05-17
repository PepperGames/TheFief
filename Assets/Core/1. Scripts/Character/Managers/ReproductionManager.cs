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
        Debug.Log("GetRandomDelay " + randomDelay);
        return randomDelay;
    }

    public void Reproduce()
    {
        Debug.Log("Reproduce");
        if (GetFamilyPair() != null)
        {
            CharacterData characterData = _services.CharacterManager.GenerateBornedCharacterData();
            _services.CharacterManager.SpawnCharacter(characterData);
        }
    }

    private ResidentialStructure GetResidentialStructure()
    {
        if (_services.StructureManager.woodenHutList.Count > 0)
        {
            int randomIndex = Random.Range(0, _services.StructureManager.woodenHutList.Count);
            ResidentialStructure residentialStructure = _services.StructureManager.woodenHutList[randomIndex];
            Debug.Log("+ " + residentialStructure);



            Debug.Log("- " + residentialStructure);
            return residentialStructure;
        }
        return null;
    }

    private FamilyPair GetFamilyPair()
    {
        //if (_services.StructureManager.woodenHutList.Count > 0)
        //{
        //    int randomIndex = Random.Range(0, _services.StructureManager.woodenHutList.Count);
        //    ResidentialStructure residentialStructure = _services.StructureManager.woodenHutList[randomIndex];
        //    Debug.Log("+ " + residentialStructure);



        //    Debug.Log("- " + residentialStructure);
        //    return residentialStructure;
        //}
        return null;
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

        public FamilyPair(Character father, Character mother)
        {
            this.father = father;
            this.mother = mother;
        }
    }
}
