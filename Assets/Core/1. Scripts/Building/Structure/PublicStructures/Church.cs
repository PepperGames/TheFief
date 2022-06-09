using System.Collections.Generic;
using UnityEngine;

public class Church : PublicStructure
{
    [SerializeField] float _range = 0;
    [SerializeField] StructureCircle _circle;

    [SerializeField] private List<ResidentialStructure> structures;

    protected override void Start()
    {
        base.Start();
        _circle.SetRange(_range);
        _circle.Initialize();
        UpdateStructuresList();
        services.StructureManager.OnPlaceHouse += OnUpgrade;
    }

    protected override void OnUpgrade()
    {
        switch (LVL)
        {
            case 2:
                _range += 4;
                _circle.SetRange(_range);
                UpdateStructuresList();
                break;

            case 3:
                _range += 3;
                _circle.SetRange(_range);
                UpdateStructuresList();
                break;

            default:
                break;
        }
    }

    private void UpdateStructuresList()
    {
        foreach (ResidentialStructure residential in GetAllStructuresInRange())
        {
            if (structures.Contains(residential) == false)
            {
                structures.Add(residential);
                BufFaith(residential);
                Subscribe(residential);
            }
        }
    }

    private List<ResidentialStructure> GetAllStructuresInRange()
    {
        var qwe = Physics2D.OverlapCircleAll(transform.position, _range);

        List<ResidentialStructure> residentialStructures = new List<ResidentialStructure>();

        foreach (var item in qwe)
        {
            ResidentialStructure residentialStructure = item.gameObject.GetComponent<ResidentialStructure>();
            if (residentialStructure != null)
            {
                residentialStructures.Add(residentialStructure);
            }
        }
        return residentialStructures;
    }

    private void Subscribe(ResidentialStructure residential)
    {
        residential.CharacterPlaces.OnCharacterAdd += BufFaith;
        residential.CharacterPlaces.OnCharacterKick += DebufFaith;
        residential.OnDemolishAction += OnResidentialStructureDemolish;
    }

    private void OnResidentialStructureDemolish(ResidentialStructure residential)
    {
        residential.CharacterPlaces.OnCharacterAdd -= BufFaith;
        residential.CharacterPlaces.OnCharacterKick -= DebufFaith;
        residential.OnDemolishAction -= OnResidentialStructureDemolish;
    }

    private void BufFaith(ResidentialStructure residential)
    {
        foreach (var item in residential.CharacterPlaces.Characters)
        {
            BufFaith(item);
        }
    }

    private void BufFaith(Character character)
    {
        character.CharacterData.Faith.IndexOfFaith += 10;
        character.CharacterData.Faith.numberOfChurchesAround++;
    }

    private void DebufFaith(Character character)
    {
        character.CharacterData.Faith.IndexOfFaith -= 10;
        character.CharacterData.Faith.numberOfChurchesAround--;
    }

    private void DebufFaith(ResidentialStructure residential)
    {
        foreach (var item in residential.CharacterPlaces.Characters)
        {
            DebufFaith(item);
            OnResidentialStructureDemolish(residential);
        }
    }

    private void DebufFaith(List<ResidentialStructure> structures)
    {
        foreach (var item in structures)
        {
            DebufFaith(item);
        }
    }

    protected override void OnDemolish()
    {
        base.OnDemolish();
        DebufFaith(structures);
        services.StructureManager.OnPlaceHouse -= OnUpgrade;
    }
}
