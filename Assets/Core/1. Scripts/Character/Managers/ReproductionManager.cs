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
        if (GetResidentialStructure() != null)
        {

            CharacterData characterData = _services.CharacterManager.GenerateRandomCharacterData();
            _services.CharacterManager.SpawnCharacter(characterData);
        }
    }

    private ResidentialStructure GetResidentialStructure()
    {
        ResidentialStructure residentialStructure = _services.StructureManager.woodenHutList[0];
        Debug.Log("+ " + residentialStructure);
        
        Debug.Log("- " + residentialStructure);
        return residentialStructure;
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
}
