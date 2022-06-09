using System.Collections;
using UnityEngine;

public class TestTrait : CharacterTrait
{
    public override void Initialiize(Character character)
    {
        base.Initialiize(character);
        _character = character;
    }

    protected override void Subscribe()
    {
        _character.CharacterData.FamilyTies.OnFamilyChange += Recheck;
    }   

    private void Recheck()
    {
        if (_character.CharacterData.FamilyTies.FamilySize >= 5)
        {
            Activate();
        }

        if (_character.CharacterData.FamilyTies.FamilySize < 5)
        {
            Deactivate();
        }
    }

    private void Start()
    {
        Debug.LogWarning("Start");
        StartCoroutine(Ewnumerable());
    }

    private IEnumerator Ewnumerable()
    {
        Debug.LogWarning("Ewnumerable");
        yield return new WaitForSeconds(3f);
        Activate();
        yield return new WaitForSeconds(5f);
        Deactivate();
    }

    protected override void Activate()
    {
        _character.EffectsManager.InitializeEffect(_effectBox);
    }

    protected override void Deactivate()
    {
        _character.EffectsManager.RemoveEffectByType(_effectBox);
    }
}
