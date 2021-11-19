using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public Action<Vector2Int> OnMouseClick;
    public Action<Vector2Int> OnMouseHold;
    public Action OnMouseUp;

    private Vector2 cameraMovementVector;

    [SerializeField] private Camera mainCamera;

    public LayerMask groundMask;

    private bool isMouseDown = false;

    public Vector2 CameraMovementVector
    {
        get { return cameraMovementVector; }
    }

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
        CheckArrowInput();
    }

    private Vector2Int? RaycastGround()
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

    private void CheckArrowInput()
    {
        cameraMovementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void CheckClickDownEvent()
    {
        if (isMouseDown == true)
            return;

        if (Input.GetMouseButton(0))
        {
            var position = RaycastGround();
            if (position != null)
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
            if (position != null)
            {
                OnMouseHold?.Invoke(position.Value);
            }
        }
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
