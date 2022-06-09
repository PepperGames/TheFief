using UnityEngine;

public class StructureCircle : MonoBehaviour
{
    public void Initialize()
    {
        BuilderModeView.Instance.OnModeChange += ChangeViewMode;
        ChangeViewMode();
    }

    private void ChangeViewMode()
    {
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

    private void OnDestroy()
    {
        BuilderModeView.Instance.OnModeChange -= ChangeViewMode;
    }
}
