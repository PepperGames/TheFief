using UnityEngine;

public class AlertList : MonoBehaviour
{
    [SerializeField] protected Transform content;

    public void ClearContent()
    {
        foreach (Transform character in content)
        {
            Destroy(character.gameObject);
        }
    }

    public GameObject Create(GameObject popupItem)
    {
        GameObject popup = Instantiate(popupItem, content);
        return popup;
    }
}
