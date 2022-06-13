using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FamilyTree : MonoBehaviour
{
    [SerializeField] private Button _closeButton;

    [SerializeField] private FamilyTreeNodeView _familyTreeNodeViewPrefab;
    [SerializeField] private HardFamilyTree _hardFamilyTree;

    [SerializeField] private Character _character;

    [SerializeField] private List<Character> _firstGeneration;
    [SerializeField] private List<Character> _secondGeneration;
    [SerializeField] private List<Character> thirdGeneration;


    [SerializeField] private List<FamilyTreeNodeView> _familyTreeNodeViews = new List<FamilyTreeNodeView>();

    private static bool _hardFamilyTreeDisplayed = false;
    private bool _hardFamilyTreeDisplayedHere = false;

    private void Start()
    {
        _closeButton.onClick.AddListener(Close);
    }

    public void Initialize(Character character)
    {
        gameObject.SetActive(true);

        if (_hardFamilyTreeDisplayed == false)
        {
            _hardFamilyTree.Initialize(character);
            _hardFamilyTreeDisplayed = true;
            _hardFamilyTreeDisplayedHere = true;
        }
        else if (_hardFamilyTreeDisplayedHere == false)
        {
            Debug.Log("Initialize");
            _character = character;

            Clear();

            SetGenerations();
            DrawTree();
        }
    }

    public void Close()
    {
        Debug.Log("Close");
        gameObject.SetActive(false);
    }

    private void DrawTree()
    {
        Debug.Log("DrawTree");
        FamilyTreeNodeView familyTreeNodeView = Instantiate(_familyTreeNodeViewPrefab, transform.position, Quaternion.identity, transform);
        Debug.Log("familyTreeNodeView1" + familyTreeNodeView);
        familyTreeNodeView.Initialize(_character.CharacterData.Portrait, _character.CharacterData.CharacterName);
        Debug.Log("familyTreeNodeView2" + familyTreeNodeView);

        _familyTreeNodeViews.Add(familyTreeNodeView);
        Debug.Log("_familyTreeNodeViews" + _familyTreeNodeViews);
        Debug.Log("_familyTreeNodeViews.Count" + _familyTreeNodeViews.Count);
    }

    private void SetGenerations()
    {
        Debug.Log("SetGenerations");
        SetFirstGeneration();
    }

    private void SetFirstGeneration()
    {
        Debug.Log("SetFirstGeneration");

    }

    private void Clear()
    {
        Debug.Log("Clear");
        foreach (var item in _familyTreeNodeViews)
        {
            Destroy(item.gameObject);
        }
        _familyTreeNodeViews = new List<FamilyTreeNodeView>();
    }
}
