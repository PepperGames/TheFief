using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureCircle : MonoBehaviour
{
    public void SetRange(float range)
    {
        transform.localScale = new Vector2(range, range);
    }
}
