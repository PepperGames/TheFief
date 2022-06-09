using UnityEngine;

public class StructureCircle : MonoBehaviour
{
    public void Initialize()
    {
        Debug.Log("Initialize");
        BuilderModeView.Instance.OnModeChange += ChangeViewMode;
        ChangeViewMode();
    }

    private void ChangeViewMode()
    {
        Debug.LogError("ChangeViewMode");
        Debug.LogError(BuilderModeView.Instance.IsActive);
        if (BuilderModeView.Instance.IsActive == false)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public void SetRange(float range)
    {
        transform.localScale = new Vector2(range, range);
    }
}
