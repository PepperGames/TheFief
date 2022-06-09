using UnityEngine;

public class Services : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private PlacementManager _placementManager;
    [SerializeField] private RoadManager _roadManager;
    [SerializeField] private RoadFixer _roadFixer;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private StructureManager _structureManager;
    [SerializeField] private ResourcesManager _resourcesManager;
    [SerializeField] private HappinessManager _happinessManager;
    [SerializeField] private FaithManager _faithManager;
    [SerializeField] private CharacterManager _characterManager;
    [SerializeField] private AiDirector _aiDirector;
    [SerializeField] private UIController _UIController;
    [SerializeField] private Grid _grid;
    [SerializeField] private CharacterAttractor _characterAttractor;

    public GameManager GameManager => _gameManager;
    public Grid Grid => _grid;
    public PlacementManager PlacementManager => _placementManager;
    public RoadManager RoadManager => _roadManager;
    public RoadFixer RoadFixer => _roadFixer;
    public InputManager InputManager => _inputManager;
    public StructureManager StructureManager => _structureManager;
    public ResourcesManager ResourcesManager => _resourcesManager;
    public HappinessManager HappinessManager => _happinessManager;
    public FaithManager FaithManager => _faithManager;
    public CharacterManager CharacterManager => _characterManager;
    public AiDirector AiDirector => _aiDirector;
    public UIController UIController => _UIController;
    public CharacterAttractor CharacterAttractor => _characterAttractor;

}
