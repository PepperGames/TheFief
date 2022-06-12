using UnityEngine;

public abstract class StructureCostInfo : MonoBehaviour
{
    public virtual void Show(Resources resources)
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
