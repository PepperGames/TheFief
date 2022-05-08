using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private BuildingUIController _buildingUIController;
    [SerializeField] private ListOfAblebodiedCharacters _listOfAblebodiedCharacters;
    [SerializeField] private ListOfLivingCharacters _listOfLivingCharacters;

    [SerializeField] public BuildingUIController BuildingUIController => _buildingUIController;
    [SerializeField] public ListOfAblebodiedCharacters ListOfAblebodiedCharacters => _listOfAblebodiedCharacters;
    [SerializeField] public ListOfLivingCharacters ListOfLivingCharacters => _listOfLivingCharacters;
    
}