using UnityEngine;
using DG.Tweening;

public class UICondomSizeSeter : MonoBehaviour
{
    [SerializeField] private RectTransform _targetRectTransform;
    [SerializeField] private RectTransform _parentRectTransform;

    //private const float _targetHeightByPixelSize = 380.8f;
    //private const float _targetWidthByPixelSize = 62.54f;

    //private const float _targetCenterPixelSize = 380.8f / 2 - 59.2f;
    //private const float _targetHeightByPixelSize3 = -59.2f;

    [SerializeField] private GameObject player;
    [SerializeField] private Camera cam;

    private float targetWidthByPixelSize;
    private float targetHeightByPixelSize;
    //-2.5   -4.9  -2.4
    //5.45+4.55
    //4.92-0.45

    public void Activate()
    {
        SetSize();
        SetPosition();
    }

    private void SetSize()
    {
        targetWidthByPixelSize = GetTargetWidthByPixelSize(player);
        float percentCondomWidthFromScreen = targetWidthByPixelSize / Screen.width;

        targetHeightByPixelSize = GetTargetHeightByPixelSize(player);
        float percentCondomHeightFromScreen = targetHeightByPixelSize / Screen.height;

        _targetRectTransform.sizeDelta = new Vector2(
            _parentRectTransform.sizeDelta.x * percentCondomWidthFromScreen,
            _parentRectTransform.sizeDelta.y * percentCondomHeightFromScreen);
    }

    private float GetTargetWidthByPixelSize(GameObject target)
    {
        Vector3 center = target.GetComponent<Renderer>().bounds.center;
        Vector3 size = target.GetComponent<Renderer>().bounds.size;

        float left = cam.WorldToScreenPoint(new Vector3(center.x - size.x / 2, center.y, center.z)).x;
        float right = cam.WorldToScreenPoint(new Vector3(center.x + size.x / 2, center.y, center.z)).x;

        float targetWidth = right - left;

        //Debug.Log("targetWidth " + targetWidth);
        return targetWidth;
    }

    private float GetTargetHeightByPixelSize(GameObject target)
    {
        Vector3 center = target.GetComponent<Renderer>().bounds.center;
        Vector3 size = target.GetComponent<Renderer>().bounds.size;

        float top = cam.WorldToScreenPoint(new Vector3(center.x, center.y + size.y / 2, center.z)).y;
        float bottom = cam.WorldToScreenPoint(new Vector3(center.x, center.y - size.y / 2, center.z)).y;

        float targetHeight = top - bottom;

        //Debug.Log("targetHeight " + targetHeight);
        return targetHeight;
    }

    private void SetPosition()
    {
        float finalYPosition = GetFinalYPosition();
        Sequence mySequence = DOTween.Sequence();
        mySequence.PrependInterval(0.8f).Append(_targetRectTransform.transform.DOLocalMoveY(finalYPosition, 1));
    }

    private float GetFinalYPosition()
    {
        float res = 5.37f / 10;
        return 0 - _parentRectTransform.sizeDelta.y * res;
    }
}