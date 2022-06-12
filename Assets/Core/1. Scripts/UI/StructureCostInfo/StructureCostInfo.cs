using UnityEngine;

public abstract class StructureCostInfo : MonoBehaviour
{
    protected virtual void Start()
    {
        Hide();
    }

    public virtual void Show(Resources resources)
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
