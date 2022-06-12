using UnityEngine;
using TMPro;

public class StructureBuildCostInfo : StructureCostInfo
{
    [SerializeField] protected TMP_Text _moneyText;
    [SerializeField] protected TMP_Text _woodText;
    [SerializeField] protected TMP_Text _stoneText;
    [SerializeField] protected TMP_Text _metalText;

    protected virtual void Start()
    {
        Hide();
    }

    public override void Show(Resources resources)
    {
        base.Show(resources);

        _moneyText.text = CharacterRounder.Round(resources.Money, 0);
        _woodText.text = CharacterRounder.Round(resources.Wood, 0);
        _stoneText.text = CharacterRounder.Round(resources.Stone, 0);
        _metalText.text = CharacterRounder.Round(resources.Metal, 0);
    }
}
