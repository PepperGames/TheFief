using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicStructure : MonoBehaviour, IDemolishable
{
    public GameObject prefab; //в теории это обжект у которого дети это арт  || или это и  есть сам обьект

    [SerializeField] private bool drawGizmo = true;

    [SerializeField] private List<Vector2Int> points;
    public List<Vector2Int> Points
    {
        get
        {
            return points;
        }
    }

    public virtual void Demolish()
    {
        gameObject.transform.localScale /= 3f;
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
