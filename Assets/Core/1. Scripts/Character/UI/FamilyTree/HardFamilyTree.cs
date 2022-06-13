using UnityEngine;

public class HardFamilyTree : MonoBehaviour
{
    [SerializeField] private FamilyTreeNodeView _familyTreeNodeViewMale;
    [SerializeField] private FamilyTreeNodeView _familyTreeNodeViewFemale;

    [SerializeField] private Character _character;

    public void Initialize(Character character)
    {
        Debug.Log("Initialize");
        gameObject.SetActive(true);
        _character = character;

        DrawTree();
    }

    private void DrawTree()
    {
        if (_character.CharacterData.Gender==Genders.Female)
        {
            _familyTreeNodeViewFemale.Initialize(_character.CharacterData.Portrait, _character.CharacterData.CharacterName);
        }
        else
        {
            _familyTreeNodeViewMale.Initialize(_character.CharacterData.Portrait, _character.CharacterData.CharacterName);
        }
    }
}
