using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class BasicStructure : MonoBehaviour
{
    //public GameObject model; //������ � �������� ���� ��� ���
    public GameObject modelView;

    [SerializeField] private bool drawGizmo = true;

    [SerializeField] private List<Vector2Int> points;
    public List<Vector2Int> Points
    {
        get
        {
            return points;
        }
    }

    [HideInInspector] public StructureCost structureCost;

    [Inject] protected Services services;

    public Action OnInitialize;

    public void Initialize()
    {
        structureCost = gameObject.GetComponent<StructureCost>();
        structureCost.IncreaseCurrentCost(structureCost.GetAmountOfResourcesForBuild());

        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "ModelView")
            {
                modelView = child.gameObject;
                break;
            }
        }
    }

    public void SwapModelView(GameObject newModel, Quaternion rotation)
    {
        Destroy(modelView);

        modelView = Instantiate(newModel, transform);
        modelView.transform.localPosition = new Vector3(0, 0, 0);
        modelView.transform.localRotation = rotation;
    }

    public virtual void Demolish()
    {
        Vector3 structurePosition = transform.position;
        services.PlacementManager.Demolish(new Vector2Int((int)structurePosition.x, (int)structurePosition.y));
    }

    protected virtual void OnDemolish() { }

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

    protected virtual void OnDestroy()
    {
        OnDemolish();
    }
}
