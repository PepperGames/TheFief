using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour, IDemolished
{
    public GameObject prefab;
    [SerializeField] private bool drawGizmo = true;

    [SerializeField] private List<Vector2Int> points;
    public List<Vector2Int> Points
    {
        get
        {
            return points;
        }
    }

    public void Demolish()
    {
        throw new System.NotImplementedException();
    }

    private void OnDrawGizmos()
    {
        if (drawGizmo)
        {
            foreach (Vector2Int item in Points)
            {
                Gizmos.color = new Color(1, 0, 0, 0.5f);
                Gizmos.DrawCube(new Vector3(item.x, item.y), new Vector3(1, 1, 0));
            }
        }
    }

}
