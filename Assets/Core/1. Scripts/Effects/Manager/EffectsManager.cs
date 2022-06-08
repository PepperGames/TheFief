using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    [SerializeField] private List<EffectBox> _effectBoxes;
    [SerializeField] private Character _character;

    public List<EffectBox> EffectBoxes => _effectBoxes;

    public void Restart()
    {
        ClearEffectsList();
    }

    public void InitializeEffect(EffectBox effectBoxPrefab)
    {
        switch (effectBoxPrefab.EffectType)
        {
            case EffectType.Renewable:
                if (!(_effectBoxes.FirstOrDefault(x => x.GetType() == effectBoxPrefab.GetType()) is RenewableEffectBox oldEffect))
                {
                    AddEffectToList(effectBoxPrefab);
                }
                else
                {
                    oldEffect.ExtendTime();
                }
                break;

            case EffectType.NonRenewable:
                AddEffectToList(effectBoxPrefab);
                break;

            case EffectType.Permanent:
                AddEffectToList(effectBoxPrefab);
                break;
        }
    }

    private void AddEffectToList(EffectBox effectBoxPrefab)
    {
        EffectBox effectBox = Instantiate(effectBoxPrefab, transform);
        _effectBoxes.Add(effectBox);
        effectBox.Activate(_character);
        effectBox.OnEnd += RemoveEffectFromList;
            }

    private void ClearEffectsList()
    {
        foreach (EffectBox effectBox in _effectBoxes)
        {
            effectBox.OnEnd -= RemoveEffectFromList;
            effectBox.Deactivate();
        }
        _effectBoxes = new List<EffectBox>();
    }

    private void RemoveEffectFromList(EffectBox effectBox)
    {
        effectBox.OnEnd -= RemoveEffectFromList;
        effectBox.Deactivate();
        _effectBoxes.Remove(effectBox);
        Destroy(effectBox.gameObject);
    }
}
