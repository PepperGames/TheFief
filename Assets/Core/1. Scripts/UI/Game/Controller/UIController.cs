using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private BuildingUIController buildingUIController;
    [SerializeField] private ListOfAblebodiedCharacters listOfAblebodiedCharacters;

    [SerializeField] public BuildingUIController BuildingUIController => buildingUIController;
    [SerializeField] public ListOfAblebodiedCharacters ListOfAblebodiedCharacters => listOfAblebodiedCharacters;
    
}