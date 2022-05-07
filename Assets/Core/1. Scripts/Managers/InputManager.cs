using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class InputManager : MonoBehaviour
{
    public Action<Vector2Int> OnMouseClick;
    public Action<Vector2Int> OnMouseHold;
    public Action OnMouseUp;

    [SerializeField] private Camera mainCamera;
    [Inject] [SerializeField] private Services services;

    public LayerMask groundMask;

    private bool isMouseDown = false;

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void Update()
    {
        CheckClickDownEvent();
        CheckClickUpEvent();
        CheckClickHoldEvent();
    }

    private Vector2Int? RaycastGround()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero, Mathf.Infinity, groundMask);
            if (hit.collider != null)
            {
                Vector2Int positionInt = Vector2Int.RoundToInt(hit.point);
                return positionInt;
            }
            return null;
        }
        return null;
    }

    private void CheckClickDownEvent()
    {
        if (isMouseDown == true)
            return;

        if (Input.GetMouseButton(0))
        {
            var position = RaycastGround();
            if (PositionInGridBounds(position))
            {
                OnMouseClick?.Invoke(position.Value);
                isMouseDown = true;
            }
        }
    }

    private void CheckClickHoldEvent()
    {
        if (isMouseDown == false)
            return;

        if (Input.GetMouseButton(0))
        {
            var position = RaycastGround();
            if (PositionInGridBounds(position))
            {
                OnMouseHold?.Invoke(position.Value);
            }
        }
    }

    private bool PositionInGridBounds(Vector2Int? position)
    {
        if (position != null)
        {
            if ((position.Value.x >= 0 && position.Value.x < services.Grid.Width) && (position.Value.y >= 0 && position.Value.y < services.Grid.Height))
            {
                return true;
            }
            return false;
        }
        return false;
    }

    private void CheckClickUpEvent()
    {
        if (Input.GetMouseButton(0) == false && isMouseDown == true)
        {
            OnMouseUp?.Invoke();
            isMouseDown = false;
        }
    }
}
