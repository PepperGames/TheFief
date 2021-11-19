using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static Zenject.ZenAutoInjecter;

public class StructureModel : MonoBehaviour
{
    float yHeight = 0;

    public void CreateModel(GameObject model)
    {
        var structure = Instantiate(model, transform);
        ZenAutoInjecter zenAutoInjecter = structure.AddComponent<ZenAutoInjecter>();
        zenAutoInjecter.ContainerSource = ContainerSources.SceneContext;
        yHeight = model.transform.position.y;
    }

    public void SwapModel(GameObject model, Quaternion rotation)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        var structure = Instantiate(model, transform);
        structure.transform.localPosition = new Vector3(0, yHeight, 0);
        structure.transform.localRotation = rotation;
    }
    // � �������� ����� ���� ��������
}
