using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static Zenject.ZenAutoInjecter;

public abstract class BasicStructure : MonoBehaviour, IDemolishable
{
    public GameObject model; //в теории это обжект у которого дети это арт  || или это и  есть сам обьект
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

    public void Initialize()
    {
        //var structure = Instantiate(model, transform);
        ZenAutoInjecter zenAutoInjecter = gameObject.AddComponent<ZenAutoInjecter>();
        zenAutoInjecter.ContainerSource = ContainerSources.SceneContext;
    }

    public void SwapModelView(GameObject model, Quaternion rotation)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        var structure = Instantiate(model, transform);
        structure.transform.localPosition = new Vector3(0, 0, 0);
        structure.transform.localRotation = rotation;
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
