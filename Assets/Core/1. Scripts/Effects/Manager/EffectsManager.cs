using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class EffectsManager : MonoBehaviour
{
    //[Inject] private RewardManager _rewardManager;

    [SerializeField] private List<EffectBox> _effectBoxes;

    [SerializeField] private EffectView _effectViewPrefab;
    [SerializeField] private Transform _effectViewsContainer;

    private void Awake()
    {
        //GameManager.OnStartGame += Restart;
        //GameManager.OnStartGame += InitializeEffectsFromRewardManager;
        //GameManager.OnEndGame += Restart;
    }

    public void Restart()
    {
        ClearEffectsList();
    }

    private void InitializeEffectsFromRewardManager()
    {
        //foreach (RewardEffectBoxContainer item in _rewardManager.currentRewardEffectBoxContainers)
        //{
        //    InitializeEffect(item.effectBox);
        //}
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
        effectBox.Activate();
        effectBox.OnEnd += RemoveEffectFromList;

        if (effectBox.Duration > 0)
        {
            EffectView effectViewPrefab = Instantiate(_effectViewPrefab, _effectViewsContainer);
            effectViewPrefab.Initialize(effectBox);
        }
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

    private void OnDestroy()
    {
        //GameManager.OnStartGame -= Restart;
        //GameManager.OnStartGame -= InitializeEffectsFromRewardManager;
        //GameManager.OnEndGame -= Restart;
    }
}
