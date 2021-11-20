using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static Zenject.ZenAutoInjecter;

public abstract class BasicStructure : MonoBehaviour
{
    public GameObject model; //обжект у которого дети это арт
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

    public Action OnInitialize;
    public void Initialize()
    {
        ZenAutoInjecter zenAutoInjecter = gameObject.AddComponent<ZenAutoInjecter>();
        zenAutoInjecter.ContainerSource = ContainerSources.SceneContext;
        structureCost = gameObject.GetComponent<StructureCost>();
        structureCost.IncreaseCurrentCost(structureCost.GetAmountOfResourcesForBuild());
    }

    public void SwapModelView(GameObject newModel, Quaternion rotation)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        var currentModelView = Instantiate(newModel, transform);
        currentModelView.transform.localPosition = new Vector3(0, 0, 0);
        currentModelView.transform.localRotation = rotation;
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
