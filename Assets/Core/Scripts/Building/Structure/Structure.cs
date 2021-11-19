using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

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

    [SerializeField] protected int lvl;
    [SerializeField] protected int maxLvl;

    public StructureCost structureCost;

    [Inject] [SerializeField] protected ResourcesManager resourcesManager;

    protected virtual void Start()
    {
        maxLvl = structureCost.GetMaxLvl();
        StartCoroutine("WaitForImprove");
        StartCoroutine("WaitForDemolish");
        lvl = 1;
    }

    public void Demolish()
    {
        gameObject.transform.localScale /= 3f;
    }
   
    IEnumerator WaitForDemolish() //TODO удалить
    {
        yield return new WaitForSeconds(4f);
        Demolish();
        Debug.Log("Demolish");
    }

    public bool CanBeImprove()
    {
        int newLvl = lvl + 1;
        print(newLvl);
        if (newLvl <= maxLvl)
        {
            if (resourcesManager.EnoughResources(structureCost.GetAmountOfResourcesForUpdate(newLvl)))
            {
                Debug.Log("CanBeImprove");
                return true;
            }
        }
        Debug.Log("Not CanBeImprove");
        return false;
    }

    public virtual void Improve()
    {
        lvl++;
        resourcesManager.SpendResources(structureCost.GetAmountOfResourcesForUpdate(lvl));
    }

    //TODO
    IEnumerator WaitForImprove() //TODO удалить
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
