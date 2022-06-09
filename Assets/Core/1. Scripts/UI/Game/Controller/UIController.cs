using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private BuildingUIController _buildingUIController;
    [SerializeField] private ListOfAblebodiedCharacters _listOfAblebodiedCharacters;
    [SerializeField] private ListOfLivingCharacters _listOfLivingCharacters;
    [SerializeField] private AlertList _alertList;

    public BuildingUIController BuildingUIController => _buildingUIController;

    public ListOfAblebodiedCharacters ListOfAblebodiedCharacters
    {
        get
        {
            _listOfLivingCharacters.Close();
            return _listOfAblebodiedCharacters;
        }
    }
    public ListOfLivingCharacters ListOfLivingCharacters
    {
        get
        {
            _listOfAblebodiedCharacters.Close();
            return _listOfLivingCharacters;
        }
    }
    public AlertList AlertList => _alertList;

}