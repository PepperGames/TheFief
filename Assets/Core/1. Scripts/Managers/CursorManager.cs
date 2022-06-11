using System.Collections;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D _standartCursor;
    public Texture2D _errorcursor;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    private static CursorManager _instance;
    public static CursorManager Instance
    {
        get
        {
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

    private void Start()
    {
        Instance = this;
    }

    public void SetErrorcursor()
    {
        StartCoroutine(SetErrorcursorForTime());
    }

    private IEnumerator SetErrorcursorForTime()
    {
        SetCursor(_errorcursor);
        yield return new WaitForSeconds(1.5f);
        SetCursor(_standartCursor);
    }

    private void SetCursor(Texture2D cursor)
    {
        Cursor.SetCursor(cursor, hotSpot, cursorMode);
    }
}
