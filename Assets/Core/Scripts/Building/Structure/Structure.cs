using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StructureCost))]
public abstract class Structure : MonoBehaviour, IDemolishable, IImprovable
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
    [SerializeField] protected int lvl = 1;
    [SerializeField] protected int maxLvl;
    public StructureCost structureCost;

    protected virtual void Start()
    {
        maxLvl = structureCost.GetMaxLvl();
        StartCoroutine("WaitForImprove");
    }

    public void Demolish()
    {
        throw new System.NotImplementedException();
    }
    public virtual void Improve()
    {
        lvl++;
    }

    IEnumerator WaitForImprove()
    {
        yield return new WaitForSeconds(4f);
        Improve();
        Debug.Log("Improve");
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
