using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EffectView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _durationText;

    [SerializeField] private EffectBox _effectBox;

    public void Initialize(EffectBox effectBox)
    {
        _effectBox = effectBox;
        OnEventsSubscribe();
        InitializeInfo();
    }

    private void OnEventsSubscribe()
    {
        _effectBox.OnEnd += Deactivate;
    }

    private void InitializeInfo()
    {
        _image.sprite = _effectBox.Sprite;

        switch (_effectBox.EffectType)
        {
            case EffectType.Renewable:
                StartCoroutine(DisplayInfo());
                break;
            case EffectType.NonRenewable:
                StartCoroutine(DisplayInfo());
                break;
            case EffectType.Permanent:
                _durationText.text = "∞";
                break;
            default:
                break;
        }
    }

    private IEnumerator DisplayInfo()
    {
        while (true)
        {
            yield return null;
            string duration = Math.Round(_effectBox.Duration + 1, 0).ToString();
            _durationText.text = duration;
        }
    }

    private void OnEventsUnscribe()
    {
        _effectBox.OnEnd += Deactivate;
    }

    private void Deactivate(EffectBox effectBox)
    {
        StopAllCoroutines();
        Destroy(gameObject);
        OnEventsUnscribe();
    }

}
