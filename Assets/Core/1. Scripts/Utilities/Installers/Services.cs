using UnityEngine;

public class Services : MonoBehaviour
{
    [SerializeField] private PlacementManager _placementManager;
    [SerializeField] private RoadManager _roadManager;
    [SerializeField] private RoadFixer _roadFixer;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private StructureManager _structureManager;
    [SerializeField] private ResourcesManager _resourcesManager;
    [SerializeField] private CharacterManager _characterManager;
    [SerializeField] private AiDirector _aiDirector;
    [SerializeField] private UIController _UIController;
    [SerializeField] private Grid _grid;

    public Grid Grid => _grid;
    public PlacementManager PlacementManager => _placementManager;
    public RoadManager RoadManager => _roadManager;
    public RoadFixer RoadFixer => _roadFixer;
    public InputManager InputManager => _inputManager;
    public StructureManager StructureManager => _structureManager;
    public ResourcesManager ResourcesManager => _resourcesManager;
    public CharacterManager CharacterManager => _characterManager;
    public AiDirector AiDirector => _aiDirector;
    public UIController UIController => _UIController;

}
