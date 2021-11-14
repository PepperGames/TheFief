using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StructureCost))]
public class Structure : MonoBehaviour, IDemolishable
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
    protected int lvl = 1;
    [SerializeField] protected int maxLvl;
    public StructureCost structureCost;

    protected virtual void Start()
    {
        maxLvl = structureCost.GetMaxLvl();
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
